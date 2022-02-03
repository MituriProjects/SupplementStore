using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [Authorize]
    public class BasketController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IBasketProductCreator BasketProductCreator { get; }

        public BasketController(
            UserManager<IdentityUser> userManager,
            IBasketProductCreator basketProductCreator) {

            UserManager = userManager;
            BasketProductCreator = basketProductCreator;
        }

        [HttpPost]
        public IActionResult Add(string productId, int quantity, string returnUrl = null) {

            var userId = UserManager.GetUserId(HttpContext.User);

            BasketProductCreator.Create(userId, productId, quantity);

            return LocalRedirect(returnUrl ?? "/Basket");
        }
    }
}
