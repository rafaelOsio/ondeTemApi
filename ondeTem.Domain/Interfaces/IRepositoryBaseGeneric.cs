using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ondeTem.Domain.Interfaces
{
    public interface  IRepositoryBaseGeneric<TEntity> : IDisposable where TEntity : class
    {
        Task<string> AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id);

        Task<List<TEntity>> GetAllAsync();

        Task<string> UpdateAsync(TEntity obj);
        
        Task<string> RemoveAsync(long id);

        Task<int> SaveChangesAsync();
    }
}