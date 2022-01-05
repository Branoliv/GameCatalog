using GameCatalog.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameCatalog.Infra.Perisitence.EFCoreSqlServer.Map
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            //Nome tabela
            builder.ToTable("Game");

            //Chave primaria
            builder.HasKey(g => g.Id);

            //Propriedades
            builder.Property(g => g.Id).IsRequired();
            builder.Property(g => g.Name).IsRequired();
            builder.Property(g => g.Producer).IsRequired();
            builder.Property(g => g.Price).IsRequired();
        }
    }
}
