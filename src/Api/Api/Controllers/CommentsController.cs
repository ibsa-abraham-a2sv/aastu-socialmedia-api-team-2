using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Comment;
using Application.Features.Comments.Requests.Commands;
using Application.Features.Comments.Requests.Queries;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("comments/{id:Guid}")]
        public async Task<ActionResult<CommentDto>> GetComment(Guid id)
        {
            var comment = await _mediator.Send(new GetCommentDetailRequest() { Id = id });
            return Ok(comment);
        }

        [HttpGet("posts/{postId:Guid}/comments")]
        public async Task<ActionResult<List<CommentsOfPostDto>>> GetCommentsOfPost(Guid postId, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("Invalid page index or page size.");
            }

            var comments = await _mediator.Send(new GetCommentsByPostIdRequest { PostId = postId, pageIndex = pageIndex, pageSize = pageSize });
            return Ok(comments);
        }

        [HttpGet("users/{userId:Guid}/comments")]
        public async Task<ActionResult<List<CommentsOfUserDto>>> GetCommentsOfUser(Guid userId, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("Invalid page index or page size.");
            }
            
            var comments = await _mediator.Send(new GetCommentsByUserIdRequest { UserId = userId, pageIndex = pageIndex, pageSize = pageSize });
            return Ok(comments);
        }

        [HttpPost("comments")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCommentDto comment)
        {
            var command = new CreateCommentCommand { CreateCommentDto = comment };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        [HttpPut("comments")]
        public async Task<ActionResult> Put([FromBody] UpdateCommentDto comment)
        {
            var command = new UpdateCommentCommand { UpdateCommentDto = comment };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("comments/{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteCommentCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}