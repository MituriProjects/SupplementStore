using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration {

    public abstract class IntegrationTestsBase {

        HttpClientManager ClientManager { get; } = new HttpClientManager();

        [TestInitialize]
        public void Setup() {

            ResponseDetails = new NullResponseDetails();

            TestDocumentApprover.ClearDocuments();

            Mocks.Reset();
        }

        IResponseDetails ResponseDetails { get; set; }

        protected async Task GetAsync(string requestUri, IdentityUser user = null) {

            ResponseDetails = await ClientManager.GetAsync(requestUri, user);
        }

        protected async Task PostAsync(string requestUri, Dictionary<string, string> formData, IdentityUser user = null) {

            ResponseDetails = await ClientManager.PostAsync(requestUri, formData, user);
        }

        protected async Task PostAsync(string requestUri, Dictionary<string, string> formData, FormFileData formFileData, IdentityUser user = null) {

            ResponseDetails = await ClientManager.PostAsync(requestUri, formData, formFileData, user);
        }

        protected async Task PostAsync(string requestUri, IdentityUser user) {

            await PostAsync(requestUri, null, user);
        }

        protected async Task PostAsync(string requestUri) {

            await PostAsync(requestUri, null, null);
        }

        protected async Task PatchAsync(string requestUri, params object[] operations) {

            await ClientManager.PatchAsync(requestUri, operations);
        }

        protected async Task DeleteAsync(string requestUri, IdentityUser user = null) {

            await ClientManager.DeleteAsync(requestUri, user);
        }

        protected void Examine(ContentScheme contentScheme) {

            ResponseDetails.Examine(contentScheme);
        }

        protected void Examine(string exactContent) {

            ResponseDetails.Examine(exactContent);
        }

        protected void ExamineRedirect(string uri) {

            ResponseDetails.ExamineRedirect(uri);
        }

        protected void ExamineAuthRedirect(string returnUri) {

            ResponseDetails.ExamineAuthRedirect(returnUri);
        }

        protected void ExamineAccessDeniedRedirect(string returnUri) {

            ResponseDetails.ExamineAccessDeniedRedirect(returnUri);
        }

        protected void ExamineExceptionThrown<TException>()
            where TException : Exception {

            ResponseDetails.ExamineExceptionThrown<TException>();
        }

        protected void ExamineNoExceptionThrown() {

            ResponseDetails.ExamineNoExceptionThrown();
        }
    }
}
