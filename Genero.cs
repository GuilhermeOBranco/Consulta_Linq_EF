using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class Genero
    {
        public Genero()
        {
            Faixas = new HashSet<Faixa>();
        }

        public int GeneroId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Faixa> Faixas { get; set; }
    }
}
