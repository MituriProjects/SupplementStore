using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplementStore.Application.Args;
using SupplementStore.Application.Services;
using SupplementStore.Controllers.Filters;
using SupplementStore.ViewModels.Support;

namespace SupplementStore.Controllers {

    public class SupportController : AppControllerBase {

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
        [ReturnToViewOnModelInvalid]
        public IActionResult SendMessage(SendMessageVM model) {

            string userId = UserManager.GetUserId(HttpContext.User)
                ?? UserManager.FindByEmailAsync(model.Email).Result?.Id;

            var result = MessageService.Create(new MessageCreateArgs {
                Text = model.Text,
                UserId = userId,
                SenderEmail = model.Email
            });

            SetResultMessage(result.Success);

            return RedirectToAction(nameof(SendMessage));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult MessageList(MessageListVM model) {

            model = model ?? new MessageListVM();

            var messageListResult = MessageService.LoadMany(new MessageListArgs {
                Skip = model.Page.Skip,
                Take = model.Page.Take
            });

            model.Page.Count = messageListResult.AllMessagesCount;
            model.Messages = messageListResult.Messages;

            return View(model);
        }
    }
}
