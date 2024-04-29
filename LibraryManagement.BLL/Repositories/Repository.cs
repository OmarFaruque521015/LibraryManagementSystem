using LibraryManagement.BLL.IRepositories;
using LibraryManagement.DLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Repositories
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly LibraryDBContext _libraryDbContext;

        public Repository(LibraryDBContext libraryDbContext)
        {
            _dbSet = libraryDbContext.Set<T>();
            _libraryDbContext = libraryDbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _libraryDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _libraryDbContext.Entry(entity).State = EntityState.Modified;
            await _libraryDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _libraryDbContext.SaveChangesAsync();

        }
    }
}
