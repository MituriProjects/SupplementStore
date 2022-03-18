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

        IOpinionProductProvider OpinionProductProvider { get; }

        IOpinionProvider OpinionProvider { get; }

        IOpinionsProvider OpinionsProvider { get; }

        IOpinionCreator OpinionCreator { get; }

        IOpinionTextUpdater OpinionTextUpdater { get; }

        IOpinionHider OpinionHider { get; }

        public OpinionController(
            UserManager<IdentityUser> userManager,
            IProductToOpineProvider productToOpineProvider,
            IOpinionProductProvider opinionProductProvider,
            IOpinionProvider opinionProvider,
            IOpinionsProvider opinionsProvider,
            IOpinionCreator opinionCreator,
            IOpinionTextUpdater opinionTextUpdater,
            IOpinionHider opinionHider) {

            UserManager = userManager;
            ProductToOpineProvider = productToOpineProvider;
            OpinionProductProvider = opinionProductProvider;
            OpinionProvider = opinionProvider;
            OpinionsProvider = opinionsProvider;
            OpinionCreator = opinionCreator;
            OpinionTextUpdater = opinionTextUpdater;
            OpinionHider = opinionHider;
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

        [Authorize(Roles = "Owner, Admin")]
        public IActionResult Edit(string id) {

            var opinion = OpinionProvider.Load(id);

            return View(new OpinionEditViewModel {
                Id = opinion.Id,
                Text = opinion.Text
            });
        }

        [HttpPost]
        [Authorize(Roles = "Owner, Admin")]
        public IActionResult Edit(OpinionEditViewModel model) {

            OpinionTextUpdater.Update(model.Id, model.Text);

            var product = OpinionProductProvider.Load(model.Id);

            return RedirectToAction("Details", "Product", new { product.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Owner, Admin")]
        public IActionResult Hide(string id) {

            OpinionHider.Hide(id);

            var product = OpinionProductProvider.Load(id);

            return RedirectToAction("Details", "Product", new { product.Id });
        }
    }
}
