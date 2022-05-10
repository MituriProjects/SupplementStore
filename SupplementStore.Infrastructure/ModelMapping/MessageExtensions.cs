using SupplementStore.Application.Models;
using SupplementStore.Domain.Messages;
using SupplementStore.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SupplementStore.Infrastructure.ModelMapping {

    static class MessageExtensions {

        public static MessageDetails ToDetails(this Message message, UserRepository userRepository) {

            return new MessageDetails {
                Email = message.SenderEmail ?? userRepository.FindBy(message.UserId).Email,
                Text = message.Text,
                CreatedOn = message.CreatedOn
            };
        }

        public static IEnumerable<MessageDetails> ToDetails(this IEnumerable<Message> messages, UserRepository userRepository) {

            return messages
                .Select(e => e.ToDetails(userRepository))
                .ToList();
        }
    }
}
