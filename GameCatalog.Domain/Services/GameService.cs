using GameCatalog.Domain.Interfaces.Repository;
using GameCatalog.Domain.Interfaces.Service;
using GameCatalog.Domain.Model.DTO;
using GameCatalog.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameCatalog.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }


        public async Task<Game> AddAsync(GameAddDTO gameAddDTO)
        {
            var gameExist = await _gameRepository.FindByNameAndProducerAsync(gameAddDTO.Name, gameAddDTO.Producer);

            if (gameExist.Count > 0)
                return (Game)null;

            var gameRequest = new Game(gameAddDTO.Name, gameAddDTO.Producer, gameAddDTO.Price);

            return await _gameRepository.AddAsync(gameRequest);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var gameExist = await _gameRepository.FindByIdAsync(id);

            if (gameExist == null)
                return false;

            return await _gameRepository.RemoveAsync(gameExist);
        }

        public async Task<Game> FindByIdAsync(Guid id)
        {
            return await _gameRepository.FindByIdAsync(id);
        }

        public async Task<List<Game>> FindByNameAndProducerAsync(string gameName, string producer)
        {
            return await _gameRepository.FindByNameAndProducerAsync(gameName, producer);
        }

        public async Task<List<Game>> ListAsync(int pageNumber, int quantityItemsList)
        {
            return await _gameRepository.ListAsync(pageNumber, quantityItemsList);
        }

        public async Task UpdateAsync(GameDTO obj)
        {
            var gameExist = await _gameRepository.FindByIdAsync(obj.Id);

            if (gameExist == null)
                throw new Exception();

            var game = new Game(obj.Id, obj.Name, obj.Producer, obj.Price);

            await _gameRepository.UpdateAsync(game);
        }
    }
}
