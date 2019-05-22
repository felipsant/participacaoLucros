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
        public async Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException("TODO");
        }
        public bool CreateList(IEnumerable<T> entities)
        {
            var request = new RestRequest($"./{typeof(T).Name}.json", Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(entities);
            IRestResponse response = _client.Execute(request);
            return response.IsSuccessful;
        }
        public async Task Update(Guid id, T entity)
        {
            throw new NotImplementedException("TODO");
        }
        public async Task Delete(T entity)
        {
            throw new NotImplementedException("TODO");
        }
    }
}
