using System;
using System.Collections.Generic;

namespace ondeTem.WebApi.ViewModel
{
    public class CategoriaViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
        //public List<Story> Stories { get; set; }
    }
}