using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.ViewModels.Support;

namespace SupplementStore.Controllers {

    public class SupportController : Controller {

        UserManager<IdentityUser> UserManager { get; }

        IMessageService MessageService { get; }

        public SupportController(
            UserManager<IdentityUser> userManager,
            IMessageService messageService) {

            UserManager = userManager;
            MessageService = messageService;
        }

        public IActionResult SendMessage() {

            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(SendMessageVM model) {

            if (ModelState.IsValid == false)
                return View(model);

            string userId = UserManager.GetUserId(HttpContext.User)
                ?? UserManager.FindByEmailAsync(model.Email).Result?.Id;

            var result = MessageService.Create(new MessageCreateArgs {
                Text = model.Text,
                UserId = userId,
                SenderEmail = model.Email
            });

            return RedirectToAction(nameof(SendMessage));
        }
    }
}
