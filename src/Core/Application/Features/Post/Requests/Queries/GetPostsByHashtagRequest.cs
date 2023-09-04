using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Requests.Queries
{
    public class GetPostsByHashtagRequest : IRequest<List<PostDto>>
    {
        public required string Tag { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}