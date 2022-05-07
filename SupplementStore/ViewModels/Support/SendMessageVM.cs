using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Support {

    public class SendMessageVM {

        [Required(ErrorMessage = "TextRequiredErrorMessage")]
        [Display(Name = "TextLabel")]
        public string Text { get; set; }

        [Required(ErrorMessage = "EmailRequiredErrorMessage")]
        [Email]
        [Display(Name = "EmailLabel")]
        public string Email { get; set; }
    }
}
