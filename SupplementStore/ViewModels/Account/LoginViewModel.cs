using System.ComponentModel.DataAnnotations;

namespace SupplementStore.ViewModels.Account {

    public class LoginViewModel {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
