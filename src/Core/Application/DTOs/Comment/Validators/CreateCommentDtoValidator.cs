using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.DTOs.Comment.Validators
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommentDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Include(new ICommentDtoValidator());

            // RuleFor(c => c.PostId)
            // .MustAsync(async (id, token) => {
            //     var postExists = await _unitOfWork.PostRepository.Exists(id);
            //     return postExists;
            // })
            // .WithMessage("{ProperyName} does not exist.");
            // RuleFor(c => c.UserId)
            // .MustAsync(async (id, token) => {
            //     var userExists = await _unitOfWork.UserRepository.Exists(id);
            //     return userExists;
            // })
            // .WithMessage("{ProperyName} does not exist.");
        }
    }
}