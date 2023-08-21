using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User.Validators
{
    public class CreateUserProfileDtoValidator : AbstractValidator<CreateUserProfileDto>
    {
        public CreateUserProfileDtoValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is Required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters");
            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is Required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters");
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("{PropertyName} is Required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters");
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("Invalid {PropertyName} format.");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(8).WithMessage("{PropertyName} must have a minimum of {MinLength} characters.");
            RuleFor(p => p.Birthday)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .LessThan(DateTime.Now).WithMessage("{PropertyName} must be in the past.");

        }
    }
}
