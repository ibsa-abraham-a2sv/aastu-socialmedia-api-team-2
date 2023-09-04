using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.DTOs.Hashtag.Validators
{
    public class IHashtagDtoValidator : AbstractValidator<IHashtagDto>
    {
        public IHashtagDtoValidator()
        {
                RuleFor(c => c.Tag)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}