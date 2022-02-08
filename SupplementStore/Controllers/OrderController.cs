using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Order;

namespace SupplementStore.Controllers {

    [Authorize]
    public class OrderController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IBasketProductsProvider BasketProductsProvider { get; }

        IOrderCreator OrderCreator { get; }

        public OrderController(
            UserManager<IdentityUser> userManager,
            IBasketProductsProvider basketProductsProvider,
            IOrderCreator orderCreator) {

            UserManager = userManager;
            BasketProductsProvider = basketProductsProvider;
            OrderCreator = orderCreator;
        }

        public IActionResult Create() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(new OrderCreateViewModel {
                BasketProducts = BasketProductsProvider.Load(userId)
            });
        }

        [HttpPost]
        public IActionResult Create(OrderCreateViewModel model) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid == false) {

                model.BasketProducts = BasketProductsProvider.Load(userId);

                return View(model);
            }

            var order = OrderCreator.Create(new OrderCreatorArgs {
                UserId = userId,
                Address = model.Address,
                PostalCode = model.PostalCode,
                City = model.City
            });

            if (order == null) {

                ModelState.AddModelError(string.Empty, "The creation of an order failed.");

                model.BasketProducts = BasketProductsProvider.Load(userId);

                return View(model);
            }

            return LocalRedirect($"/Order/Summary/{order.Id.ToString()}");
        }
    }
}
