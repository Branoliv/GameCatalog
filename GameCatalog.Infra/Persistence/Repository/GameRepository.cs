using GameCatalog.Domain.Interfaces.Repository;
using GameCatalog.Domain.Model.Entities;
using GameCatalog.Infra.Perisitence.EFCoreSqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCatalog.Infra.Perisitence.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly Context _context;

        public GameRepository(Context context)
        {
            _context = context;
        }


        public async Task<Game> AddAsync(Game game)
        {
            var gameResult = await _context.Games.AddAsync(game);
            _context.SaveChanges();

            return gameResult.Entity;
        }

        public async Task<Game> FindByIdAsync(Guid id)
        {
            var game = await _context.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            return game;
        }

        public async Task<List<Game>> FindByNameAndProducerAsync(string name, string producer)
        {
            var gamesResult = _context.Games
                .AsNoTracking()
                .Where(g => g.Name.ToLower().Equals(name.ToLower()) && g.Producer.ToLower().Equals(producer.ToLower()))
                .ToListAsync();

            return await gamesResult;
        }

        public async Task<List<Game>> ListAsync(int pageNumber, int quantityItemsList)
        {
            var result = await _context.Games.Skip((pageNumber - 1) * quantityItemsList)
                .Take(quantityItemsList)
                .ToListAsync();
            return result;
        }

        public async Task<bool> RemoveAsync(Game obj)
        {
            _context.Games.Remove(obj);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games
                .Update(game);

            await _context.SaveChangesAsync();
        }
    }
}
