using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Requests.Command
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
