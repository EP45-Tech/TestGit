using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Barn_Mart
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=NIGHTWING\\SQLEXPRESS;" +
            //                            "database=SuppliersINV_PUNO;" +
            //                            "Integrated Security=SSPI;" +
            //                            "TrustServerCertificate=true");
        }



        public DbSet<Seller> SellersINV { get; set; }
        public DbSet<Product> ProductsINV { get; set; }
        public DbSet<Buyer> BuyersINV { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Schedule> Schedules { get; set; }



    }
}
