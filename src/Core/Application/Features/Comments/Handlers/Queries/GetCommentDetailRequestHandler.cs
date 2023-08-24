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
    public class GetCommentDetailRequestHandler : IRequestHandler<GetCommentDetailRequest, CommentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public GetCommentDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<CommentDto> Handle(GetCommentDetailRequest request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.Get(request.Id);
            return _mapper.Map<CommentDto>(comment);
        }
    }
}