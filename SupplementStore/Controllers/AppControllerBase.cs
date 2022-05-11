using Microsoft.AspNetCore.Mvc;

namespace SupplementStore.Controllers {

    public abstract class AppControllerBase : Controller {

        protected bool IsModelValid =>
            ModelState.IsValid;

        protected bool IsModelInvalid =>
            ModelState.IsValid == false;
    }
}
