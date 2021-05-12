using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class NotaFiscal
    {
        public NotaFiscal()
        {
            ItemNotaFiscals = new HashSet<ItemNotaFiscal>();
        }

        public int NotaFiscalId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataNotaFiscal { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public decimal Total { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ItemNotaFiscal> ItemNotaFiscals { get; set; }
    }
}
