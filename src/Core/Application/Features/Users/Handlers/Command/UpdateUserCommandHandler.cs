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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _maper;
        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _maper = mapper;

        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UpdateUserProfileDto.Id);
            _maper.Map(request.UpdateUserProfileDto, user);
            await _userRepository.Update(user);
            return Unit.Value;
        }
    }
}
