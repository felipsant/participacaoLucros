using ParticipacaoLucros.Models;
using ParticipacaoLucros.Services.Helpers;
using RestSharp;
using System;
using System.Collections.Generic;

namespace ParticipacaoLucros.Services
{
    public interface IFuncionariosService
    {
        bool AddOrUpdate(IEnumerable<FuncionarioDTO> lFuncionarios);
    }
    public class FuncionariosService : Firebase, IFuncionariosService
    {
        public FuncionariosService()
        {
            this.client = new RestClient(databaseAddress);
        }
        public FuncionariosService(RestClient _client)
        {
            this.client =_client;
        }
        public bool AddOrUpdate(IEnumerable<FuncionarioDTO> lFuncionarios)
        {
            var request = new RestRequest($".json", Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(lFuncionarios);
            IRestResponse response = client.Execute(request);
            return response.IsSuccessful;
        }
    }
}
