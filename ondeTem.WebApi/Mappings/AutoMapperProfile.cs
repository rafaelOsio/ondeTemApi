using AutoMapper;
using ondeTem.Domain.CategoriaRoot;
using ondeTem.Domain.EstabelecimentoRoot;
using ondeTem.Domain.ProdutoRoot;
using ondeTem.WebApi.ViewModel;

namespace ondeTem.WebApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Produto, ProdutoViewModel>().PreserveReferences().MaxDepth(1);

            CreateMap<Estabelecimento, EstabelecimentoViewModel>().PreserveReferences().MaxDepth(1);

            CreateMap<Categoria, CategoriaViewModel>().PreserveReferences().MaxDepth(1);
        }
    }
}