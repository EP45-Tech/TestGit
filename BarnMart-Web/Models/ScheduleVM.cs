using System.ComponentModel.DataAnnotations;

namespace BarnMart_Web.Models
{
    public class ScheduleVM
    {
        [Required]
        public int Id { get; set; }

        public string vehicle { get; set; }

        public string Driver { get; set; }

        public string PickUpLocation { get; set; }

        public string PickUpDay { get; set; }

        public string PickUpTime { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}
