using System.Net;
using Microsoft.AspNetCore.Mvc;
using User.API.Data;
using User.API.Repositories.Interfaces;

namespace User.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;
    
    public UserController(UserContext context, IUserRepository userRepository, ILogger<UserController> logger)
    {
        _context = context;
        _userRepository = userRepository;
        _logger = logger;
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(typeof(IEnumerable<Entities.User>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Entities.User>>> GetAll()
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
    
    [HttpGet("GetProductByIdAsync {id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.User>> GetProductByIdAsync(Guid id)
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
    [ProducesResponseType(typeof(Entities.User), (int)HttpStatusCode.OK)]
    public async Task<ActionResult> CreateUser([FromBody] Entities.User user)
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
                _logger.LogError("An error occurred while creating user", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating user");
            }
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(typeof(Entities.User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateUser([FromBody] Entities.User user)
        {
            try
            {
                await _userRepository.Update(user);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating user", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating user");
            }
        }

        [HttpDelete("DeleteUser {id}")]
        [ProducesResponseType(typeof(Entities.User), (int)HttpStatusCode.OK)]
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
                    "An error occurred while deleting user", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting user");
            }
        }
}