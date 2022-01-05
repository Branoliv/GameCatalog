using System;
using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Domain.Model.Entities
{
    public class Game : BaseEntitie
    {
        protected Game() { }
        public Game(string name, string producer, double price)
        {
            Name = name;
            Producer = producer;
            Price = price;
        }

        public Game(Guid id, string name, string producer, double price):base(id)
        {
            Name = name;
            Producer = producer;
            Price = price;
        }



        [Required(ErrorMessage = "O campo é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres.")]
        public string Producer { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório.")]
        [Range(1, 1000, ErrorMessage = "O preço dever ser de nomínimo 1 real e no máximo 1000 reais.")]
        public double Price { get; set; }
    }
}
