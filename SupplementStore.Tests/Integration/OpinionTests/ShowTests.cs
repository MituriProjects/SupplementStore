using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Opinions;
using SupplementStore.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.OpinionTests {

    [TestClass]
    public class ShowTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            var opinionId = Guid.NewGuid();

            await PostAsync($"/Opinion/Show/{opinionId}");

            ExamineAuthRedirect($"/Opinion/Show/{opinionId}");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            var opinionId = Guid.NewGuid();

            await PostAsync($"/Opinion/Show/{opinionId}", TestData.User);

            ExamineAccessDeniedRedirect($"/Opinion/Show/{opinionId}");
        }

        [TestMethod]
        public async Task UserIsAuthorized_RedirectsToAdminHiddenOpinions() {

            var opinion = TestEntity.Random<Opinion>();

            await PostAsync($"/Opinion/Show/{opinion.OpinionId}", TestData.Admin);

            ExamineRedirect("/Admin/HiddenOpinions");
        }

        [TestMethod]
        public async Task OpinionIdIsValid_ShowsOpinion() {

            var opinion = TestEntity.Random<Opinion>()
                .WithIsHidden(true);

            await PostAsync($"/Opinion/Show/{opinion.OpinionId}", TestData.Admin);

            Assert.AreEqual(false, opinion.IsHidden);
            TestDocumentApprover.ExamineSaveChanges();
        }

        [TestMethod]
        public async Task InvalidOpinionId_ThrowsMissingEntityException() {

            await PostAsync($"/Opinion/Show/{Guid.NewGuid()}", TestData.Admin);

            ExamineExceptionThrown<MissingEntityException>();
        }
    }
}
