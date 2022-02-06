using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [Authorize]
    public class BasketController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IBasketProductsProvider BasketProductsProvider { get; }

        IBasketProductCreator BasketProductCreator { get; }

        IBasketProductRemover BasketProductRemover { get; }

        public BasketController(
            UserManager<IdentityUser> userManager,
            IBasketProductsProvider basketProductsProvider,
            IBasketProductCreator basketProductCreator,
            IBasketProductRemover basketProductRemover) {

            UserManager = userManager;
            BasketProductsProvider = basketProductsProvider;
            BasketProductCreator = basketProductCreator;
            BasketProductRemover = basketProductRemover;
        }

        public IActionResult Index() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(BasketProductsProvider.Load(userId));
        }

        [HttpPost]
        public IActionResult Add(string productId, int quantity, string returnUrl = null) {

            var userId = UserManager.GetUserId(HttpContext.User);

            BasketProductCreator.Create(userId, productId, quantity);

            return LocalRedirect(returnUrl ?? "/Basket");
        }

        [HttpPost]
        public IActionResult Remove(string productId) {

            var userId = UserManager.GetUserId(HttpContext.User);

            BasketProductRemover.Remove(userId, productId);

            return RedirectToAction("Index");
        }
    }
}
