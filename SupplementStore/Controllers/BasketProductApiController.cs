using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [ApiController]
    [Route("api/basketproduct")]
    public class BasketProductApiController : ControllerBase {

        IBasketProductService BasketProductService { get; }

        public BasketProductApiController(IBasketProductService basketProductService) {

            BasketProductService = basketProductService;
        }

        [HttpGet("{id}")]
        public BasketProductDetails Get(string id) {

            return BasketProductService.Load(id);
        }

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(string id, [FromBody]JsonPatchDocument<BasketProductDetails> patchDocument) {

            var basketProduct = Get(id);

            if (basketProduct == null)
                return NotFound();

            patchDocument.ApplyTo(basketProduct);

            BasketProductService.Update(basketProduct);

            return Ok();
        }
    }
}
