using System.Collections.Generic;
using System.Threading.Tasks;
using ondeTem.Domain.EstabelecimentoRoot;

namespace ondeTem.Domain.Interfaces
{
    public interface IEstabelecimentoRepository : IRepositoryBaseGeneric<Estabelecimento>
    {
        Task<Estabelecimento> AuthenticateAsync(EstabelecimentoUser item);
    }
}