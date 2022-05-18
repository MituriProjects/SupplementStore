using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace SupplementStore.Controllers.Filters {

    public class ReturnToViewOnModelInvalidAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {

            if (context.Controller is Controller controller
                && controller.ModelState.IsValid == false) {

                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                context.Result = controller.View(descriptor.ActionName, context.ActionArguments.First().Value);
            }
        }
    }
}
