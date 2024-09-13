using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SocialNetwork.Users.Application.DTOs;
using SocialNetwork.Users.Application.Interfaces;
using MySql.Data.MySqlClient;

namespace SocialNetwork.Users.Api.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    //[HttpPost]
    //[SwaggerOperation(Summary = "Create User")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //public IActionResult CreateUser([FromBody] CreateUserDto userDto)
    //{
    //    return CreatedAtAction(nameof(GetUserId), new { });
    //}

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get Specific User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        try
        {
            UserDto userDto = await _userService.GeUserByIdAsync(id);
            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
        catch (MySqlException ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "Erro interno do servidor.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "Erro inesperado.");
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get All Users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ListUsersDto>>> GetAllUsers()
    {
        IEnumerable<ListUsersDto> usersDto = await _userService.GetAllUsersAsync();
        return Ok(usersDto);
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