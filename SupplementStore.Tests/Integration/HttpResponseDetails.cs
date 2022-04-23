using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SupplementStore.Tests.Integration {

    class HttpResponseDetails : IResponseDetails {

        string Content { get; set; }

        HttpResponseHeaders Headers { get; set; }

        Exception ExceptionThrown { get; set; }

        public static HttpResponseDetails WithResponse(HttpResponseMessage httpResponse) {

            return new HttpResponseDetails {
                Content = httpResponse.Content.ReadAsStringAsync().Result,
                Headers = httpResponse.Headers
            };
        }

        public static HttpResponseDetails WithException(Exception exception) {

            return new HttpResponseDetails {
                ExceptionThrown = exception
            };
        }

        public void Examine(ContentScheme contentScheme) {

            AssertAgainstRedirection();

            contentScheme.Examine(Content);
        }

        public void Examine(string exactContent) {

            AssertAgainstRedirection();

            if (Content != exactContent)
                throw new AssertFailedException($"The received content does not equal the examined string. \nExpected: '{exactContent}'; \nContent: '{Content}'");
        }

        public void ExamineRedirect(string uri) {

            if (Headers.Location == null)
                throw new AssertFailedException($"No redirection was detected. \nExpected: '{uri}'");

            if (Headers.Location.ToString() != uri)
                throw new AssertFailedException($"An unexpected redirection was detected. \nExpected: '{uri}'; \nActual: '{Headers.Location}'");
        }

        public void ExamineAuthRedirect(string returnUri) {

            Assert.IsNotNull(Headers.Location, $"No authentication redirection was detected. Expected: '/Account/Login?ReturnUrl={returnUri}'");

            Assert.IsTrue(Headers.Location.ToString().EndsWith($"/Account/Login?ReturnUrl={ Uri.EscapeDataString(returnUri) }", StringComparison.OrdinalIgnoreCase),
                $"Redirection was performed to '{Headers.Location}' instead of '/Account/Login?ReturnUrl={returnUri}'");
        }

        public void ExamineAccessDeniedRedirect(string returnUri) {

            Assert.IsNotNull(Headers.Location, $"No redirection was detected. Expected: '/Account/AccessDenied?ReturnUrl={returnUri}'");

            Assert.IsTrue(Headers.Location.ToString().EndsWith($"/Account/AccessDenied?ReturnUrl={ Uri.EscapeDataString(returnUri) }", StringComparison.OrdinalIgnoreCase),
                $"Redirection was performed to '{Headers.Location}' instead of '/Account/AccessDenied?ReturnUrl={returnUri}'");
        }

        public void ExamineExceptionThrown<TException>()
            where TException : Exception {

            if (ExceptionThrown == null)
                throw new AssertFailedException("No exception was detected.");

            if ((ExceptionThrown is TException) == false)
                throw new AssertFailedException($"No expected exception was detected. Expected: {typeof(TException).FullName}; Actual: {ExceptionThrown.GetType().FullName};");
        }

        public void ExamineNoExceptionThrown() {

            if (ExceptionThrown != null)
                throw new AssertFailedException($"Exception was thrown. \nException: {ExceptionThrown}");
        }

        void AssertAgainstRedirection() {

            Assert.IsNotNull(Headers, "No headers from the performed request were detected.");

            Assert.IsNull(Headers.Location, $"Detected unexpected redirection to '{Headers.Location}'.");
        }
    }
}
