using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CookieHeaderValue = Microsoft.Net.Http.Headers.CookieHeaderValue;

namespace SupplementStore.Tests.Integration {

    public abstract class IntegrationTestsBase {

        static HttpClient Client { get; set; }

        static IntegrationTestsBase() {

            var testsPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = testsPath.Substring(0, testsPath.IndexOf(".Tests", testsPath.IndexOf(typeof(IntegrationTestsBase).Assembly.GetName().Name)));

            var webHost = WebHost.CreateDefaultBuilder<TestStartup>(null)
                .UseContentRoot(applicationPath)
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration((context, b) => {
                    context.HostingEnvironment.ApplicationName = typeof(Startup).Assembly.GetName().Name;
                });

            Client = new TestServer(webHost).CreateClient();
        }

        [TestInitialize]
        public void Setup() {

            Content = "";
            ExceptionThrown = null;

            TestDocumentApprover.ClearDocuments();

            Mocks.Reset();
        }

        IdentityUser User { get; set; }

        HttpResponseHeaders Headers { get; set; }

        string Content { get; set; }

        Exception ExceptionThrown { get; set; }

        protected async Task GetAsync(string requestUri, IdentityUser user = null) {

            await ManageAuthentication(user);

            var response = await Client.GetAsync(requestUri);

            Content = await response.Content.ReadAsStringAsync();

            Headers = response.Headers;
        }

        protected async Task PostAsync(string requestUri, Dictionary<string, string> formData, IdentityUser user = null) {

            await ManageAuthentication(user);

            formData = await ArrangeAntiforgeryFormData(formData);

            var formContent = new FormUrlEncodedContent(formData);

            try {

                var response = await Client.PostAsync(requestUri, formContent);

                Content = await response.Content.ReadAsStringAsync();

                Headers = response.Headers;
            }
            catch (Exception e) {

                ExceptionThrown = e;
            }
        }

        protected async Task PostAsync(string requestUri, Dictionary<string, string> formData, FormFileData formFileData, IdentityUser user = null) {

            await ManageAuthentication(user);

            var content = new MultipartFormDataContent {
                { new StreamContent(new MemoryStream(formFileData.Bytes)), formFileData.Name, formFileData.FileName }
            };

            formData = await ArrangeAntiforgeryFormData(formData);

            foreach (var dataPiece in formData) {

                content.Add(new StringContent(dataPiece.Value), dataPiece.Key);
            }

            try {

                var response = await Client.PostAsync(requestUri, content);

                Content = await response.Content.ReadAsStringAsync();

                Headers = response.Headers;
            }
            catch (Exception e) {

                ExceptionThrown = e;
            }
        }

        protected async Task PostAsync(string requestUri, IdentityUser user) {

            await PostAsync(requestUri, null, user);
        }

        protected async Task PostAsync(string requestUri) {

            await PostAsync(requestUri, null, null);
        }

        protected async Task PatchAsync(string requestUri, params object[] operations) {

            var jsonData = JsonConvert.SerializeObject(operations);

            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            httpContent.Headers.Add("RequestVerificationToken", await ArrangeAntiforgery());

            await Client.PatchAsync(requestUri, httpContent);
        }

        protected async Task DeleteAsync(string requestUri, IdentityUser user = null) {

            await ManageAuthentication(user);

            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            httpMessage.Headers.Add("RequestVerificationToken", await ArrangeAntiforgery());

            var response = await Client.SendAsync(httpMessage);
        }

        protected void Examine(ContentScheme contentScheme) {

            AssertAgainstRedirection();

            contentScheme.Examine(Content);
        }

        protected void Examine(string exactContent) {

            AssertAgainstRedirection();

            if (Content != exactContent)
                throw new AssertFailedException($"The received content does not equal the examined string. \nExpected: '{exactContent}'; \nContent: '{Content}'");
        }

        protected void ExamineRedirect(string uri) {

            if (Headers.Location == null)
                throw new AssertFailedException($"No redirection was detected. \nExpected: '{uri}'");

            if (Headers.Location.ToString() != uri)
                throw new AssertFailedException($"An unexpected redirection was detected. \nExpected: '{uri}'; \nActual: '{Headers.Location}'");
        }

        protected void ExamineAuthRedirect(string returnUri) {

            Assert.IsNotNull(Headers.Location, $"No authentication redirection was detected. Expected: '/Account/Login?ReturnUrl={returnUri}'");

            Assert.IsTrue(Headers.Location.ToString().EndsWith($"/Account/Login?ReturnUrl={ Uri.EscapeDataString(returnUri) }", StringComparison.OrdinalIgnoreCase),
                $"Redirection was performed to '{Headers.Location}' instead of '/Account/Login?ReturnUrl={returnUri}'");
        }

        protected void ExamineAccessDeniedRedirect(string returnUri) {

            Assert.IsNotNull(Headers.Location, $"No redirection was detected. Expected: '/Account/AccessDenied?ReturnUrl={returnUri}'");

            Assert.IsTrue(Headers.Location.ToString().EndsWith($"/Account/AccessDenied?ReturnUrl={ Uri.EscapeDataString(returnUri) }", StringComparison.OrdinalIgnoreCase),
                $"Redirection was performed to '{Headers.Location}' instead of '/Account/AccessDenied?ReturnUrl={returnUri}'");
        }

        protected void ExamineExceptionThrown<TException>()
            where TException : Exception {

            if (ExceptionThrown == null)
                throw new AssertFailedException("No exception was detected.");

            if ((ExceptionThrown is TException) == false)
                throw new AssertFailedException($"No expected exception was detected. Expected: {typeof(TException).FullName}; Actual: {ExceptionThrown.GetType().FullName};");
        }

        protected void ExamineNoExceptionThrown() {

            if (ExceptionThrown != null)
                throw new AssertFailedException($"Exception was thrown. \nException: {ExceptionThrown}");
        }

        void AssertAgainstRedirection() {

            Assert.IsNotNull(Headers, "No headers from the previous request were detected.");

            Assert.IsNull(Headers.Location, $"Detected unexpected redirection to '{Headers.Location}'.");
        }

        async Task ManageAuthentication(IdentityUser user) {

            if (user != null && User != user) {

                await ArrangeAuthentication(user);
            }
            else {

                Client.DefaultRequestHeaders.Remove("Cookie");
            }

            User = user;
        }

        async Task ArrangeAuthentication(IdentityUser user) {

            var formData = await ArrangeAntiforgeryFormData(new Dictionary<string, string>
            {
                { "Email", user.Email },
                { "Password", TestData.Password }
            });

            var response = await Client.PostAsync("/Account/Login", new FormUrlEncodedContent(formData));

            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);

            if (response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values)) {

                var authenticationCookie = SetCookieHeaderValue
                    .ParseList(values
                    .ToList())
                    .SingleOrDefault(c => c.Name.StartsWith(".AspNetCore.Identity.Application", StringComparison.InvariantCultureIgnoreCase) && c.Value != "");

                Client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue(authenticationCookie.Name, authenticationCookie.Value).ToString());
            }
        }

        async Task<Dictionary<string, string>> ArrangeAntiforgeryFormData(Dictionary<string, string> formData = null) {

            if (formData == null)
                formData = new Dictionary<string, string>();

            formData.Add("__RequestVerificationToken", await ArrangeAntiforgery());

            return formData;
        }

        async Task<string> ArrangeAntiforgery() {

            var loginResponse = await Client.GetAsync("/Account/Login");

            if (loginResponse.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> values)) {

                var antiforgeryCookie = SetCookieHeaderValue
                    .ParseList(values.ToList())
                    .SingleOrDefault(c => c.Name.StartsWith(".AspNetCore.AntiForgery.", StringComparison.InvariantCultureIgnoreCase));

                Client.DefaultRequestHeaders.Add("Cookie", new CookieHeaderValue(antiforgeryCookie.Name, antiforgeryCookie.Value).ToString());
            }

            var loginResponseContent = await loginResponse.Content.ReadAsStringAsync();

            var antiforgeryFormFieldRegex = new Regex(@"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
            var match = antiforgeryFormFieldRegex.Match(loginResponseContent);

            var antiforgeryToken = match.Success ? match.Groups[1].Captures[0].Value : null;

            return antiforgeryToken;
        }
    }
}
