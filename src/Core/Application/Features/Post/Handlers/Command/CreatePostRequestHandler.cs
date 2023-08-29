using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Command;
using Application.Responses;
using Domain.Post;
using AutoMapper;
using MediatR;
using Application.DTOs.Post.Validators;
using System.Text.RegularExpressions;
using Domain.Hashtag;

namespace Application.Features.Post.Handlers.Command;

public class CreatePostRequestHandler : IRequestHandler<CreatePostRequest, BaseCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreatePostRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }

    public async Task<BaseCommandResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken)
    {


        var validator = new PostDtoValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(request.postDto);


        if (validationResult.IsValid == false)
        {
            return new BaseCommandResponse() { Success = false, Message = validationResult.Errors.Select(q => q.ErrorMessage).ToList()[0] };
        }


        var post = _mapper.Map<Domain.Post.Post>(request.postDto);
        
        post = await _unitOfWork.PostRepository.Add(post);

        var regex = new Regex(@"#\w+");
        var hashtagMatches = regex.Matches(post.Content);
        foreach (Match match in hashtagMatches)
        {
            string tag = match.Value.TrimStart('#');

            var existingHashtag = await _unitOfWork.HashtagRepository.GetByTag(tag);
            if (existingHashtag == null)
            {
                existingHashtag = await _unitOfWork.HashtagRepository.Add(new Hashtag { Tag = tag });
            }

            var postHashtag = new PostHashtag
            {
                PostId = post.Id,
                HashtagId = existingHashtag.Id,
                Hashtag = existingHashtag,
                Post = post
            };

            post.PostHashtags.Add(postHashtag);
            existingHashtag.PostHashtags.Add(postHashtag);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return new BaseCommandResponse() { Id = post.Id, Success = true, Message = "Successfully created" };
    }
}
