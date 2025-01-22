using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Barn_Mart
{
    public class Schedule
    {
        [Key]
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
