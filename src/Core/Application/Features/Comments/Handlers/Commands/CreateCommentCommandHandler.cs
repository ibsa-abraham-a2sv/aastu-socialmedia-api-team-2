using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Comment;
using Application.DTOs.Comment.Validators;
using Application.Features.Comments.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Application.Responses;
using AutoMapper;
using Domain.Comment;
using MediatR;
using Application.Constants;

namespace Application.Features.Comments.Handlers.Queries
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<BaseCommandResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCommentDtoValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request.CreateCommentDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var comment = _mapper.Map<Comment>(request.CreateCommentDto);
                comment.UserId = request.UserId;
                
                comment = await _unitOfWork.CommentRepository.Add(comment);
                await _unitOfWork.SaveChangesAsync();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = comment.Id;
            }

            return response;
        }
    }
}