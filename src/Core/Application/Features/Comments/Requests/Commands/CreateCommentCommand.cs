using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Comment;
using Application.Responses;
using MediatR;

namespace Application.Features.Comments.Requests.Commands
{
    public class CreateCommentCommand : IRequest<BaseCommandResponse>
    {
        public required CreateCommentDto CreateCommentDto {get; set;}
        public Guid UserId {get; set;}
    }
}