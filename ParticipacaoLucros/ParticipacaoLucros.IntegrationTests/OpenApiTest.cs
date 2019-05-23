using FluentAssertions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ParticipacaoLucros.IntegrationTests
{
    public class OpenApiTest : BaseTest
    {
        [Fact]
        public void GetAll_Should_ReturnSuccessfullyRequest()
        {
            //Arrange
            var request = new RestRequest("OpenApi", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            response.IsSuccessful.Should().BeTrue();
            var result = response.Content;
            string.IsNullOrWhiteSpace(result).Should().Be(false);
        }
    }
}
