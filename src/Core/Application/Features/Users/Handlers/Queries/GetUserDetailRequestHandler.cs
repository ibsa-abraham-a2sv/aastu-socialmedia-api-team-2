using Application.Contracts;
using Application.DTOs.User;
using Application.Features.Users.Requests.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers.Queries
{
    public class GetUserDetailRequestHandler : IRequestHandler<GetUserDetailRequest, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _maper;
        public GetUserDetailRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _maper = mapper;

        }
        public async Task<UserDto> Handle(GetUserDetailRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id);
            return _maper.Map<UserDto>(user);
        }
    }
}
