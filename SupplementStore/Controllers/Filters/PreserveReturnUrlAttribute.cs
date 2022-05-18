using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SupplementStore.Controllers.Filters {

    public class PreserveReturnUrlAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {

            if (context.ActionArguments.ContainsKey("returnUrl")
                && context.ActionArguments["returnUrl"] is string returnUrl
                && context.Controller is Controller controller) {

                controller.ViewBag.ReturnUrl = returnUrl;
            }
        }
    }
}
