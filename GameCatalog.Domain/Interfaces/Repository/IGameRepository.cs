using GameCatalog.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalog.Domain.Interfaces.Repository
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<List<Game>> FindByNameAndProducerAsync(string gameName, string producer);
    }
}
