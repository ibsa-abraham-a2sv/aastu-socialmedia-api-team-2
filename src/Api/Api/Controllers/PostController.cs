using System.Security.Claims;
using Application.Constants;
using Application.Contracts.Identity;
using Application.DTOs.Notification;
using Application.DTOs.Post;
using Application.Features.Follows.Requests.Queries;
using Application.Features.Notifications.Requests;
using Application.Features.Post.Requests.Command;
using Application.Features.Post.Requests.Queries;
using Application.Responses;
using Domain.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Persistence.Service;


namespace Api.Controllers;

[Route("api")]
[ApiController]
// [Authorize]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

     private readonly IHttpContextAccessor _contextAccessor;

    private readonly IUserService _userService;
    private readonly IHubContext<NotificationHub> _notificationHubContext;

    public PostController(IMediator mediator, IHttpContextAccessor contextAccessor, IUserService userService, IHubContext<NotificationHub> notificationHubContext)
    {
        _contextAccessor = contextAccessor;
        _mediator = mediator;
        _userService = userService;
        _notificationHubContext = notificationHubContext;
    }

    [HttpGet("posts/{pageIndex}/{pageSize}")]
    public async Task<ActionResult<List<PostDto>>> GetPosts(int pageIndex = 1, int pageSize = 10)
    {
         var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        if (pageIndex < 1 || pageSize < 1)
    {
        return BadRequest("Invalid page index or page size.");
    }
        var posts = await _mediator.Send(new GetPostsRequest(pageIndex, pageSize));
        return Ok(posts);
    }
[HttpGet("posts/{postId}")]
public async Task<ActionResult<PostDto>> GetPostById(Guid postId)
{
    var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
    var request = new GetPostRequest(postId);
    var post = await _mediator.Send(request);

    if (post == null)
    {
        return NotFound(); 
    }

    return Ok(post);
}
[HttpGet("posts/user/{pageIndex}/{pageSize}")]
public async Task<ActionResult<PostDto>> GetPostsByUserId(int pageIndex = 1, int pageSize = 10)
{
   
    var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);

        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
    if (pageIndex < 1 || pageSize < 1)
    {
        return BadRequest("Invalid page index or page size.");
    }
    var request = new GetPostsByUserIdRequest(new Guid(id), pageIndex, pageSize);
    var posts = await _mediator.Send(request);
    return Ok(posts);
}
[HttpPost("posts")]
public async Task<ActionResult<BaseCommandResponse>> CreatePost(string content)
{
    var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
    
    if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
    var request = new CreatePostRequest(new PostDto{
        UserId= new Guid(id),   
        Content= content
    });


    var response = await _mediator.Send(request);

    if (response.Success)
    {
            var user = await _userService.GetUserById(id);
            var followers = await _mediator.Send(new GetFollowersRequest(new Guid(id)));
            var notificationDto = new CreateNotificationDto();
            foreach (var follower in followers)
            {
                notificationDto.UserId = follower.UserId;
                notificationDto.Message = $"{user.UserName} Posted recently";
                notificationDto.IsRead = false;

                var command = new CreateNotificationCommand { CreateNotificationDto = notificationDto };
                var res = await _mediator.Send(command);
                
                // send notification to a user using it's connection id
                var followingUser = await _userService.GetUserById(follower.UserId.ToString());
                if(followingUser.ConnectionId != null)
                    await _notificationHubContext.Clients.Client(followingUser.ConnectionId).SendAsync("ReceiveNotification", notificationDto.Message);
            }
            return Ok(response);
    }

    return BadRequest(response);
}
[HttpPatch("posts")]
public async Task<ActionResult<BaseCommandResponse>> UpdatePost(Guid postId, string Content)
{
    var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
    var request = new UpdatePostRequest(new UpdatePostDto{
        Id= postId,
        Content= Content
    });

    var response = await _mediator.Send(request);

    if (response.Success)
    {
        return Ok(response);
    }

    return BadRequest(response);
}
[HttpDelete("posts")]
public async Task<ActionResult<BaseCommandResponse>> DeletePost(Guid postId)
{
    var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
    var request = new DeletePostRequest(new Guid(id), postId);

    var response = await _mediator.Send(request);

    if (response.Success)
    {
        return Ok(response);
    }

    return BadRequest(response);
}
[HttpGet("following/posts")]
public async Task<ActionResult<PostDto>> GetFollowingPosts()
{
    var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
    var request = new GetFollowingPostsRequest(new Guid(id));
    var posts = await _mediator.Send(request);
    return Ok(posts);
}

   
}