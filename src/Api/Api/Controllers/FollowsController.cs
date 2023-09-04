using System.Security.Claims;
using Application.Constants;
using Application.Contracts.Identity;
using Application.DTOs.Follows;
using Application.DTOs.Notification;
using Application.Exceptions;
using Application.Features.Follows.Requests.Commands;
using Application.Features.Follows.Requests.Queries;
using Application.Features.Notifications.Requests;
using Application.Models.Identity;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[Route("api/user/[controller]")]
[ApiController]
// [Authorize]
public class FollowsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserService _userService;

    public FollowsController(IMediator mediator, IHttpContextAccessor contextAccessor, IUserService userService)
    {
        _contextAccessor = contextAccessor;
        _mediator = mediator;
        _userService = userService;
    }

    [HttpGet("followers")]
    public async Task<ActionResult<List<FollowsReturnDto>>> GetFollowers()
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var followers = await _mediator.Send(new GetFollowersRequest(new Guid(id)));
        return Ok(followers);
    }

    [HttpGet("following")]
    public async Task<ActionResult<FollowsReturnDto>> GetFollowing()
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var following = await _mediator.Send(new GetFollowingRequest(new Guid(id)));
        return Ok(following);
    }

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateFollowing([FromBody] FollowsDto follows)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");


        var validateUserExists = await _userService.Exists(follows.FollowsId.ToString());

        if (!validateUserExists) throw new NotFoundException("user", follows.FollowsId);

        if (new Guid(id) == follows.FollowsId) throw new BadRequestException("user cannot follow themselves");
        
        var response = await _mediator.Send(new CreateFollowingRequest(new Guid(id), follows.FollowsId));

        if (response.Success == true) {
            var user = await _userService.GetUserById(id);
            var Gid = follows.FollowsId;
            var notificationDto = new CreateNotificationDto();
            notificationDto.UserId = Gid;
            notificationDto.Message = $"{user.UserName} followed you recently";
            notificationDto.IsRead = false;

            var command = new CreateNotificationCommand { CreateNotificationDto = notificationDto };
            var res = await _mediator.Send(command);

        }
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<Unit>> RemoveFollowing([FromBody] FollowsDto follows)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        await _mediator.Send(new RemoveFollowingRequest(new Guid(id), follows.FollowsId));
        return NoContent();
    }
    
    [HttpGet("following/count")]
    public async Task<ActionResult<int>> GetFollowingCount()
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var following = await _mediator.Send(new GetFollowingCountRequest(new Guid(id)));
        return Ok(following);
    }
    
    [HttpGet("followers/count")]
    public async Task<ActionResult<int>> GetFollowersCount()
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var following = await _mediator.Send(new GetFollowersCountRequest(new Guid(id)));
        return Ok(following);
    }
    
    [HttpGet("check/{followsId:guid}")]
    public async Task<ActionResult<bool>> CheckUserFollows(Guid followsId)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        return Ok(await _mediator.Send(new CheckIfUserFollowsRequest(new Guid(id), followsId)));
    }
}
