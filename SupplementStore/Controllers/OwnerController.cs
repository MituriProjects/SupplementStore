using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    public class OwnerController : AppControllerBase {

        UserManager<IdentityUser> UserManager { get; }

        RoleManager<IdentityRole> RoleManager { get; }

        public OwnerController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager) {

            UserManager = userManager;
            RoleManager = roleManager;
        }

        private readonly string OwnerRoleName = "Owner";

        [Authorize(Roles = "Owner")]
        public IActionResult Index() {

            return View();
        }

        public async Task<IActionResult> Assign(string email) {

            await CreateOwnerRole();
            await DesignateUserToOwnerRole(email);

            return LocalRedirect("/");
        }

        private async Task CreateOwnerRole() {

            var ownerRole = await RoleManager.FindByNameAsync(OwnerRoleName);

            if (ownerRole == null) {

                await RoleManager.CreateAsync(new IdentityRole(OwnerRoleName));
            }
        }

        private async Task DesignateUserToOwnerRole(string email) {

            var owners = await UserManager.GetUsersInRoleAsync(OwnerRoleName);

            if (owners.Count > 0)
                return;

            var user = await UserManager.FindByEmailAsync(email);

            if (user == null)
                return;

            await UserManager.AddToRoleAsync(user, OwnerRoleName);
        }
    }
}
