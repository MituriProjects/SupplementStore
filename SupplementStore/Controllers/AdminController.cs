using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {

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

        public async Task<IActionResult> Orders() {

            var orderDetails = OrderService.LoadMany();

            Dictionary<string, string> UserEmails = new Dictionary<string, string>();

            foreach (var userId in orderDetails.Select(e => e.UserId).Distinct()) {

                UserEmails[userId] = (await UserManager.FindByIdAsync(userId)).Email;
            }

            return View(new OrdersVM {
                OrderDetails = orderDetails,
                UserEmails = UserEmails
            });
        }

        public IActionResult HiddenOpinions() {

            return View(OpinionService.LoadHidden());
        }
    }
}
