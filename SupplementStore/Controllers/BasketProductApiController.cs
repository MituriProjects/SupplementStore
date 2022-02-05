using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Models;
using SupplementStore.Application.Services;

namespace SupplementStore.Controllers {

    [Route("api/basketproduct")]
    public class BasketProductApiController : Controller {

        IBasketProductProvider BasketProductProvider { get; }

        public BasketProductApiController(
            IBasketProductProvider basketProductProvider) {

            BasketProductProvider = basketProductProvider;
        }

        [HttpGet("{id}")]
        public BasketProductDetails Get(string id) {

            return BasketProductProvider.Load(id);
        }
    }
}
