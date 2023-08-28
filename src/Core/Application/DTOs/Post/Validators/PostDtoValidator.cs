using FluentValidation;
using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DTOs.Post.Validators
{
    public class PostDtoValidator : AbstractValidator<PostDto>
    {
        
      private readonly IUnitOfWork _unitOfWork;
        public PostDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(p => p.Content)
    .MinimumLength(1) 
    .WithMessage("{PropertyName} is required");
        }
    }
}