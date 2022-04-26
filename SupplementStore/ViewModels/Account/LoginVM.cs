using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Account {

    public class LoginVM {

        [Required(ErrorMessage = "EmailRequiredErrorMessage")]
        [EmailAddress]
        [Display(Name = "EmailLabel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequiredErrorMessage")]
        [UIHint("password")]
        [Display(Name = "PasswordLabel")]
        public string Password { get; set; }
    }
}
