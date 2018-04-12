using System.ComponentModel.DataAnnotations;

namespace elearning.model.ViewModels
{
    public class LoginVm
    {
        [Required(ErrorMessage = "You need to enter an Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You need to enter a Password")]
        public string Password { get; set; }

        public bool LoginFailed { get; set; }
    }
}
