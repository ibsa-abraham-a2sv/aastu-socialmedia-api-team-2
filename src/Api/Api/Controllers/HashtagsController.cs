using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Hashtag;
using Application.DTOs.Post;
using Application.Features.Hashtags.Requests.Queries;
using Application.Features.Post.Requests.Queries;
using Domain.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class HashtagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HashtagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("posts/hashtag/{tag}/{pageIndex}/{pageSize}")]
        public async Task<ActionResult<PostDto>> GetPostsByHashtag(string tag, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("Invalid page index or page size.");
            }
            var request = new GetPostsByHashtagRequest{Tag = tag, PageIndex = pageIndex, PageSize = pageSize};
            var posts = await _mediator.Send(request);
            return Ok(posts);
        }

        [HttpGet("hashtags/post/{postId}/{pageIndex}/{pageSize}")]
        public async Task<ActionResult<HashtagDto>> GetHashtagsByPostId(Guid postId, int pageIndex = 1, int pageSize = 10)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest("Invalid page index or page size.");
            }
            var request = new GetHashtagsByPostIdRequest{ PostId = postId, PageIndex = pageIndex, PageSize = pageSize};
            var posts = await _mediator.Send(request);
            return Ok(posts);
        }
    }
}