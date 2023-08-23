using Application.Contracts.Identity;
using Application.DTOs.User;
using Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize]
public class ProfileManager : ControllerBase
{
    private readonly IUserService _userService;

    public ProfileManager(IUserService userService)
    {
        _userService = userService;
    }

    // get user by id
    [HttpGet("api/[controller]/[action]")]
    // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<User>> GetUserById(string id)
    {
        return Ok(await _userService.GetUserById(id));
    }
    
    // update user
    [HttpPut("api/[controller]/[action]")]
    public async Task<ActionResult> UpdateUser(UpdateUserDto user)
    {
        await _userService.UpdateUser(user);
        return NoContent();
    }
    
    // delete user
    [HttpDelete("api/[controller]/[action]")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }
    
    // upload profile picture
    [HttpPost("api/[controller]/[action]")]
    public async Task<ActionResult> UploadProfilePicture(string userId, IFormFile file)
    {
        if (file.Length > 0)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            await _userService.UploadProfilePicture(userId, memoryStream.ToArray());
            return NoContent();
        }

        return BadRequest("No file was uploaded");
    }
    
    // get profile picture
    [HttpGet("api/[controller]/[action]")]
    public async Task<ActionResult> GetProfilePicture(string userId)
    {
        var picture = await _userService.GetProfilePicture(userId);
        return Ok(File(picture, "image/jpeg"));
    }
    
    

}