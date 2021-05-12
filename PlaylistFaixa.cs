using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class PlaylistFaixa
    {
        public int PlaylistId { get; set; }
        public int FaixaId { get; set; }
        public virtual Faixa Faixa { get; set; }
        public virtual Playlist Playlist { get; set; }
    }
}
