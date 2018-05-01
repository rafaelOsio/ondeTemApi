using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ondeTem.Domain.Interfaces
{
    public interface  IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task<string> AddAsync(TEntity obj);

        Task<TEntity> GetByIdAsync(long id, long estabelecimentoId);

        Task<List<TEntity>> GetAllAsync(long estabelecimentoId);

        Task<string> UpdateAsync(TEntity obj, long estabelecimentoId);
        
        Task<string> RemoveAsync(long id, long estabelecimentoId);

        Task<int> SaveChangesAsync();
    }
}
