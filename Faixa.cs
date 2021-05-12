using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class Faixa
    {
        public Faixa()
        {
            ItemNotaFiscals = new HashSet<ItemNotaFiscal>();
            PlaylistFaixas = new HashSet<PlaylistFaixa>();
        }

        public int FaixaId { get; set; }
        public string Nome { get; set; }
        public int? AlbumId { get; set; }
        public int TipoMidiaId { get; set; }
        public int? GeneroId { get; set; }
        public string Compositor { get; set; }
        public int Milissegundos { get; set; }
        public int? Bytes { get; set; }
        public decimal PrecoUnitario { get; set; }

        public virtual Album Album { get; set; }
        public virtual Genero Genero { get; set; }
        public virtual TipoMidium TipoMidia { get; set; }
        public virtual ICollection<ItemNotaFiscal> ItemNotaFiscals { get; set; }
        public virtual ICollection<PlaylistFaixa> PlaylistFaixas { get; set; }
    }
}
