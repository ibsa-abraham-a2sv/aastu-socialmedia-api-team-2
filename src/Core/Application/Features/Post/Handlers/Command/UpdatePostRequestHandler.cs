using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Command;
using Application.Responses;
using Domain.Post;
using AutoMapper;
using MediatR;
using Application.DTOs.Post.Validators;
using System.Text.RegularExpressions;
using Domain.Hashtag;

namespace Application.Features.Post.Handlers.Command
{
    public class UpdatePostRequestHandler : IRequestHandler<UpdatePostRequest, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
          private readonly IMapper _mapper;

        public UpdatePostRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
               _mapper = mapper;
        }


        public async Task<BaseCommandResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            
        var post = _mapper.Map<Domain.Post.Post>(request.postUpdateDto);

            var existingPost = await _unitOfWork.PostRepository.GetPost(request.postUpdateDto.Id);
              var validator = new UpdatePostDtoValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request.postUpdateDto, cancellationToken);

             if (validationResult.IsValid == false)
            {
               return new BaseCommandResponse(){Id = existingPost.Id, Success = false, Message = validationResult.Errors.Select(q => q.ErrorMessage).ToList()[0]}; 
            }
            if (existingPost != null)
            {
                existingPost.Content = request.postUpdateDto.Content; 

                await _unitOfWork.PostRepository.Update(existingPost);
                
                var regex = new Regex(@"#\w+");
                var hashtagMatches = regex.Matches(existingPost.Content);
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
                        PostId = existingPost.Id,
                        HashtagId = existingHashtag.Id,
                        Hashtag = existingHashtag,
                        Post = existingPost
                    };

                    existingPost.PostHashtags.Add(postHashtag);
                    existingHashtag.PostHashtags.Add(postHashtag);
                }
                
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Successfully updated the post"
                };
            }

            return new BaseCommandResponse
            {
                Success = false,
                Message = "Post not found or not authorized for update"
            };
        }
    }
}
