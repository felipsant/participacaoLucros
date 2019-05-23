using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParticipacaoLucros.AzureFunctions
{
    public class OpenApi
    {
        [FunctionName("OpenApi_GetAll")]
        public static async Task<HttpResponseMessage> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "OpenApi")] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            try
            {
                log.LogInformation("OpenApi_GetAll started a request.");

                string jsonFile = $"{context.FunctionAppDirectory}\\participacaolucrosazurefunctions.json";
                string json = File.ReadAllText(jsonFile, Encoding.UTF8);
                
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                throw;
            }
            finally
            {
                log.LogInformation("OpenApi_GetAll finished a request.");
            }
        }
    }
}
