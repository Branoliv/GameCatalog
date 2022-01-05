using GameCatalog.Domain.Model.Entities;
using GameCatalog.Infra.Perisitence.EFCoreSqlServer.Map;
using Microsoft.EntityFrameworkCore;

namespace GameCatalog.Infra.Perisitence.EFCoreSqlServer
{
    public class Context : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //inclui o objeto no log
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GameMap());
        }
    }
}
