using System;
using System.ComponentModel.DataAnnotations;

namespace GameCatalog.Domain.Model.DTO
{
    public class GameDTO
    {
        [Required(ErrorMessage = "O campo é obrigatório.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres.")]
        public string Producer { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório.")]
        [Range(1, 1000, ErrorMessage = "O preço dever ser de nomínimo 1 real e no máximo 1000 reais.")]
        public double Price { get; set; }
    }
}
