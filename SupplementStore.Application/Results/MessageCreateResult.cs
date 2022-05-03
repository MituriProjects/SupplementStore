namespace SupplementStore.Application.Results {

    public class MessageCreateResult {

        public bool Success { get; set; }

        public static MessageCreateResult Succeeded = new MessageCreateResult { Success = true };

        public static MessageCreateResult Failed = new MessageCreateResult { Success = false };
    }
}
