using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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

        protected void SetResultMessage(bool result, [CallerMemberName]string callerName = null) {

            if (result)
                SetSuccessMessage($"{callerName}Success");
            else
                SetFailureMessage($"{callerName}Failure");
        }

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
