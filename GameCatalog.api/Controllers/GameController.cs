using GameCatalog.api.Validations;
using GameCatalog.Domain.Interfaces.Service;
using GameCatalog.Domain.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GameCatalog.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse(statusCode: 400, description: "Ocorreu um erro na requisição.", Type = typeof(ValidationsModels))]
    [SwaggerResponse(statusCode: 422, description: "Não foi possível processar a requisição.", Type = typeof(GenericError))]
    [SwaggerResponse(statusCode: 500, description: "Ocorreu um erro interno no servidador.", Type = typeof(GenericError))]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Busca todos os jogos de forma paginada.
        /// </summary>
        ///  <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pageNumber">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantityItemsList">Indica a quantidade de registros por página</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>
        /// <response code="422">Caso haja algum erro</response>
        /// <returns>Retorna uma lista de GameDTO de forma assíncrona</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao listar os jogos.", Type = typeof(IEnumerable<GameDTO>))]
        [SwaggerResponse(statusCode: 204, description: "Lista está vazia.", Type = typeof(GenericError))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GamesAsync([FromQuery, Range(1, int.MaxValue)] int pageNumber = 1, [FromQuery, Range(1, 50)] int quantityItemsList = 5)
        {
            try
            {
                var games = await _gameService.ListAsync(pageNumber, quantityItemsList);

                if (games.Count == 0)
                    return NoContent();

                return Ok(games);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }


        /// <summary>
        /// Busca jogo pelo número de Id.
        /// </summary>
        /// <param name="idGame">Id referente a qual jogo deve ser pesquisado.</param>
        /// <response code="200">Retorna o jogo pesquisado</response>
        /// <response code="204">Caso o jogo não exista</response>
        /// <response code="422">Caso haja algum erro</response>
        /// <returns>Retorna objeto GameDTO de forma assíncrona</returns>
        [SwaggerResponse(statusCode: 200, description: "Retorna o jogo pesquisado", Type = typeof(GameDTO))]
        [SwaggerResponse(statusCode: 204, description: "Caso não haja jogos", Type = typeof(GenericError))]
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameDTO>> GetGameByIdAsync([FromRoute] Guid idGame)
        {
            try
            {
                var game = await _gameService.FindByIdAsync(idGame);

                if (game == null)
                    return NoContent();

                return Ok(game);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new GenericError(ex.Message));
            }
        }


        /// <summary>
        /// Adiciona um novo jogo
        /// </summary>
        /// <param name="gameAddDTO">Objeto GameDTO que representa os dados do jogo.</param>
        /// <response code="201">Retorna os dados jogo adicionado</response>
        /// <response code="422">Caso haja algum erro no cadastro do jogo</response>
        /// <returns>Retorna objeto GameDTO de forma assíncrona</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar o jogo.", Type = typeof(GameDTO))]
        [HttpPost]
        public async Task<ActionResult<GameDTO>> AddGameAsync([FromBody] GameAddDTO gameAddDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ValidationsModels(ModelState));

                var game = await _gameService.AddAsync(gameAddDTO);

                if (game == null)
                    return UnprocessableEntity(new GenericError("O jogo já existe"));

                //TODO - autoMap game => gameDTO
                var gameResponse = new GameDTO
                {
                    Id = game.Id,
                    Name = game.Name,
                    Producer = game.Producer,
                    Price = game.Price
                };

                return Created("", gameResponse);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new GenericError(ex.Message));
            }
        }

        /// <summary>
        /// Atualiza um jogo existente  de forma assíncrona
        /// </summary>
        /// <param name="gameDTO">Objeto GameDTO com as atualizações a serem realizadas no registro.</param>
        /// <response code="200">O jogo foi atualizado com sucesso</response>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao atualizar o jogo.")]
        [HttpPut()]
        public async Task<ActionResult> UpdateGameAsync([FromBody] GameDTO gameDTO)
        {
            try
            {
                //TODO - verificar modelo é valido "ModelIsValid"
                if (!ModelState.IsValid)
                    return BadRequest(new ValidationsModels(ModelState));

                await _gameService.UpdateAsync(gameDTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new GenericError(ex.Message));
            }
        }

        /// <summary>
        /// Apaga o registro de um determinado jogo de forma assíncrona.
        /// </summary>
        /// <param name="idGame">Id referente ao jogo a ser apagado.</param>
        /// <response code="200">O jogo foi excluido com sucesso.</response>
        /// <response code="204">Jogo não excluído.</response>
        /// <response code="422">Caso haja algum erro na exclusão do jogo</response>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao excluir o jogo.")]
        [SwaggerResponse(statusCode: 204, description: "Jogo não excluído.")]
        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGameAsync([FromRoute] Guid idGame)
        {
            try
            {
                var response = await _gameService.DeleteAsync(idGame);

                if (!response)
                    return NoContent();

                return Ok();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new GenericError(ex.Message));
            }
        }
    }
}
