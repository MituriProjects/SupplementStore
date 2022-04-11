using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [ApiController]
    [Route("api/wish")]
    public class WishApiController : ControllerBase {

        UserManager<IdentityUser> UserManager { get; }

        IWishService WishService { get; }

        public WishApiController(
            UserManager<IdentityUser> userManager,
            IWishService wishService) {

            UserManager = userManager;
            WishService = wishService;
        }

        [HttpGet("{id}")]
        public bool Get(string id) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (userId == null)
                return false;

            return WishService.IsOnWishList(userId, id);
        }

        [HttpPost("{id}")]
        public void Post(string id) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (userId == null)
                return;

            WishService.Create(userId, id);
        }

        [HttpDelete("{id}")]
        public void Delete(string id) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (userId == null)
                return;

            WishService.Remove(userId, id);
        }
    }
}
