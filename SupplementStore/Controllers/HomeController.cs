using Microsoft.AspNetCore.Mvc;

namespace SupplementStore.Controllers {

    public class HomeController : Controller {

        public IActionResult Index() {

            return View();
        }
    }
}
