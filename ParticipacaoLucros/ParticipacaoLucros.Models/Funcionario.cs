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
        public string salario_bruto { get; set; }
        public string data_de_admissao { get; set; }
    }

    public class Participacao
    {
        public string matricula { get; set; }
        public string nome { get; set; }
        public decimal valor_da_participação { get; set; }
    }

    public class RetornoLucros
    {
        List<Participacao> participacoes { get; set; }
        public int total_de_funcionarios { get; set; }
        public decimal total_distribuido { get; set; }
        public decimal total_disponibilizado { get; set; }
        public decimal saldo_total_disponibilizado { get; set; }
    }
}
