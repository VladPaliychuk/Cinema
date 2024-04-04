using System.Net;
using Microsoft.AspNetCore.Mvc;
using UserCard.API.Data;
using UserCard.API.Repositories.Interfaces;

namespace UserCard.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardsRepository _cardsRepository;
    private readonly ILogger<CardsController> _logger;

    public CardsController(ICardsRepository cardsRepository,
        ILogger<CardsController> logger)
    {
        _cardsRepository = cardsRepository;
        _logger = logger;
    }

    [HttpGet("GetCards")]
    [ProducesResponseType(typeof(IEnumerable<Entities.UserCard>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.UserCard>>> GetCards()
    {
        try
        {
            // Отримуємо всі картки користувачів з репозиторію
            var result = await _cardsRepository.GetCards();
            _logger.LogInformation("Отримали всі картки користувачів з бази даних.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка у методі GetCards: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Виникла помилка на сервері.");
        }
    }

    [HttpGet("GetShortCards")]
    [ProducesResponseType(typeof(IEnumerable<Entities.UserCard>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.UserCard>>> GetShortCards()
    {
        try
        {
            // Отримуємо короткі картки користувачів з репозиторію
            var result = await _cardsRepository.GetShortCards();
            _logger.LogInformation("Отримали короткі картки користувачів з бази даних.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка у методі GetShortCards: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Виникла помилка на сервері.");
        }
    }


    [HttpGet("GetCardById/{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.UserCard>> GetCardById(Guid id)
    {
        try
        {
            var result = await _cardsRepository.GetCardById(id);
            _logger.LogInformation($"Отримали картку користувача з ID: {id}");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetCardById() - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Виникла помилка на сервері.");
        }
    }

    [HttpGet("GetCardByUsername/{username}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.UserCard>> GetCardByUsername(string username)
    {
        try
        {
            var result = await _cardsRepository.GetCardByUsername(username);
            _logger.LogInformation($"Отримали картку користувача за іменем: {username}.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка у методі GetCardByUsername: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Виникла помилка на сервері.");
        }
    }


    [HttpPost("CreateCard")]
    [ProducesResponseType(typeof(Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateCard(Entities.UserCard card)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                return BadRequest("Обєкт івенту є некоректним");
            }
            await _cardsRepository.CreateCard(card);
            _logger.LogInformation($"Картка користувача з ID: {card.Id} була створена.");
            return CreatedAtAction(nameof(GetCardById), new { id = card.Id }, card);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка при створенні картки: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Помилка на сервері при створенні картки.");
        }
    }


    [HttpPut("UpdateCard")]
    [ProducesResponseType(typeof(Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCard(Entities.UserCard card)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _cardsRepository.UpdateCard(card);
            _logger.LogInformation($"Картка користувача з ID: {card.Id} була оновлена.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка при оновленні картки: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Помилка на сервері при оновленні картки.");
        }
    }


    [HttpDelete("DeleteCard/{id}")]
    [ProducesResponseType(typeof(Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCard(Guid id)
    {
        var cardToDelete = await _cardsRepository.GetCardById(id);
        if (cardToDelete == null)
        {
            _logger.LogInformation($"Картка користувача з ID: {id} не знайдена.");
            return NotFound();
        }

        try
        {
            await _cardsRepository.DeleteCard(id);
            _logger.LogInformation($"Картка користувача з ID: {id} була видалена.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка при видаленні картки: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Помилка на сервері при видаленні картки.");
        }
    }

}
