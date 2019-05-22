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

namespace ParticipacaoLucros.AzureFunctions
{
    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class Funcionarios
    {
        [FunctionName("Funcionarios")]
        public static async Task<IActionResult> AddorUpdate(
            [HttpTrigger(AuthorizationLevel.Function, "post","put", Route = null)] HttpRequest req,
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
                    return (ActionResult)new OkObjectResult($"{ lFuncionario.Count.ToString() }");
                else
                    throw new Exception("Firebase returned Unsucessfully status code");
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(ex);
            }
            finally
            {
                log.LogInformation("Funcionarios_AddorUpdate finished a request.");
            }
        }
    }
}