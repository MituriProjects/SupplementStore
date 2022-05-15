using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Account {

    public class LoginVM {

        [Required(ErrorMessage = "EmailRequiredErrorMessage")]
        [Email]
        [Label]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordRequiredErrorMessage")]
        [UIHint("password")]
        [Label]
        public string Password { get; set; }
    }
}
