using FizzWare.NBuilder;
using ParticipacaoLucros.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticipacaoLucros.UnitTests.Models
{
    public class FuncionarioUnitModel
    {
        public IList<Funcionario> getDefaultFuncionarios(int n = 3)
        {
            return Builder<Funcionario>.CreateListOfSize(n).TheFirst(1)
            .With(c => c.salario_bruto = "R$ 12.696,20")
            .With(c => c.area = "Diretoria")
            .With(c => c.data_de_admissao = "2001-01-05")
            .TheNext(1)
            .With(c => c.salario_bruto = "R$ 3.000,00")
            .With(c => c.area = "Contabilidade")
            .With(c => c.cargo = "Estagiário")
            .With(c => c.data_de_admissao = "2015-01-05")
            .TheNext(1)
            .With(c => c.salario_bruto = "R$ 998,00")
            .With(c => c.area = "Relacionamento com o Cliente")
            .With(c => c.data_de_admissao = "2018-01-03")
            .Build();
        }
    }
}
