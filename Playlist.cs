using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class Playlist
    {
        public Playlist()
        {
            PlaylistFaixas = new HashSet<PlaylistFaixa>();
        }

        public int PlaylistId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<PlaylistFaixa> PlaylistFaixas { get; set; }
    }
}
