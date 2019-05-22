using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParticipacaoLucros.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        bool CreateList(IEnumerable<T> entity);
    }
}
