using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Support {

    public class SendMessageVM {

        [Required(ErrorMessage = "TextRequiredErrorMessage")]
        [Label]
        public string Text { get; set; }

        [Required(ErrorMessage = "EmailRequiredErrorMessage")]
        [Email]
        [Label]
        public string Email { get; set; }
    }
}
