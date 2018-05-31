using System;
using System.Collections.Generic;

namespace ondeTem.WebApi.ViewModel
{
    public class EstabelecimentoViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? isComplete { get; set; }
        public string Nome { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string TelefonePrincipal { get; set; }
        public string TelefoneSecundario { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool? Desativado { get; set; }
        public string MensagemParaClientes { get; set; }
        public byte[] ImageHash { get; set; }
        public string CaminhoImage { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
    }
}