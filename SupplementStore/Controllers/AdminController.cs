using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    [Authorize(Roles = "Admin")]
    public class AdminController : AppControllerBase {

        IOrderService OrderService { get; }

        IOpinionService OpinionService { get; }

        UserManager<IdentityUser> UserManager { get; }

        public AdminController(
            IOrderService orderService,
            IOpinionService opinionService,
            UserManager<IdentityUser> userManager) {

            OrderService = orderService;
            OpinionService = opinionService;
            UserManager = userManager;
        }

        public IActionResult Index() {

            return View();
        }

        public async Task<IActionResult> Orders(OrdersVM model) {

            model = model ?? new OrdersVM();

            var orderListResult = OrderService.LoadMany(new OrderListArgs {
                Skip = model.Page.Skip,
                Take = model.Page.Take
            });

            model.Page.Count = orderListResult.AllOrdersCount;
            model.OrderDetails = orderListResult.Orders;

            model.UserEmails.Clear();

            foreach (var userId in orderListResult.Orders.Select(e => e.UserId).Distinct()) {

                model.UserEmails[userId] = (await UserManager.FindByIdAsync(userId)).Email;
            }

            return View(model);
        }

        public IActionResult HiddenOpinions(HiddenOpinionsVM model) {

            model = model ?? new HiddenOpinionsVM();

            var hiddenOpinionsListResult = OpinionService.LoadHidden(new HiddenOpinionListArgs {
                Skip = model.Page.Skip,
                Take = model.Page.Take
            });

            model.Page.Count = hiddenOpinionsListResult.AllHiddenOpinionsCount;
            model.HiddenOpinions = hiddenOpinionsListResult.HiddenOpinions;

            return View(model);
        }
    }
}
