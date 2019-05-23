using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParticipacaoLucros.Models;
using System.Collections.Generic;
using ParticipacaoLucros.Services;
using AzureFunctions.Autofac;
using ParticipacaoLucros.AzureFunctions.Helpers;
using System.Net.Http;
using System.Net;
using System.Text;

namespace ParticipacaoLucros.AzureFunctions
{
    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class Funcionarios
    {
        [FunctionName("Funcionarios_AddorUpdate")]
        public static async Task<HttpResponseMessage> AddorUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "post","put", Route = "Funcionarios")] HttpRequest req,
            ILogger log, [Inject] IFuncionariosService funcionariosService)
        {
            try
            {
                log.LogInformation("Funcionarios_AddorUpdate started a request.");

                // Get request body
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var lFuncionario = JsonConvert.DeserializeObject<List<Funcionario>>(requestBody);
                if(lFuncionario == null || lFuncionario.Count == 0)
                    throw new Exception("FuncionariosList Cannot be Empty");

                bool result = await funcionariosService.AddOrUpdate(lFuncionario);
                if (result)
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(lFuncionario.Count.ToString(), Encoding.UTF8, "application/json")
                    };

                else
                    throw new Exception("Firebase returned Unsucessfully status code");
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message, Encoding.UTF8)
                };
            }
            finally
            {
                log.LogInformation("Funcionarios_AddorUpdate finished a request.");
            }
        }
        [FunctionName("Funcionarios_GetAll")]
        public static async Task<HttpResponseMessage> GetAll(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Funcionarios")] HttpRequest req,
            ILogger log, [Inject] IFuncionariosService funcionariosService)
        {
            try
            {
                log.LogInformation("Funcionarios_GetAll started a request.");

                var result = await funcionariosService.GetAll();
                string json = JsonConvert.SerializeObject(result);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                 return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message, Encoding.UTF8)
                };
            }
            finally
            {
                log.LogInformation("Funcionarios_GetAll finished a request.");
            }
        }

        [FunctionName("Funcionarios_CalculaLucros")]
        public static async Task<HttpResponseMessage> CalculaLucros(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Funcionarios/CalculaLucros/{total_disponibilizado}")] HttpRequest req,
            decimal total_disponibilizado, ILogger log, [Inject] IFuncionariosService funcionariosService)
        {
            try
            {
                log.LogInformation("Funcionarios_GetAll started a request.");

                var result = await funcionariosService.CalculaLucros(total_disponibilizado);
                string json = JsonConvert.SerializeObject(result);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(ex.Message, Encoding.UTF8)
                };
            }
            finally
            {
                log.LogInformation("Funcionarios_GetAll finished a request.");
            }
        }
    }
}