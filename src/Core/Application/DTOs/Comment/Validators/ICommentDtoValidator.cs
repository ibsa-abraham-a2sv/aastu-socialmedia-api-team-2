using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.DTOs.Comment.Validators
{
    public class ICommentDtoValidator : AbstractValidator<ICommentDto>
    {
        public ICommentDtoValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}