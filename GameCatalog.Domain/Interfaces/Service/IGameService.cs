using GameCatalog.Domain.Model.DTO;
using GameCatalog.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalog.Domain.Interfaces.Service
{
    public interface IGameService : IServiceBase<Game>
    {
        Task<Game> AddAsync(GameAddDTO gameAddDTO);
        Task UpdateAsync(GameDTO gameDTO);
        Task<List<Game>> FindByNameAndProducerAsync(string gameName, string producer);
    }
}
