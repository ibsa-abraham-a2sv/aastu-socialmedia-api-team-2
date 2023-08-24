using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Comment;
using MediatR;

namespace Application.Features.Comments.Requests.Queries
{
    public class GetCommentsByPostIdRequest : IRequest<List<CommentsOfPostDto>>
    {
        public Guid PostId { get; set; }
    }
}