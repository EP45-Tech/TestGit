using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Barn_Mart
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }


        public bool Available { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateUpdated { get; set; }
        public Decimal Price { get; set; }

    }
}
