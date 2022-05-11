using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Order;

namespace SupplementStore.Controllers {

    [Authorize]
    public class OrderController : AppControllerBase {

        UserManager<IdentityUser> UserManager { get; }

        IBasketProductService BasketProductService { get; }

        IOrderService OrderService { get; }

        public OrderController(
            UserManager<IdentityUser> userManager,
            IBasketProductService basketProductService,
            IOrderService orderService) {

            UserManager = userManager;
            BasketProductService = basketProductService;
            OrderService = orderService;
        }

        public IActionResult Create() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(new OrderCreateVM {
                BasketProducts = BasketProductService.LoadMany(userId)
            });
        }

        [HttpPost]
        public IActionResult Create(OrderCreateVM model) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (IsModelInvalid) {

                model.BasketProducts = BasketProductService.LoadMany(userId);

                return View(model);
            }

            var order = OrderService.Create(new OrderCreateArgs {
                UserId = userId,
                Address = model.Address,
                PostalCode = model.PostalCode,
                City = model.City,
                ShouldAddressBeHidden = !model.IsAddressToBeSaved
            });

            if (order == null) {

                AddModelError(string.Empty, "The creation of an order failed.");

                model.BasketProducts = BasketProductService.LoadMany(userId);

                return View(model);
            }

            return LocalRedirect($"/Order/Summary/{order.Id.ToString()}");
        }

        public IActionResult Summary(string id) {

            var orderDetails = OrderService.Load(id);

            var userId = UserManager.GetUserId(HttpContext.User);

            if (orderDetails == null)
                return LocalRedirect("/");

            if (orderDetails.UserId != userId)
                return LocalRedirect("/");

            return View(orderDetails);
        }
    }
}
