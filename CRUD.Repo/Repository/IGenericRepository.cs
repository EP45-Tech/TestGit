using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Repository.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllSync();
        Task DeleteAsync(int id);
        Task<T?> GetAsync(int? id);
        Task EditAsync(T entity);
        Task<bool> Exists(int? id);
    }
}
