using System;
using System.Collections.Generic;
using ondeTem.Domain.Core;
using ondeTem.Domain.ProdutoRoot;
using ondeTem.Domain.StoryRoot;

namespace ondeTem.Domain.CategoriaRoot
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<Story> Stories { get; set; }
    }
}