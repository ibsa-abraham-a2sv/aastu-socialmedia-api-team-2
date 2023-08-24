using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Command;
using Application.Responses;
using Domain.Post;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Command
{
    internal sealed class DeletePostRequestHandler : IRequestHandler<DeletePostRequest, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            var existingPost = await _unitOfWork.PostRepository.GetPost(request.postDeleteDto.PostId);
           

            if (existingPost != null && existingPost.UserId == request.postDeleteDto.UserId)
            {
                await _unitOfWork.PostRepository.DeletePost(request.postDeleteDto);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Successfully deleted the post"
                };
            }

            return new BaseCommandResponse
            {
                Success = false,
                Message = "Post not found or not authorized for deletion"
            };
        }
    }
}
