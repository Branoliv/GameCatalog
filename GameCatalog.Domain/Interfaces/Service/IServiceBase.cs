using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCatalog.Domain.Interfaces.Service
{
    public interface IServiceBase<T> 
    {
        Task<List<T>> ListAsync(int pageNumber, int quantityItemsList);
        Task<T> FindByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
