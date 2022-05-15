using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Account {

    public class RegisterVM {

        [Required(ErrorMessage = "EmailRequiredErrorMessage")]
        [Email]
        [Display(Name = "EmailLabel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequiredErrorMessage")]
        [UIHint("password")]
        [Display(Name = "PasswordLabel")]
        public string Password { get; set; }
    }
}
