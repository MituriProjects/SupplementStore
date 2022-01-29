using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.ViewModels.Account;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    public class AccountController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        public AccountController(UserManager<IdentityUser> userManager) {

            UserManager = userManager;
        }

        public IActionResult Register(string returnUrl) {

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl) {

            if (ModelState.IsValid) {

                IdentityUser user = new IdentityUser {
                    UserName = model.Email,
                    Email = model.Email
                };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {

                    return LocalRedirect(returnUrl ?? "/");
                }
                else {

                    foreach (IdentityError error in result.Errors) {

                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}
