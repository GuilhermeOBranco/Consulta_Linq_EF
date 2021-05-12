using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class Album
    {
        public Album()
        {
            Faixas = new HashSet<Faixa>();
        }

        public int AlbumId { get; set; }
        public string Titulo { get; set; }
        public int ArtistaId { get; set; }

        public virtual Artistum Artista { get; set; }
        public virtual ICollection<Faixa> Faixas { get; set; }
    }
}
