using FluentAssertions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ParticipacaoLucros.AzureFunctions;
using ParticipacaoLucros.UnitTests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ParticipacaoLucros.UnitTests.AzureFunctions
{
    public class OpenApiAZFTests
    {
        [Fact]
        public async Task GetAll_Should_ReturnOk()
        {
            //Arrange
            var mockLog = Substitute.For<ILogger>();
            var mockRequest = NSubstituteHttpRequest.CreateMockRequest(string.Empty);
            var mockExecution = new ExecutionContext();

            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory).ToString();
            mockExecution.FunctionAppDirectory = currentDirectory; 

            //Act
            var result = await OpenApi.GetAll(mockRequest, mockLog, mockExecution);

            //Assert
            result.StatusCode.Should().Be(200);
            var content = await result.Content.ReadAsStringAsync();
            string.IsNullOrEmpty(content).Should().BeFalse();
            content.Should().NotBe("null");
        }
    }
}
