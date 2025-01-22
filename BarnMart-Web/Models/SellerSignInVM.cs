using System.ComponentModel.DataAnnotations;

namespace BarnMart_Web.Models
{
    public class SellerSignInVM
    {
        public SellerSignInVM()
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
