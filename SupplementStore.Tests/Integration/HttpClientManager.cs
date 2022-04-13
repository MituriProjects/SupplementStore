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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration {

    class HttpClientManager {

        static HttpClient Client { get; set; }

        static HttpClientManager() {

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

        IdentityUser User { get; set; }

        public async Task<HttpResponseDetails> GetAsync(string requestUri, IdentityUser user = null) {

            await ManageAuthentication(user);

            var response = await Client.GetAsync(requestUri);

            return HttpResponseDetails.WithResponse(response);
        }

        public async Task<HttpResponseDetails> PostAsync(string requestUri, Dictionary<string, string> formData, IdentityUser user = null) {

            await ManageAuthentication(user);

            formData = await ArrangeAntiforgeryFormData(formData);

            var formContent = new FormUrlEncodedContent(formData);

            try {

                var response = await Client.PostAsync(requestUri, formContent);

                return HttpResponseDetails.WithResponse(response);
            }
            catch (Exception e) {

                return HttpResponseDetails.WithException(e);
            }
        }

        public async Task<HttpResponseDetails> PostAsync(string requestUri, Dictionary<string, string> formData, FormFileData formFileData, IdentityUser user = null) {

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

                return HttpResponseDetails.WithResponse(response);
            }
            catch (Exception e) {

                return HttpResponseDetails.WithException(e);
            }
        }

        public async Task PatchAsync(string requestUri, params object[] operations) {

            var jsonData = JsonConvert.SerializeObject(operations);

            var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            httpContent.Headers.Add("RequestVerificationToken", await ArrangeAntiforgery());

            await Client.PatchAsync(requestUri, httpContent);
        }

        public async Task DeleteAsync(string requestUri, IdentityUser user = null) {

            await ManageAuthentication(user);

            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            httpMessage.Headers.Add("RequestVerificationToken", await ArrangeAntiforgery());

            await Client.SendAsync(httpMessage);
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
