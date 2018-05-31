using System;
using ondeTem.Domain.CategoriaRoot;
using ondeTem.Domain.Core;
using ondeTem.Domain.EstabelecimentoRoot;

namespace ondeTem.Domain.ProdutoRoot
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public int Acessos { get; set; }
        public string Descricao { get; set; }
        public decimal? Preco { get; set; }
        public DateTime DataCadastro { get; set; }
        public byte[] ImageHash { get; set; }
        public string CaminhoImage { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
        public long EstabelecimentoId { get; set; }
        public Categoria Categoria { get; set; }
        public long CategoriaId { get; set; }
    }
}