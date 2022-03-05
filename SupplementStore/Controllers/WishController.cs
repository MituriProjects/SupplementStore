using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [Authorize]
    public class WishController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IWishesProvider WishesProvider { get; }

        public WishController(
            UserManager<IdentityUser> userManager,
            IWishesProvider wishesProvider) {

            UserManager = userManager;
            WishesProvider = wishesProvider;
        }

        public IActionResult Index() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(WishesProvider.Load(userId));
        }
    }
}
