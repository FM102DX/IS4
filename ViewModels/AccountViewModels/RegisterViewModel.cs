using System.ComponentModel.DataAnnotations;

namespace FerryData.IS4.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
        public override string ToString()
        {
            return $"LoginViewModel UserName={UserName}, ReturnUrl={ReturnUrl} ";
        }
    }
}
