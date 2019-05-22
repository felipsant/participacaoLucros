using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParticipacaoLucros.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        bool CreateList(IEnumerable<T> entity);
        Task Update(Guid id, T entity);
        Task Delete(T entity);
    }
}
