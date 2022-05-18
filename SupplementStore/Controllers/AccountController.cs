using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Localization;
using SupplementStore.Controllers.Filters;
using SupplementStore.ViewModels.Account;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    [ViewComponent(Name = "Account")]
    public class AccountController : AppControllerBase {

        UserManager<IdentityUser> UserManager { get; }

        SignInManager<IdentityUser> SignInManager { get; }

        IStringLocalizer<AccountController> Localizer { get; }

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IStringLocalizer<AccountController> stringLocalizer) {

            UserManager = userManager;
            SignInManager = signInManager;
            Localizer = stringLocalizer;
        }

        public IViewComponentResult Invoke() {

            return new ViewViewComponentResult();
        }

        [Authorize]
        public IActionResult Index() {

            return View();
        }

        [PreserveReturnUrl]
        public IActionResult Login(string returnUrl) {

            return View();
        }

        [HttpPost]
        [PreserveReturnUrl]
        public async Task<IActionResult> Login(LoginVM details, string returnUrl) {

            if (IsModelValid) {

                IdentityUser user = await UserManager.FindByEmailAsync(details.Email);

                if (user != null) {

                    await SignInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, details.Password, false, false);

                    if (result.Succeeded) {

                        return LocalRedirect(returnUrl ?? "/");
                    }
                }

                AddModelError(nameof(LoginVM.Email), Localizer["LoginAttemptErrorMessage"]);
            }

            return View(details);
        }

        [PreserveReturnUrl]
        public IActionResult Register(string returnUrl) {

            return View();
        }

        [HttpPost]
        [PreserveReturnUrl]
        public async Task<IActionResult> Register(RegisterVM model, string returnUrl) {

            if (IsModelValid) {

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

                        AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl) {

            await SignInManager.SignOutAsync();

            return LocalRedirect(returnUrl ?? "/");
        }
    }
}
