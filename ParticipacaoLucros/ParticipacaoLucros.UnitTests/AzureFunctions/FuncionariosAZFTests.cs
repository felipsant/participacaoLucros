using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using ParticipacaoLucros.AzureFunctions;
using ParticipacaoLucros.Models;
using ParticipacaoLucros.Repositories;
using ParticipacaoLucros.Services;
using ParticipacaoLucros.UnitTests.Helpers;
using ParticipacaoLucros.UnitTests.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ParticipacaoLucros.UnitTests.AzureFunctions
{
    public class FuncionariosAZFTests
    {
        [Fact]
        public async Task AddOrUpdate_Should_ReturnOk()
        {
            //Arrange
            var mockRepository = Substitute.For<IRepository<Funcionario>>();
            mockRepository.CreateList(Arg.Any<IEnumerable<Funcionario>>()).Returns(true);

            var mockLog = Substitute.For<ILogger>();

            IList<Funcionario> lFuncionarios = new FuncionarioUnitModel().getDefaultFuncionarios();
            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(lFuncionarios);

            var funcionariosService = new FuncionariosService(mockRepository);

            //Act
            var result = await Funcionarios.AddorUpdate(mockRequest, mockLog, funcionariosService);
            //Assert
            result.StatusCode.Should().Be(200);
            var content = await result.Content.ReadAsStringAsync();
            content.Should().Be(lFuncionarios.Count.ToString());
        }

        [Fact]
        public async Task AddOrUpdate_Should_ReturnBadRequest()
        {
            //Arrange
            var mockRepository = Substitute.For<IRepository<Funcionario>>();
            mockRepository.CreateList(Arg.Any<IEnumerable<Funcionario>>()).Returns(true);

            var mockLog = Substitute.For<ILogger>();

            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(string.Empty);

            var funcionariosService = new FuncionariosService(mockRepository);

            //Act
            var result = await Funcionarios.AddorUpdate(mockRequest, mockLog, funcionariosService);

            //Assert
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetAll_Should_ReturnOk()
        {
            //Arrange
            var mockRepository = Substitute.For<IRepository<Funcionario>>();
            IList<Funcionario> lFuncionarios = new FuncionarioUnitModel().getDefaultFuncionarios();

            mockRepository.GetAll().Returns(lFuncionarios);

            var mockLog = Substitute.For<ILogger>();
            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(string.Empty);

            var funcionariosService = new FuncionariosService(mockRepository);

            //Act
            var result = await Funcionarios.GetAll(mockRequest, mockLog, funcionariosService);

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CalculaLucros_Should_ReturnOk()
        {
            //Arrange
            decimal totalDisponibilizado = 100000;
            var mockRepository = Substitute.For<IRepository<Funcionario>>();
            IList<Funcionario> lFuncionarios = new FuncionarioUnitModel().getDefaultFuncionarios();

            mockRepository.GetAll().Returns(lFuncionarios);

            var mockLog = Substitute.For<ILogger>();
            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(string.Empty);

            var funcionariosService = new FuncionariosService(mockRepository);

            //Act
            var result = await Funcionarios.CalculaLucros(mockRequest, totalDisponibilizado, mockLog, funcionariosService);

            //Assert
            result.StatusCode.Should().Be(200);
            var content = await result.Content.ReadAsStringAsync();
            string.IsNullOrEmpty(content).Should().BeFalse();
            content.Should().NotBe("null");
        }
    }
}
