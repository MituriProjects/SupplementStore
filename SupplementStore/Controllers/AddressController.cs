using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Address;

namespace SupplementStore.Controllers {

    [Authorize]
    public class AddressController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IAddressService AddressService { get; }

        public AddressController(
            UserManager<IdentityUser> userManager,
            IAddressService addressService) {

            UserManager = userManager;
            AddressService = addressService;
        }

        public IActionResult Index() {

            var userId = UserManager.GetUserId(HttpContext.User);

            return View(AddressService.LoadMany(userId));
        }

        public IActionResult Create() {

            return View(new CreateVM());
        }

        [HttpPost]
        public IActionResult Create(CreateVM model) {

            if (ModelState.IsValid == false) {

                return View(model);
            }

            var userId = UserManager.GetUserId(HttpContext.User);

            AddressService.Create(new AddressCreateArgs {
                UserId = userId,
                Street = model.Street,
                PostalCode = model.PostalCode,
                City = model.City
            });

            return RedirectToAction("Index");
        }
    }
}
