using System.ComponentModel.DataAnnotations;

namespace BarnMart_Web.Models
{
    public class SellerVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? Middlename { get; set; }
    }
}
