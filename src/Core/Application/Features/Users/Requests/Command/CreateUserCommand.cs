using Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Requests.Command
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserProfileDto CreateUserProfileDto { get; set; }
    }
}
