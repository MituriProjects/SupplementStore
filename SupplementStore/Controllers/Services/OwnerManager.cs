using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace SupplementStore.Controllers.Services {

    public class OwnerManager {

        UserManager<IdentityUser> UserManager { get; }

        Lazy<AdminManager> AdminManager { get; }

        public OwnerManager(
            UserManager<IdentityUser> userManager,
            Lazy<AdminManager> adminManager) {

            UserManager = userManager;
            AdminManager = adminManager;
        }

        public async Task<bool> IsInRoleAsync(IdentityUser user) {

            return await UserManager.IsInRoleAsync(user, "Owner");
        }

        public async Task AddAsync(IdentityUser user) {

            await AdminManager.Value.AddAsync(user);

            if (await AdminManager.Value.IsInRoleAsync(user)) {

                await UserManager.AddToRoleAsync(user, "Owner");
            }
        }

        public async Task RemoveAsync(IdentityUser user) {

            var ownerUsers = await UserManager.GetUsersInRoleAsync("Owner");

            if (ownerUsers.Count > 1) {

                await UserManager.RemoveFromRoleAsync(user, "Owner");
            }
        }
    }
}
