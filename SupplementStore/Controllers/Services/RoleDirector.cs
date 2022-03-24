using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Controllers.Services {

    public class RoleDirector {

        UserManager<IdentityUser> UserManager { get; }

        OwnerManager OwnerManager { get; }

        AdminManager AdminManager { get; }

        public RoleDirector(
            UserManager<IdentityUser> userManager,
            OwnerManager ownerManager,
            AdminManager adminManager) {

            UserManager = userManager;
            OwnerManager = ownerManager;
            AdminManager = adminManager;
        }

        public async Task ManageAsync(string userId, IReadOnlyDictionary<string, bool> roles) {

            var user = await UserManager.FindByIdAsync(userId);

            await ManageOwner(user, roles["Owner"]);
            await ManageAdmin(user, roles["Admin"]);
        }

        private async Task ManageOwner(IdentityUser user, bool shouldAssign) {

            if (shouldAssign) {

                await OwnerManager.AddAsync(user);
            }
            else {

                await OwnerManager.RemoveAsync(user);
            }
        }

        private async Task ManageAdmin(IdentityUser user, bool shouldAssign) {

            if (shouldAssign) {

                await AdminManager.AddAsync(user);
            }
            else {

                await AdminManager.RemoveAsync(user);
            }
        }
    }
}
