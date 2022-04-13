using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [Authorize]
    public class WishController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IWishService WishService { get; }

        public WishController(
            UserManager<IdentityUser> userManager,
            IWishService wishService) {

            UserManager = userManager;
            WishService = wishService;
        }

        public IActionResult Index() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(WishService.LoadMany(userId));
        }
    }
}
