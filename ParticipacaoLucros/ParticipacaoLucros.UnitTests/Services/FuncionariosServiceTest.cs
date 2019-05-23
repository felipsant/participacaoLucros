using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using ParticipacaoLucros.Models;
using ParticipacaoLucros.Repositories;
using ParticipacaoLucros.Services;
using ParticipacaoLucros.UnitTests.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ParticipacaoLucros.UnitTests.Services
{
    public class FuncionariosServiceTest
    {
        [Fact]
        public async Task AddOrUpdate_Should_SendSuccessfullyRequest()
        {
            //Arrange
            var mockFuncionarioRepository = Substitute.For<IRepository<Funcionario>>();
            mockFuncionarioRepository.CreateList(Arg.Any<IEnumerable<Funcionario>>()).
                Returns(true);

            FuncionariosService fs = new FuncionariosService(mockFuncionarioRepository);
            IList<Funcionario> lFuncionarios = new FuncionarioUnitModel().getDefaultFuncionarios();
            
            //Act
            bool result = await fs.AddOrUpdate(lFuncionarios);

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task GetAll_Should_ReturnSuccessfullyRequest()
        {
            //Arrange
            var mockFuncionarioRepository = Substitute.For<IRepository<Funcionario>>();
            IList<Funcionario> lFuncionarios = new FuncionarioUnitModel().getDefaultFuncionarios();

            mockFuncionarioRepository.GetAll().
                Returns(lFuncionarios);

            FuncionariosService fs = new FuncionariosService(mockFuncionarioRepository);

            //Act
            var result = await fs.GetAll();

            //Assert
            result.Count().Should().Be(lFuncionarios.Count);
        }

        [Fact]
        public async Task CalculaLucros_Should_ReturnSuccessfullyRequest()
        {
            //Arrange
            IList<Funcionario> lFuncionarios = new FuncionarioUnitModel().getDefaultFuncionarios();
            var mockFuncionarioRepository = Substitute.For<IRepository<Funcionario>>();
            decimal totalDisponibilizado = 100000;

            mockFuncionarioRepository.GetAll().
                Returns(lFuncionarios);
            FuncionariosService fs = new FuncionariosService(mockFuncionarioRepository);

            //Act
            var result = await fs.CalculaLucros(totalDisponibilizado);

            //Assert
            //TODO: WRITE one fixed return for the defaultFuncionarios
            result.total_de_funcionarios.Should().Be(lFuncionarios.Count);
            result.participacoes.Count.Should().Be(lFuncionarios.Count);
            result.participacoes.First().matricula.Should().NotBe(string.Empty);
            result.participacoes.First().valor_da_participação.Should().NotBe(0);
            result.participacoes.First().nome.Should().NotBe(string.Empty);
            result.total_disponibilizado.Should().Be(totalDisponibilizado);
            result.total_distribuido.Should().BeGreaterThan(0);
            result.saldo_total_disponibilizado.Should().NotBe(0);
        }
    }
}
