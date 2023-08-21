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
    public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, List<BasicUserInfoDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _maper;
        public GetUserListRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _maper = mapper;

        }
        public async Task<List<BasicUserInfoDto>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAll();
            return _maper.Map<List<BasicUserInfoDto>>(user);
        }
    }
}
