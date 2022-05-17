using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Opinion;

namespace SupplementStore.Controllers {

    [Authorize]
    public class OpinionController : AppControllerBase {

        UserManager<IdentityUser> UserManager { get; }

        IOpinionService OpinionService { get; }

        public OpinionController(
            UserManager<IdentityUser> userManager,
            IOpinionService opinionService) {

            UserManager = userManager;
            OpinionService = opinionService;
        }

        public IActionResult Index() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(new IndexVM {
                IsProductToOpineWaiting = OpinionService.LoadProductToOpine(userId).IsEmpty == false,
                Opinions = OpinionService.LoadMany(userId)
            });
        }

        public IActionResult Create() {

            var userId = UserManager.GetUserId(HttpContext.User);

            var productToOpine = OpinionService.LoadProductToOpine(userId);

            if (productToOpine.IsEmpty)
                return RedirectToAction(nameof(Index));

            return View(new CreateVM {
                PurchaseId = productToOpine.PurchaseId,
                ProductName = productToOpine.ProductName,
                BuyingDate = productToOpine.BuyingDate
            });
        }

        [HttpPost]
        public IActionResult Create(CreateVM model) {

            OpinionService.Create(new OpinionCreateArgs {
                PurchaseId = model.PurchaseId,
                Text = model.Text,
                Stars = model.Stars
            });

            return RedirectToAction(nameof(Create));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id) {

            var opinion = OpinionService.Load(id);

            return View(new EditVM {
                Id = opinion.Id,
                Text = opinion.Text
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(EditVM model) {

            OpinionService.UpdateText(model.Id, model.Text);

            var product = OpinionService.LoadOpinionProduct(model.Id);

            return RedirectToAction<ProductController>(nameof(ProductController.Details), new { product.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Hide(string id) {

            OpinionService.Hide(id);

            var product = OpinionService.LoadOpinionProduct(id);

            return RedirectToAction<ProductController>(nameof(ProductController.Details), new { product.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Show(string id) {

            OpinionService.Reveal(id);

            return RedirectToAction<AdminController>(nameof(AdminController.HiddenOpinions));
        }
    }
}
