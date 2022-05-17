namespace SupplementStore.ViewModels.Support {

    public class SendMessageVM {

        [IsRequired]
        [Label]
        public string Text { get; set; }

        [IsRequired]
        [Email]
        [Label]
        public string Email { get; set; }
    }
}
