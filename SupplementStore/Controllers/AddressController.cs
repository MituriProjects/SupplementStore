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

        public IActionResult Edit(string id) {

            var address = AddressService.Load(id);

            if (address == null)
                return RedirectToAction(nameof(Index));

            return View(new EditVM {
                Id = address.Id,
                Street = address.Street,
                PostalCode = address.PostalCode,
                City = address.City
            });
        }

        [HttpPost]
        public IActionResult Edit(EditVM model) {

            if (ModelState.IsValid == false)
                return View(model);

            var userId = UserManager.GetUserId(HttpContext.User);

            AddressService.Update(new AddressUpdateArgs {
                Id = model.Id,
                UserId = userId,
                Street = model.Street,
                PostalCode = model.PostalCode,
                City = model.City
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string id) {

            var userId = UserManager.GetUserId(HttpContext.User);

            AddressService.Hide(userId, id);

            return RedirectToAction(nameof(Index));
        }
    }
}
