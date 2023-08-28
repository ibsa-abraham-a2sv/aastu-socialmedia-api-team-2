using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Comment;
using Application.Features.Comments.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Comments.Handlers.Queries
{
    public class GetCommentsByUserIdRequestHandler : IRequestHandler<GetCommentsByUserIdRequest, List<CommentsOfUserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public GetCommentsByUserIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<List<CommentsOfUserDto>> Handle(GetCommentsByUserIdRequest request, CancellationToken cancellationToken)
        {
            var comments = await _unitOfWork.CommentRepository.GetCommentsByUserId(request.UserId, request.PageIndex, request.PageSize);
            return _mapper.Map<List<CommentsOfUserDto>>(comments);
        }
    }
}