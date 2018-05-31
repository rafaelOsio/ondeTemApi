using System;
using ondeTem.Domain.CategoriaRoot;
using ondeTem.Domain.Core;

namespace ondeTem.Domain.StoryRoot
{
    public class Story : Entity
    {
        public string Descricao { get; set; }
        public byte[] ImageHash { get; set; }
        public string CaminhoImage { get; set; }
        public DateTime DataFinalPostagem { get; set; }
        public Categoria Categoria { get; set; }
        public long CategoriaId { get; set; }
    }
}