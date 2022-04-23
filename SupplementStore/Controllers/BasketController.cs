using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [Authorize]
    public class BasketController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IBasketProductService BasketProductService { get; }

        public BasketController(
            UserManager<IdentityUser> userManager,
            IBasketProductService basketProductService) {

            UserManager = userManager;
            BasketProductService = basketProductService;
        }

        public IActionResult Index() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(BasketProductService.LoadMany(userId));
        }

        [HttpPost]
        public IActionResult Add(string productId, int quantity, string returnUrl = null) {

            var userId = UserManager.GetUserId(HttpContext.User);

            BasketProductService.Create(userId, productId, quantity);

            return LocalRedirect(returnUrl ?? "/Basket");
        }

        [HttpPost]
        public IActionResult Remove(string productId) {

            var userId = UserManager.GetUserId(HttpContext.User);

            BasketProductService.Remove(userId, productId);

            return RedirectToAction(nameof(Index));
        }
    }
}
