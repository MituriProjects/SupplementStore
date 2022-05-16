namespace SupplementStore.ViewModels.Support {

    public class SendMessageVM {

        [IsRequired(typeof(SendMessageVM))]
        [Label]
        public string Text { get; set; }

        [IsRequired]
        [Email]
        [Label]
        public string Email { get; set; }
    }
}
