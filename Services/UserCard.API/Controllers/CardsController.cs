using System.Net;
using Microsoft.AspNetCore.Mvc;
using UserCard.BLL.DTOs;
using UserCard.BLL.Services.Interfaces;
using UserCard.DAL.Repositories.Interfaces;

namespace UserCard.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ILogger<CardsController> _logger;
    private readonly IUserCardService _userCardService;
    private readonly ICardsRepository _cardsRepository;

    public CardsController(
        IUserCardService userCardService,
        ILogger<CardsController> logger,
        ICardsRepository cardsRepository
        )
    {
        _userCardService = userCardService;
        _logger = logger;
        _cardsRepository = cardsRepository;
    }

    [HttpGet("GetAllUserCards")]
    [ProducesResponseType(typeof(IEnumerable<UserCardDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<UserCardDto>>> GetAllUserCards()
    {
        try
        {
            var userCards = await _userCardService.GetAllUserCards();
            if (userCards == null)
            {
                _logger.LogInformation("Отримано пустий список карток користувачів.");
                return NotFound();
            }

            _logger.LogInformation("Отримали всі картки користувачів з бази даних.");
            return Ok(userCards);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка у методі GetAllUserCards: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Виникла помилка на сервері.");
        }
    }



    [HttpGet]
    [Route("[action]/{id}", Name = "GetCardById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(DAL.Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DAL.Entities.UserCard>> GetCardById(Guid id)
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

    [HttpGet]
    [Route("[action]/{username}", Name = "GetCardByUsername")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(UserCardDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UserCardDto>> GetCardByUsername(string username)
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

    [HttpGet]
    [Route("[action]/{country}", Name = "GetCardsByCountry")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(UserCardDto), (int)HttpStatusCode.OK)]
    
    public async Task<ActionResult<IEnumerable<UserCardDto>>> GetCardsByCountry(string country)
    {
        try
        {
            var result = await _userCardService.GetUserCardsByCountry(country);
            _logger.LogInformation($"Отримали картки користувачів з країни: {country}.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка у методі GetCardsByCountry: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Виникла помилка на сервері.");
        }
    }

    [HttpPost("CreateCard")]
    [ProducesResponseType(typeof(UserCardDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateCard([FromBody] UserCardDto card)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                return BadRequest("Обєкт івенту є некоректним");
            }
            await _userCardService.CreateUserCard(card);
            _logger.LogInformation($"Картка користувача з Username: {card.UserName} була створена.");
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка при створенні картки: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Помилка на сервері при створенні картки.");
        }
    }


    [HttpPut("UpdateCard")]
    [ProducesResponseType(typeof(DAL.Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCard([FromBody] UserCardDto card)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _userCardService.UpdateUserCard(card);
            _logger.LogInformation($"Картка користувача з UserName: {card.UserName} була оновлена.");
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка при оновленні картки: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Помилка на сервері при оновленні картки.");
        }
    }


    [HttpDelete]
    [Route("[action]/{username}", Name = "DeleteCard")]
    [ProducesResponseType(typeof(DAL.Entities.UserCard), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteCard(string username)
    {
        var cardToDelete = await _userCardService.GetUserCardByUsername(username);
        if (cardToDelete == null)
        {
            _logger.LogInformation($"Картка користувача з Username: {username} не знайдена.");
            return NotFound();
        }

        try
        {
            await _userCardService.DeleteUserCard(username);
            _logger.LogInformation($"Картка користувача з Username: {username} була видалена.");
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Помилка при видаленні картки: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Помилка на сервері при видаленні картки.");
        }
    }

}
