using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Services.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}