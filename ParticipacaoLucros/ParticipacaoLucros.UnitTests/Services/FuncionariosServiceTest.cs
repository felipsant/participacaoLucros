using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using ParticipacaoLucros.Models;
using ParticipacaoLucros.Repositories;
using ParticipacaoLucros.Services;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public async Task GetAll_Should_ReturnSuccessfullyRequest()
        {
            //Arrange
            var mockFuncionarioRepository = Substitute.For<IRepository<Funcionario>>();
            IList<Funcionario> lFuncionarios = getDefaultFuncionarios();

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
            IList<Funcionario> lFuncionarios = getDefaultFuncionarios();
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
