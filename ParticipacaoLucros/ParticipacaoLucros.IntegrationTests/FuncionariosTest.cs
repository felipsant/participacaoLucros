using FluentAssertions;
using Newtonsoft.Json;
using ParticipacaoLucros.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            List<Funcionario> lFuncionario = getFuncionariosFromJSONFile();
            var request = new RestRequest("Funcionarios", Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(lFuncionario);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            response.IsSuccessful.Should().BeTrue();
        }

        private static List<Funcionario> getFuncionariosFromJSONFile()
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            string projectDirectory = currentDirectory.Parent.Parent.Parent.FullName;
            string jsonFile = projectDirectory + "\\" + funcionariosJson;

            string json = File.ReadAllText(jsonFile, Encoding.UTF8);
            var lFuncionario = JsonConvert.DeserializeObject<List<Funcionario>>(json);
            return lFuncionario;
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

        [Fact]
        public void GetAll_Should_ReturnSuccessfullyRequest()
        {
            //Arrange
            AddOrUpdate_Should_SendSuccessfullyRequest();
            List<Funcionario> lFuncionarioFile = getFuncionariosFromJSONFile();

            var request = new RestRequest("Funcionarios", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            response.IsSuccessful.Should().BeTrue();
            var lFuncionario = JsonConvert.DeserializeObject<List<Funcionario>>(response.Content);
            lFuncionario.Count.Should().Be(lFuncionarioFile.Count);
        }

        [Fact]
        public void CalculaLucros_Should_ReturnSuccessfullyRequest()
        {
            //Arrange
            AddOrUpdate_Should_SendSuccessfullyRequest();
            List<Funcionario> lFuncionarios = getFuncionariosFromJSONFile();
            decimal total_disponibilizado = 100000;

            var request = new RestRequest($"Funcionarios/CalculaLucros/{total_disponibilizado}", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            response.IsSuccessful.Should().BeTrue();
            var result = JsonConvert.DeserializeObject<RetornoLucros>(response.Content);
            result.participacoes.Count.Should().Be(lFuncionarios.Count);
            result.total_de_funcionarios.Should().Be(lFuncionarios.Count);
            result.participacoes.First().valor_da_participação.Should().NotBe(0);
        }
    }
}
