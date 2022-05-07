using Microsoft.AspNetCore.Identity;
using SupplementStore.Domain.Messages;

namespace SupplementStore.Tests {

    static class MessageExtensions {

        public static Message WithUserId(this Message message, IdentityUser user) {

            message.UserId = user.Id;

            return message;
        }
    }
}
