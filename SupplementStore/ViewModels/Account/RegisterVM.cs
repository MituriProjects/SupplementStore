using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Account {

    public class RegisterVM {

        [IsRequired]
        [Email]
        [Label]
        public string Email { get; set; }

        [IsRequired]
        [UIHint("password")]
        [Label]
        public string Password { get; set; }

        [IsRequired]
        [UIHint("password")]
        [Label]
        [Compare("Password", ErrorMessage = "PasswordsEqualityRequiredErrorMessage")]
        public string RepeatPassword { get; set; }
    }
}
