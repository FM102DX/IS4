using System.ComponentModel.DataAnnotations;

namespace FerryData.IS4.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        public bool Remember { get; set; }

        public override string ToString()
        {
            return $"LoginViewModel UserName={UserName}, ReturnUrl={ReturnUrl} Remember={Remember}";
        }
    }
}
