using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using ParticipacaoLucros.Models;
using ParticipacaoLucros.Repositories;
using ParticipacaoLucros.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ParticipacaoLucros.UnitTests.Services
{
    public class FuncionariosServiceTest
    {
        private IList<Funcionario> getDefaultFuncionarios(int n=3)
        {
            return Builder<Funcionario>.CreateListOfSize(n).TheFirst(1)
            .With(c => c.salario_bruto = "R$ 12.696,20")
            .TheNext(1)
            .With(c => c.salario_bruto = "R$ 1.800,00")
            .TheNext(1)
            .With(c => c.salario_bruto = "R$ 998,00")
            .Build();
        }

        [Fact]
        public async Task AddOrUpdate_Should_SendSuccessfullyRequest()
        {
            //Arrange
            var mockFuncionarioRepository = Substitute.For<IRepository<Funcionario>>();
            mockFuncionarioRepository.CreateList(Arg.Any<IEnumerable<Funcionario>>()).
                Returns(true);

            FuncionariosService fs = new FuncionariosService(mockFuncionarioRepository);
            IList<Funcionario> lFuncionarios = getDefaultFuncionarios();
            
            //Act
            bool result = await fs.AddOrUpdate(lFuncionarios);

            //Assert
            result.Should().Be(true);
        }
    }
}
