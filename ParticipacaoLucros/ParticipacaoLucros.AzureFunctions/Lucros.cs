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

namespace ParticipacaoLucros.AzureFunctions
{
    public static class Function1
    {
        [FunctionName("Lucros")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] List<Funcionario> req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                return (ActionResult)new OkObjectResult($"Hello");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }
        }
    }
}
