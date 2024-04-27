using System.Net;
using BuildingBlocks.Messaging.Events;
using EventBus.Interfaces.Bus;
using Microsoft.AspNetCore.Mvc;
using User.API.Repositories.Interfaces;

namespace User.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<AccountController> _logger;
    private readonly IUserRepository _userRepository;

    public AccountController(IEventBus eventBus, ILogger<AccountController> logger, IUserRepository userRepository)
    {
        _eventBus = eventBus;
        _logger = logger;
        _userRepository = userRepository;
    }
    //TODO Implement Register and Login methods
    //TODO Complete EventBus 
    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] Entities.User entity)
    {
        try
        {
            await _userRepository.Create(entity);

            var eventMessage = new UserLoggedInIntegrationEvent(entity.Id.ToString());
            //await _eventBus.Publish(eventMessage);

            _logger.LogInformation($"User with id: {entity.Id} was successfully registered.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering user.");
        }
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] Entities.User entity)
    {
        try
        {
            var user = await _userRepository.GetByEmail(entity.Email);
            if (user == null)
            {
                _logger.LogError($"User with email: {entity.Email} not found.");
                return NotFound();
            }

            if (!user.Password.Equals(entity.Password))
            {
                _logger.LogError($"User with email: {entity.Email} entered the wrong password.");
                return BadRequest("Invalid password.");
            }

            var eventMessage = new UserLoggedInIntegrationEvent(user.Username);
            //await _eventBus.Publish(eventMessage);

            _logger.LogInformation($"User with id: {user.Id} logged in.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while logging in.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging in.");
        }
    }
}