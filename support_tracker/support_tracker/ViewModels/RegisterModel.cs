using System.ComponentModel.DataAnnotations;

namespace support_tracker.ViewModels
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}