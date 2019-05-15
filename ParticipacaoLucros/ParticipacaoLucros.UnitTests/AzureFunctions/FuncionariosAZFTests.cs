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
            mockService.AddOrUpdate(Arg.Any<IEnumerable<FuncionarioDTO>>()).Returns(true);

            var mockLog = Substitute.For<ILogger>();

            IList<FuncionarioDTO> lFuncionarios = Builder<FuncionarioDTO>.CreateListOfSize(2).All().Build();
            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(lFuncionarios);

            //Act
            var result = await Funcionarios.AddorUpdate(mockRequest, mockLog, mockService);
            var okResult = result as OkObjectResult;
            //Assert
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be("2");
        }

        [Fact]
        public async Task AddOrUpdate_Should_ReturnBadRequest()
        {
            //Arrange
            var mockService = Substitute.For<IFuncionariosService>();
            mockService.AddOrUpdate(Arg.Any<IEnumerable<FuncionarioDTO>>()).Returns(true);

            var mockLog = Substitute.For<ILogger>();

            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(string.Empty);

            //Act
            var result = await Funcionarios.AddorUpdate(mockRequest, mockLog, mockService);

            //Assert
            var badResult = result as BadRequestObjectResult;
            badResult.StatusCode.Should().Be(400);
        }
    }
}
