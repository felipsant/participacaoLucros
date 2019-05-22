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
using ParticipacaoLucros.Services;
using ParticipacaoLucros.UnitTests.Helpers;
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
            var mockService = Substitute.For<IFuncionariosService>();
            mockService.AddOrUpdate(Arg.Any<IEnumerable<Funcionario>>()).Returns(true);

            var mockLog = Substitute.For<ILogger>();

            IList<Funcionario> lFuncionarios = Builder<Funcionario>.CreateListOfSize(2).All().Build();
            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(lFuncionarios);

            //Act
            var result = await Funcionarios.AddorUpdate(mockRequest, mockLog, mockService);
            //Assert
            result.StatusCode.Should().Be(200);
            var content = await result.Content.ReadAsStringAsync();
            content.Should().Be("2");
        }

        [Fact]
        public async Task AddOrUpdate_Should_ReturnBadRequest()
        {
            //Arrange
            var mockService = Substitute.For<IFuncionariosService>();
            mockService.AddOrUpdate(Arg.Any<IEnumerable<Funcionario>>()).Returns(true);

            var mockLog = Substitute.For<ILogger>();

            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(string.Empty);

            //Act
            var result = await Funcionarios.AddorUpdate(mockRequest, mockLog, mockService);

            //Assert
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetAll_Should_ReturnOk()
        {
            //Arrange
            var mockService = Substitute.For<IFuncionariosService>();
            IList<Funcionario> lFuncionarios = Builder<Funcionario>.CreateListOfSize(2).All().Build();

            mockService.GetAll().Returns(lFuncionarios);

            var mockLog = Substitute.For<ILogger>();
            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(string.Empty);

            //Act
            var result = await Funcionarios.GetAll(mockRequest, mockLog, mockService);

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
