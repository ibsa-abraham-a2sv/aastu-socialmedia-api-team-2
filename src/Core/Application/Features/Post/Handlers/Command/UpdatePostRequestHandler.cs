using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Command;
using Application.Responses;
using Domain.Post;
using AutoMapper;
using MediatR;
using Application.DTOs.Post.Validators;

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

                await _unitOfWork.PostRepository.Update(post);
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
