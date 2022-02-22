using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.ProductTests {

    [TestClass]
    public class CreateTests : IntegrationTestsBase {

        [TestMethod]
        public async Task Get_UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Product/Create");

            ExamineAuthRedirect("/Product/Create");
        }

        [TestMethod]
        public async Task Get_UserIsNotAuthorized_AccessDenied() {

            await GetAsync("/Product/Create", TestData.User);

            ExamineAccessDeniedRedirect("/Product/Create");
        }
    }
}
