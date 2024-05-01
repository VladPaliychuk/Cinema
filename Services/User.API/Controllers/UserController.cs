using System.Net;
using Microsoft.AspNetCore.Mvc;
using User.BLL.DTOs;
using User.BLL.Services;
using User.DAL.Repositories.Interfaces;

namespace User.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;
    
    public UserController(
        IUserRepository userRepository, 
        ILogger<UserController> logger,
        UserService userService
        )
    {
        _userService = userService;
        _userRepository = userRepository;
        _logger = logger;
    }
    
    [HttpPost("Register")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> RegisterUser([FromBody] UserDto user)
    {
        try
        {
            bool success = await _userService.Register(user);
            return Ok(success);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while registering user - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering user");
        }
    }
    
    [HttpPost("Login")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> Login([FromBody] LoginDto login)
    {
        try
        {
            bool success = await _userService.Login(login);
            return Ok(success);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while logging in user - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logging in user");
        }
    }


    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(IEnumerable<DAL.Entities.User>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<DAL.Entities.User>>> GetAll()
    {
        try
        {
            var result = await _userRepository.GetAll();
            _logger.LogInformation("All users were successfully retrieved.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting all users.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting all users.");
        }
    }
    
    [HttpGet]
    [Route("[action]/{id}", Name = "GetUserById")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(DAL.Entities.User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DAL.Entities.User>> GetUserById(Guid id)
    {
        try
        {
            var result = await _userRepository.GetById(id);
            _logger.LogInformation($"Returned user with id: {id}");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred while getting user by id: {id}", ex);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while getting user by id");
        }
    }
    
    [HttpPost("CreateUser")]
    [ProducesResponseType(typeof(DAL.Entities.User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateUser([FromBody] DAL.Entities.User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("User object is invalid");
                    return BadRequest("Invalid model object");
                }

                await _userRepository.Create(user);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating user - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating user");
            }
        }

    [HttpPut("UpdateUser")]
    [ProducesResponseType(typeof(DAL.Entities.User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> UpdateUser([FromBody] DAL.Entities.User user)
    {
        try
        {
            await _userRepository.Update(user);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating user - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating user");
        }
    }

    [HttpDelete("DeleteUser {id}")]
    [ProducesResponseType(typeof(DAL.Entities.User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userRepository.Delete(id);
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                $"An error occurred while deleting user - {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting user");
        }
    }
}