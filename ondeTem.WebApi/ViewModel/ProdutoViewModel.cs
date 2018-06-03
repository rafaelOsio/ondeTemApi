using System;

namespace ondeTem.WebApi.ViewModel
{
    public class ProdutoViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int Acessos { get; set; }
        public string Descricao { get; set; }
        public decimal? Preco { get; set; }
        public DateTime DataCadastro { get; set; }
        public string CaminhoImage { get; set; }
        public EstabelecimentoViewModel Estabelecimento { get; set; }
        public long EstabelecimentoId { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public long CategoriaId { get; set; }
    }
}