using System.Collections.Generic;
using System.Threading.Tasks;
using ondeTem.Domain.EstabelecimentoRoot;

namespace ondeTem.Domain.Interfaces
{
    public interface IEstabelecimentoRepository : IRepositoryBase<Estabelecimento>
    {
        Task<Estabelecimento> AuthenticateAsync(EstabelecimentoUser item);
    }
}