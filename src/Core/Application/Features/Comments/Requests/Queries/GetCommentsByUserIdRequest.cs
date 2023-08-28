using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Comment;
using MediatR;

namespace Application.Features.Comments.Requests.Queries
{
    public class GetCommentsByUserIdRequest : IRequest<List<CommentsOfUserDto>>
    {
        public Guid UserId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}