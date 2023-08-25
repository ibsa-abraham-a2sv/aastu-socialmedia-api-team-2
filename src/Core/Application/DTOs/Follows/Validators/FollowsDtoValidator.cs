using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.DTOs.Follows.Validators;

public class FollowsDtoValidator : AbstractValidator<FollowsDto>
{
    private readonly IFollowsRepository _followsRepository;

    public FollowsDtoValidator(IFollowsRepository followsRepository)
    {
        _followsRepository = followsRepository;

        RuleFor(p => p.FollowsId)
            .MustAsync(async (id, token) =>
            {
                var followUserExists = await _followsRepository.Exists(id);
                return true;
            })
            .WithMessage("{PropertyName} does not exist.");
    }
    
    
}