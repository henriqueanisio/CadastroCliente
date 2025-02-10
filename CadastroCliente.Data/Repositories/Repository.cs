using CadastroCliente.Data.Context;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CadastroCliente.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<T> DbSet;

        public Repository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<T> GetByIdWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = DbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }


        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public virtual async Task<IEnumerable<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = DbSet.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }


        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            DbSet.Add(entity);
            await Db.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            DbSet.Remove(new T { Id = id });
            await Db.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
