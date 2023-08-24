using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Command;
using Application.Responses;
using Domain.Post;
using AutoMapper;
using MediatR;

namespace Application.Features.Post.Handlers.Command;

internal sealed class CreatePostRequestHandler : IRequestHandler<CreatePostRequest, BaseCommandResponse> {
    private readonly IUnitOfWork _unitOfWork;
     private readonly IMapper _mapper;
    public CreatePostRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
           _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken) {
  
        var response =  await _unitOfWork.PostRepository.CreatePost(request.postDto);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

    
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new BaseCommandResponse(){Id = response, Success = true, Message = "Successfully added the follow request"}; 
    }
}
