using Crud.Repository.Repository;
using The_Barn_Mart;

namespace BarnMart_Web.Models.Repositories
{
    public class ScheduleRepo : GenericRepository<Schedule>, IScheduleRepo
    {
        public ScheduleRepo(AppDbContext context) : base(context)
    {
    }
}
}
