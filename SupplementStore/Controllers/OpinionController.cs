using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Opinion;

namespace SupplementStore.Controllers {

    [Authorize]
    public class OpinionController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IProductToOpineProvider ProductToOpineProvider { get; }

        IOpinionsProvider OpinionsProvider { get; }

        IOpinionCreator OpinionCreator { get; }

        public OpinionController(
            UserManager<IdentityUser> userManager,
            IProductToOpineProvider productToOpineProvider,
            IOpinionsProvider opinionsProvider,
            IOpinionCreator opinionCreator) {

            UserManager = userManager;
            ProductToOpineProvider = productToOpineProvider;
            OpinionsProvider = opinionsProvider;
            OpinionCreator = opinionCreator;
        }

        public IActionResult Index() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(new OpinionIndexViewModel {
                IsProductToOpineWaiting = ProductToOpineProvider.Load(userId).IsEmpty == false,
                Opinions = OpinionsProvider.Load(userId)
            });
        }

        public IActionResult Create() {

            var userId = UserManager.GetUserId(HttpContext.User);

            var productToOpine = ProductToOpineProvider.Load(userId);

            if (productToOpine.IsEmpty)
                return RedirectToAction("Index");

            return View(new OpinionCreateViewModel {
                OrderProductId = productToOpine.OrderProductId,
                ProductName = productToOpine.ProductName,
                BuyingDate = productToOpine.BuyingDate
            });
        }

        [HttpPost]
        public IActionResult Create(OpinionCreateViewModel model) {

            OpinionCreator.Create(new OpinionCreatorArgs {
                OrderProductId = model.OrderProductId,
                Text = model.Text,
                Stars = model.Stars
            });

            return RedirectToAction(nameof(Create));
        }
    }
}
