using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Application.DTOs.Comment;
using Application.Features.Comments.Requests.Commands;
using Application.Features.Comments.Requests.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        [HttpGet("comments/{commentId:Guid}")]
        public async Task<ActionResult<CommentDto>> GetComment(Guid commentId)
        {
            var comment = await _mediator.Send(new GetCommentDetailRequest() { Id = commentId });
            return Ok(comment);
        }

        [HttpGet("posts/{postId:Guid}/comments")]
        public async Task<ActionResult<List<CommentsOfPostDto>>> GetCommentsOfPost(Guid postId, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("Invalid page index or page size.");
            }

            var comments = await _mediator.Send(new GetCommentsByPostIdRequest { PostId = postId, PageIndex = pageIndex, PageSize = pageSize });
            return Ok(comments);
        }

        [HttpGet("comments")]
        public async Task<ActionResult<List<CommentsOfUserDto>>> GetCommentsOfUser(int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("Invalid page index or page size.");
            }
            
            var id = _httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid) ?? throw new UnauthorizedAccessException("User is not authorized.");
            var userId = new Guid(id);
            var comments = await _mediator.Send(new GetCommentsByUserIdRequest { UserId = userId, PageIndex = pageIndex, PageSize = pageSize });
            return Ok(comments);
        }

        [HttpPost("comments")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCommentDto comment)
        {
            var id = _httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid) ?? throw new UnauthorizedAccessException("User is not authorized.");
            var userId = new Guid(id);
            var command = new CreateCommentCommand { CreateCommentDto = comment, UserId = userId };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        [HttpPut("comments")]
        public async Task<ActionResult> Put([FromBody] UpdateCommentDto comment)
        {
            var id = _httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid) ?? throw new UnauthorizedAccessException("User is not authorized.");
            var userId = new Guid(id);
            var command = new UpdateCommentCommand { UpdateCommentDto = comment, RequestingUserId = userId };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("comments/{commentId:Guid}")]
        public async Task<ActionResult> Delete(Guid commentId)
        {
            var id = _httpContextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid) ?? throw new UnauthorizedAccessException("User is not authorized.");
            var userId = new Guid(id);
            var command = new DeleteCommentCommand { Id = commentId, RequestingUserId = userId};
            await _mediator.Send(command);
            return NoContent();
        }
    }
}