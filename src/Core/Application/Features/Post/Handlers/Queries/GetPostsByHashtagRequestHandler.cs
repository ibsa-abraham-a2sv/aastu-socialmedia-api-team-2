using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Queries
{
    public class GetPostsByHashtagRequestHandler : IRequestHandler<GetPostsByHashtagRequest, List<PostDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public GetPostsByHashtagRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<List<PostDto>> Handle(GetPostsByHashtagRequest request, CancellationToken cancellationToken)
        {
            var posts = await _unitOfWork.PostRepository.GetPostsByHashtag(request.Tag, request.PageIndex, request.PageSize);
            return _mapper.Map<List<PostDto>>(posts);
        }
    }
}