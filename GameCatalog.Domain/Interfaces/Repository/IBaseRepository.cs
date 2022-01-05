using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalog.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T> 
    {
        Task<T> AddAsync(T obj);
        Task<T> FindByIdAsync(Guid id);
        Task UpdateAsync(T obj);
        Task<bool> RemoveAsync(T obj);
        Task<List<T>> ListAsync(int pageNumber, int quantityItemsList);
    }
}
