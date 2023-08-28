using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Comments.Requests.Commands
{
    public class DeleteCommentCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid RequestingUserId { get; set; }
    }
}