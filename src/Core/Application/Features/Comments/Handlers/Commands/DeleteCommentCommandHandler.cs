using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Comments.Requests.Commands;
using AutoMapper;
using MediatR;

namespace Application.Features.Comments.Handlers.Commands
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.Get(request.Id);

            if (comment == null)
                throw new NotFoundException(nameof(comment), request.Id);

            await _unitOfWork.CommentRepository.Delete(comment);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}