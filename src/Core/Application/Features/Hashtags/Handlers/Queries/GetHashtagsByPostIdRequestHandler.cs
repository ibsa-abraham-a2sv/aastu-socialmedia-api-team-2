using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Hashtag;
using Application.Features.Hashtags.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Hashtags.Handlers.Queries
{
    public class GetHashtagsByPostIdRequestHandler : IRequestHandler<GetHashtagsByPostIdRequest, List<HashtagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public GetHashtagsByPostIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<List<HashtagDto>> Handle(GetHashtagsByPostIdRequest request, CancellationToken cancellationToken)
        {
            var hashtags = await _unitOfWork.HashtagRepository.GetHashtagsByPostId(request.PostId, request.PageIndex, request.PageSize);
            return _mapper.Map<List<HashtagDto>>(hashtags);
        }
    }
}