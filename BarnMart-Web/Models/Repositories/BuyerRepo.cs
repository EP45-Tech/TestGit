using Crud.Repository;
using Crud.Repository.Repository;
using The_Barn_Mart;

namespace BarnMart_Web.Models.Repositories
{
    public class BuyerRepo : GenericRepository<Buyer>, IBuyerRepo
    {
        public BuyerRepo(AppDbContext context) : base(context) 
        { 
        }
    }
}
