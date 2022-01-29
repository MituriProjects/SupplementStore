using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.ViewModels.Account;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    public class AccountController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) {

            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IActionResult Login(string returnUrl) {

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel details, string returnUrl) {

            if (ModelState.IsValid) {

                IdentityUser user = await UserManager.FindByEmailAsync(details.Email);

                if (user != null) {

                    await SignInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, details.Password, false, false);

                    if (result.Succeeded) {

                        return LocalRedirect(returnUrl ?? "/");
                    }
                }

                ModelState.AddModelError(nameof(LoginViewModel.Email), "Invalid user or password");
            }

            return View(details);
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
