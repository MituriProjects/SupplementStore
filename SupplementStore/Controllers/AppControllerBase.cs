using Microsoft.AspNetCore.Mvc;

namespace SupplementStore.Controllers {

    public abstract class AppControllerBase : Controller {

        protected bool IsModelValid =>
            ModelState.IsValid;

        protected bool IsModelInvalid =>
            ModelState.IsValid == false;

        protected void AddModelError(string key, string message) =>
            ModelState.AddModelError(key, message);

        protected void SetSuccessMessage(string message) =>
            TempData["SuccessMessage"] = message;

        protected void SetFailureMessage(string message) =>
            TempData["FailureMessage"] = message;

        protected RedirectToActionResult RedirectToAction<TController>(string actionName) {

            var controllerName = typeof(TController).Name;

            return RedirectToAction(
                actionName,
                controllerName.Substring(0, controllerName.IndexOf("Controller")));
        }

        protected RedirectToActionResult RedirectToAction<TController>(string actionName, object routeValues) {

            var controllerName = typeof(TController).Name;

            return RedirectToAction(
                actionName,
                controllerName.Substring(0, controllerName.IndexOf("Controller")),
                routeValues);
        }
    }
}
