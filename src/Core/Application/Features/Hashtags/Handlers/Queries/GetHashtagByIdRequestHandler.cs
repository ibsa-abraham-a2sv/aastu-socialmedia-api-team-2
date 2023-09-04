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
    public class GetHashtagByIdRequestHandler : IRequestHandler<GetHashtagByIdRequest, HashtagDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public GetHashtagByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public Task<HashtagDto> Handle(GetHashtagByIdRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}