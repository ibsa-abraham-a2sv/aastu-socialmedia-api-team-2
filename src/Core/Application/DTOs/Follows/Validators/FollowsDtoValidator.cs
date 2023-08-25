using Application.Contracts.Identity;
using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.DTOs.Follows.Validators;

public class FollowsDtoValidator : AbstractValidator<FollowsDto>
{
    public FollowsDtoValidator(IUserService userService)
    {
        RuleFor(p => p.FollowsId)
            .MustAsync(async (id, token) => await userService.Exists(id.ToString()))
            .WithMessage("{PropertyName} does not exist.");
    }
}