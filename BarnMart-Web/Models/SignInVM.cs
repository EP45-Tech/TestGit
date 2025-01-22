using System.ComponentModel.DataAnnotations;

namespace BarnMart_Web.Models
{
    public class SignInVM
    {
        public SignInVM()
        {
            Username = "";
            Password = "";
            returnUrl = "";
        }

        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string returnUrl { get; set; }
    }
}
