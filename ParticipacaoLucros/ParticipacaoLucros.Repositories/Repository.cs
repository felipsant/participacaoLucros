using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParticipacaoLucros.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal RestClient _client;
        public Repository(RestClient client)
        {
            this._client = client;
        }
        public IEnumerable<T> GetAll()
        {
            var request = new RestRequest($"{typeof(T).Name}.json", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = _client.Execute(request);
            if (!response.IsSuccessful)
                throw new Exception($"Not Possible to get {typeof(T).Name} objects from FirebaseDB");

            var content = response.Content;
            var result = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
            return result;
        }
        public bool CreateList(IEnumerable<T> entities)
        {
            var request = new RestRequest($"{typeof(T).Name}.json", Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(entities);
            IRestResponse response = _client.Execute(request);
            return response.IsSuccessful;
        }
    }
}
