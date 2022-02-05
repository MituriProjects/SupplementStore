using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [Route("api/basketproduct")]
    public class BasketProductApiController : Controller {

        IBasketProductProvider BasketProductProvider { get; }

        IBasketProductUpdater BasketProductUpdater { get; }

        public BasketProductApiController(
            IBasketProductProvider basketProductProvider,
            IBasketProductUpdater basketProductUpdater) {

            BasketProductProvider = basketProductProvider;
            BasketProductUpdater = basketProductUpdater;
        }

        [HttpGet("{id}")]
        public BasketProductDetails Get(string id) {

            return BasketProductProvider.Load(id);
        }

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(string id, [FromBody]JsonPatchDocument<BasketProductDetails> patchDocument) {

            var basketProduct = Get(id);

            if (basketProduct == null)
                return NotFound();

            patchDocument.ApplyTo(basketProduct);

            BasketProductUpdater.Update(basketProduct);

            return Ok();
        }
    }
}
