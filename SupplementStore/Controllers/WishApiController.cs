using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [ApiController]
    [Route("api/wish")]
    public class WishApiController : ControllerBase {

        UserManager<IdentityUser> UserManager { get; }

        IWishProvider WishProvider { get; }

        IWishCreator WishCreator { get; }

        IWishRemover WishRemover { get; }

        public WishApiController(
            UserManager<IdentityUser> userManager,
            IWishProvider wishProvider,
            IWishCreator wishCreator,
            IWishRemover wishRemover) {

            UserManager = userManager;
            WishProvider = wishProvider;
            WishCreator = wishCreator;
            WishRemover = wishRemover;
        }

        [HttpGet("{id}")]
        public bool Get(string id) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (userId == null)
                return false;

            return WishProvider.Load(userId, id);
        }

        [HttpPost("{id}")]
        public void Post(string id) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (userId == null)
                return;

            WishCreator.Create(userId, id);
        }

        [HttpDelete("{id}")]
        public void Delete(string id) {

            var userId = UserManager.GetUserId(HttpContext.User);

            if (userId == null)
                return;

            WishRemover.Remove(userId, id);
        }
    }
}
