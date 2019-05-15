using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using ParticipacaoLucros.Models;
using ParticipacaoLucros.Services;
using RestSharp;
using System.Collections.Generic;
using Xunit;

namespace ParticipacaoLucros.UnitTests
{
    public class FuncionariosServiceTest
    {
        [Fact]
        public void AddOrUpdate_Should_SendSuccessfullyRequest()
        {
            //Arrange
            var mockClient = Substitute.For<RestClient>();
            var mockResponse = new RestResponse()
            {
                ResponseStatus = ResponseStatus.Completed,
                StatusCode = System.Net.HttpStatusCode.OK
            };
            mockClient.Execute(Arg.Any<RestRequest>()).Returns(mockResponse);
            FuncionariosService fs = new FuncionariosService(mockClient);
            IList<FuncionarioDTO> lFuncionarios =
                Builder<FuncionarioDTO>.CreateListOfSize(2).All()
                .Build();

            //Act
            bool result = fs.AddOrUpdate(lFuncionarios);

            //Assert
            result.Should().Be(true);
        }
    }
}
