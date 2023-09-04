using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.DTOs.Hashtag.Validators
{
    public class CreateHashtagDtoValidator : AbstractValidator<CreateHashtagDto>
    {
        public CreateHashtagDtoValidator()
        {
            Include(new IHashtagDtoValidator());
        }
    }
}