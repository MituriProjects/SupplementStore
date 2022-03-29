using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupplementStore.Controllers.Services {

    public class AdminManager {

        UserManager<IdentityUser> UserManager { get; }

        Lazy<OwnerManager> OwnerManager { get; }

        public AdminManager(
            UserManager<IdentityUser> userManager,
            Lazy<OwnerManager> ownerManager) {

            UserManager = userManager;
            OwnerManager = ownerManager;
        }

        static List<IdentityUser> RecentlyAdded { get; } = new List<IdentityUser>();

        public async Task<bool> IsInRoleAsync(IdentityUser user) {

            return await UserManager.IsInRoleAsync(user, "Admin");
        }

        public async Task AddAsync(IdentityUser user) {

            await UserManager.AddToRoleAsync(user, "Admin");

            RecentlyAdded.Add(user);
        }

        public async Task RemoveAsync(IdentityUser user) {

            if (RecentlyAdded.Contains(user))
                return;

            await OwnerManager.Value.RemoveAsync(user);

            if (await OwnerManager.Value.IsInRoleAsync(user))
                return;

            await UserManager.RemoveFromRoleAsync(user, "Admin");
        }
    }
}
