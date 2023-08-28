using System.Security.Claims;
using Application.Constants;
using Application.DTOs.Unlikes;
using Application.Features.Unlikes.Requests.Commands;
using Application.Features.Unlikes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UnlikesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    public UnlikesController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
        _mediator = mediator;
    }

    [HttpGet("{unlikesId:guid}")]
    public async Task<ActionResult<bool>> CheckUserUnlikes(Guid unlikesId)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
                
        var response = await _mediator.Send(new CheckIfUserUnlikesRequest(new Guid(id), unlikesId));
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUnlike([FromBody] UnlikesDto unlikes )
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var response = await _mediator.Send(new CreateUnlikeRequest(new Guid(id), unlikes.UnlikesId));

        if (response == null) return BadRequest("User already likes this post");

        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<Unit>> RemoveUnlike([FromBody] UnlikesDto unlikes)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var response = await _mediator.Send(new RemoveUnlikeRequest(new Guid(id), unlikes.UnlikesId));
        return NoContent();
    }
    
    [HttpGet("{unlikesId:guid}/count")]
    public async Task<ActionResult<int>> GetLikesCount(Guid unlikesId)
    {
        return Ok(await _mediator.Send(new GetUnlikesCountRequest(unlikesId)));
    }
}