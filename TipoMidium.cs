using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class TipoMidium
    {
        public TipoMidium()
        {
            Faixas = new HashSet<Faixa>();
        }

        public int TipoMidiaId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Faixa> Faixas { get; set; }
    }
}
