using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SupplementStore.Controllers {

    [Authorize(Roles = "Owner, Admin")]
    public class AdminController : Controller {

        public IActionResult Index() {

            return View();
        }
    }
}
