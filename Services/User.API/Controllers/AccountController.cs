using System.Net;
using EventBus.Interfaces.Bus;
using Microsoft.AspNetCore.Mvc;

namespace User.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IEventBus eventBus, ILogger<AccountController> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }
    //TODO Implement Register and Login methods
    //TODO Complete EventBus 
    [HttpPost("Register")]
    [ProducesResponseType(typeof(UserCreatedEvent), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
    {
        try
        {
            await _userRepository.Create(user);

            var eventMessage = new UserLoggedInIntegrationEvent(user.UserId);
            await _eventBus.Publish(@event);

            _logger.LogInformation($"User with id: {user.Id} was successfully registered.");
            return Ok(@event);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering user.");
        }
    }
    
    [HttpPost("Login")]
    [ProducesResponseType(typeof(Entities.User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> Login([FromBody] Entities.User user)
    {
        try
        {
            //var user = await _userRepository.GetByEmail(command.Email);
            if (user == null)
            {
                _logger.LogError($"User with email: {command.Email} not found.");
                return NotFound();
            }

            if (!user.Password.Equals(command.Password))
            {
                _logger.LogError($"User with email: {command.Email} entered the wrong password.");
                return BadRequest("Invalid password.");
            }

            var eventMessage = new UserLoggedInIntegrationEvent(user.Username);
            await _eventBus.Publish(eventMessage);

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