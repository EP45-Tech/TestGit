using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Barn_Mart;

namespace Crud.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext db; 
        public GenericRepository(AppDbContext context)
        {
            this.db = context;
            
        }
        public async Task<T> AddAsync(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await db.Set<T>().FindAsync(id);
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
        }

        public async Task EditAsync(T entity)
        {
            db.Set<T>().Update(entity);
            await db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int? id)
        {
            T entity = await db.Set<T>().FindAsync(id);
            if (entity != null) return true;
            else return false;
        }

        public async Task<List<T>> GetAllSync()
        {
           return await db.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int? id)
        {
            T? entity = await db.Set<T>().FindAsync(id);
            return entity;
        }
    }
}
