using FluentAssertions;
using Newtonsoft.Json;
using ParticipacaoLucros.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ParticipacaoLucros.IntegrationTests
{
    public class FuncionariosTest : BaseTest
    {
        private const string funcionariosJson = "/FuncionariosTest.json";

        [Fact]
        public void AddOrUpdate_Should_SendSuccessfullyRequest()
        {
            //Arrange
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            string projectDirectory = currentDirectory.Parent.Parent.Parent.FullName;
            string jsonFile = projectDirectory + "\\" + funcionariosJson;

            string json = File.ReadAllText(jsonFile);
            var lFuncionario = JsonConvert.DeserializeObject<List<FuncionarioDTO>>(json);
            var request = new RestRequest("Funcionarios", Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(lFuncionario);
            
            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            response.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void AddOrUpdate_Should_FailOnInvalidRequest()
        {
            //Arrange
            var request = new RestRequest("Funcionarios", Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            response.IsSuccessful.Should().BeFalse();
        }
    }
}
