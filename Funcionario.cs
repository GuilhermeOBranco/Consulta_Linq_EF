using System;
using System.Collections.Generic;

#nullable disable

namespace cursoLinqAlura.Entity
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Clientes = new HashSet<Cliente>();
            InverseSeReportaANavigation = new HashSet<Funcionario>();
        }

        public int FuncionarioId { get; set; }
        public string Sobrenome { get; set; }
        public string PrimeiroNome { get; set; }
        public string Titulo { get; set; }
        public int? SeReportaA { get; set; }
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public virtual Funcionario SeReportaANavigation { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Funcionario> InverseSeReportaANavigation { get; set; }
    }
}
