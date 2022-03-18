using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Controllers {

    [Authorize(Roles = "Owner, Admin")]
    public class AdminController : Controller {

        IOrdersProvider OrdersProvider { get; }

        IHiddenOpinionsProvider HiddenOpinionsProvider { get; }

        UserManager<IdentityUser> UserManager { get; }

        public AdminController(
            IOrdersProvider ordersProvider,
            IHiddenOpinionsProvider hiddenOpinionsProvider,
            UserManager<IdentityUser> userManager) {

            OrdersProvider = ordersProvider;
            HiddenOpinionsProvider = hiddenOpinionsProvider;
            UserManager = userManager;
        }

        public IActionResult Index() {

            return View();
        }

        public async Task<IActionResult> Orders() {

            var orderDetails = OrdersProvider.Load();

            Dictionary<string, string> UserEmails = new Dictionary<string, string>();

            foreach (var userId in orderDetails.Select(e => e.UserId).Distinct()) {

                UserEmails[userId] = (await UserManager.FindByIdAsync(userId)).Email;
            }

            return View(new OrdersViewModel {
                OrderDetails = orderDetails,
                UserEmails = UserEmails
            });
        }

        public IActionResult HiddenOpinions() {

            return View(HiddenOpinionsProvider.Load());
        }
    }
}
