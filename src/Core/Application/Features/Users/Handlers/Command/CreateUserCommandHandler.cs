using Application.Contracts;
using Application.DTOs.User.Validators;
using Application.Features.Users.Requests.Command;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _maper;
        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _maper = mapper;

        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUserProfileDto);
            if (validationResult.IsValid == false)
                throw new Exception();
            var user = _maper.Map<User>(request.CreateUserProfileDto);
            user = await _userRepository.Add(user);
            return user.Id;
        }
    }
}
