using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.ViewModels.Role;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    [Authorize(Roles = "Owner")]
    public class RoleController : Controller {

        RoleManager<IdentityRole> RoleManager { get; }

        UserManager<IdentityUser> UserManager { get; }

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager) {

            RoleManager = roleManager;
            UserManager = userManager;
        }

        public IActionResult Index() {

            return View(RoleManager.Roles.Select(r => r.Name));
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName) {

            if (roleName == null)
                return RedirectToAction("Index");

            await RoleManager.CreateAsync(new IdentityRole(roleName));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Users() {

            var roles = RoleManager.Roles.Select(r => r.Name);

            var userRolesViewModel = UserManager.Users.Select(u => new UserRolesViewModel {
                UserId = u.Id,
                UserName = u.UserName,
                UserRoles = new Dictionary<string, bool>(roles.Select(r => KeyValuePair.Create(r, false)))
            }).ToList();

            foreach (var user in UserManager.Users) {

                var userRoles = await UserManager.GetRolesAsync(user);

                foreach (var role in userRoles) {

                    userRolesViewModel
                        .First(ur => ur.UserId == user.Id)
                        .UserRoles[role] = true;
                }
            }

            return View(userRolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoles(UserRolesViewModel userRolesViewModel) {

            var user = UserManager.Users.SingleOrDefault(u => u.Id == userRolesViewModel.UserId);

            foreach (var role in userRolesViewModel.UserRoles) {

                if (role.Value) {

                    await UserManager.AddToRoleAsync(user, role.Key);
                }
                else {

                    if (role.Key == "Owner") {

                        var ownerUsers = await UserManager.GetUsersInRoleAsync(role.Key);

                        if (ownerUsers.Count > 1) {

                            await UserManager.RemoveFromRoleAsync(user, role.Key);
                        }
                    }
                    else {

                        await UserManager.RemoveFromRoleAsync(user, role.Key);
                    }
                }
            }

            return RedirectToAction(nameof(Users));
        }
    }
}
