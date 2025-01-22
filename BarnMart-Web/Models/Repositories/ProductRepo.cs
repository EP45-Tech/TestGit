using Crud.Repository.Repository;
using The_Barn_Mart;

namespace BarnMart_Web.Models.Repositories
{
    public class ProductRepo : GenericRepository<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext context) : base(context)
        {
        }
    }
}
