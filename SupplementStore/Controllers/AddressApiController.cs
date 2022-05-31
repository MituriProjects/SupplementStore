using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;
using System.Collections.Generic;

namespace SupplementStore.Controllers {

    [ApiController]
    [Route("api/address")]
    public class AddressApiController : ControllerBase {

        UserManager<IdentityUser> UserManager { get; }

        IAddressService AddressService { get; }

        public AddressApiController(
            UserManager<IdentityUser> userManager, 
            IAddressService addressService) {

            UserManager = userManager;
            AddressService = addressService;
        }

        [HttpGet]
        public IEnumerable<AddressDetails> Get() {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (userId == null)
                return new AddressDetails[] { };

            return AddressService.LoadMany(userId);
        }
    }
}
