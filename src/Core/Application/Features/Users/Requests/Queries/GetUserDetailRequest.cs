using Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Requests.Queries
{
    public class GetUserDetailRequest : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
