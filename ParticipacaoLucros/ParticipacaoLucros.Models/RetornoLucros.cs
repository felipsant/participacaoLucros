using System;
using System.Collections.Generic;
using System.Text;

namespace ParticipacaoLucros.Models
{
    public class Participacao
    {
        public string matricula { get; set; }
        public string nome { get; set; }
        public string valor_da_participação { get; set; }
    }

    public class RetornoLucros
    {
        List<Participacao> participacoes { get; set; }
        public int total_de_funcionarios { get; set; }
        public string total_distribuido { get; set; }
        public string total_disponibilizado { get; set; }
        public string saldo_total_disponibilizado { get; set; }
    }
}
