using System;
using System.Collections.Generic;

namespace ParticipacaoLucros.Models
{
    public class Funcionario
    {
        public string matricula { get; set; }
        public string nome { get; set; }
        public string area { get; set; }
        public string cargo { get; set; }
        public decimal salario_bruto { get; set; }
        public DateTime data_de_admissao { get; set; }
    }

    public class FuncionarioDTO
    {
        public string matricula { get; set; }
        public string nome { get; set; }
        public string area { get; set; }
        public string cargo { get; set; }
        public string salario_bruto { get; set; }
        public string data_de_admissao { get; set; }
    }
}
