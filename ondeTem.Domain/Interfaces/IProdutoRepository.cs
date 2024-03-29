using System.Collections.Generic;
using System.Threading.Tasks;
using ondeTem.Domain.ProdutoRoot;

namespace ondeTem.Domain.Interfaces
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<List<Produto>> GetAllByEstabelecimentoAsync(long id);
    }
}