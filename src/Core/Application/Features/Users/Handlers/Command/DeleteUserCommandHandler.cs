using Application.Contracts;
using Application.Features.Users.Requests.Command;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers.Command
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _maper;
        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _maper = mapper;

        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id);
            await _userRepository.Delete(user.Id);
            return Unit.Value;
        }
    }
}
