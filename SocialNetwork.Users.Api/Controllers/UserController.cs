using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SocialNetwork.Users.Application.DTOs;
using SocialNetwork.Users.Application.Interfaces;
using MySql.Data.MySqlClient;

namespace SocialNetwork.Users.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("User/Create")]
    [SwaggerOperation(Summary = "Create User")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid data received for user creation.");
            return BadRequest(ModelState);
        }

        try
        {
            var userId = await _userService.CreateUserAsync(userDto);
            if (userId == 0)
                return StatusCode(500, "User could not be created.");

            return CreatedAtAction(nameof(GetUserById), new { id = userId }, userDto);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error creating user: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid request",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex, "Database error when creating user: {Message}", ex.Message);
            return StatusCode(500, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Database Error",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when creating user: {Message}", ex.Message);
            return StatusCode(500, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected Error",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
    }

    [HttpGet("User/{id}")]
    [SwaggerOperation(Summary = "Get Specific User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetUserById(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Invalid user ID: {Id}", id);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid User ID",
                Detail = "The provided user ID is invalid.",
                Instance = HttpContext.Request.Path
            });
        }

        try
        {
            UserDto userDto = await _userService.GeUserByIdAsync(id);
            if (userDto == null)
            {
                _logger.LogInformation("User with ID {Id} not found", id);
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(userDto);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error fetching user with ID {Id}: {Message}", id, ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid request",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex, "Database error fetching user with ID {Id}: {Message}", id, ex.Message);
            return StatusCode(500, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Database Error",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error fetching user with ID {Id}: {Message}", id, ex.Message);
            return StatusCode(500, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected Error",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
    }

    [HttpGet("Users")]
    [SwaggerOperation(Summary = "Get All Users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllUsers()
    {
        try
        {
            IEnumerable<ListUsersDto> usersDto = await _userService.GetAllUsersAsync();
            if (usersDto == null || !usersDto.Any())
            {
                _logger.LogInformation("No users found in the database.");
                return NotFound("No users are registered in the system.");
            }

            return Ok(usersDto);
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex, "Database error when fetching the list of users: {Message}", ex.Message);
            return StatusCode(500, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Database Error",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when fetching the list of users: {Message}", ex.Message);
            return StatusCode(500, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Unexpected Error",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
    }

    //[HttpPut("{id}")]
    //[SwaggerOperation(Summary = "Update User")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public IActionResult UpdateUserId(int id, [FromBody] UpdateUserDto userDto)
    //{
    //    return NoContent();
    //}

    //[HttpPatch("{id}")]
    //[SwaggerOperation(Summary = "Create Partial User")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public IActionResult UpdatePartialUserId(int id, [FromBody] JsonPatchDocument<UpdateUserDto> userDto)
    //{
    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //[SwaggerOperation(Summary = "Delete Specific User")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public IActionResult DeleteUserId(int id)
    //{
    //    return NoContent();
    //}

    //[HttpDelete]
    //[SwaggerOperation(Summary = "Delete All User")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //public IActionResult DeleteAllUsers()
    //{
    //    return NoContent();
    //}
}