using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Comment.Validators;
using Application.Exceptions;
using Application.Features.Comments.Requests.Commands;
using Application.Responses;
using AutoMapper;
using Domain.Comment;
using MediatR;

namespace Application.Features.Comments.Handlers.Commands
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCommentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateCommentDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);
            
            var comment = await _unitOfWork.CommentRepository.Get(request.UpdateCommentDto.Id);

            if (comment is null)
                throw new NotFoundException(nameof(comment), request.UpdateCommentDto.Id);
            
            if (comment.UserId != request.RequestingUserId)
                throw new UnauthorizedAccessException("User is not authorized.");
            
            _mapper.Map(request.UpdateCommentDto, comment);

            await _unitOfWork.CommentRepository.Update(comment);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;            
        }
    }
}