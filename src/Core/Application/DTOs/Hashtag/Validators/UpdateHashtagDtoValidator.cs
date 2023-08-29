using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.DTOs.Hashtag.Validators
{
    public class UpdateHashtagDtoValidator : AbstractValidator<UpdateHashtagDto>
    {
        public UpdateHashtagDtoValidator()
        {
            Include(new IHashtagDtoValidator());
            
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}