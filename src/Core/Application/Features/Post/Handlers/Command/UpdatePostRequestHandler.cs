using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Command;
using Application.Responses;
using Domain.Post;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Command
{
    internal sealed class UpdatePostRequestHandler : IRequestHandler<UpdatePostRequest, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePostRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(UpdatePostRequest request, CancellationToken cancellationToken)
        {
            var existingPost = await _unitOfWork.PostRepository.GetPost(request.postUpdateDto.Id);

            if (existingPost != null && existingPost.UserId == request.postUpdateDto.UserId)
            {
                existingPost.Content = request.postUpdateDto.Content; 

          await _unitOfWork.PostRepository.UpdatePost(request.postUpdateDto);
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
