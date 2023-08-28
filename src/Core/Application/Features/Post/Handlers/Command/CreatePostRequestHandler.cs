using Application.Contracts.Persistence;
using Application.Features.Post.Requests.Command;
using Application.Responses;
using Domain.Post;
using AutoMapper;
using MediatR;
using Application.DTOs.Post.Validators;

namespace Application.Features.Post.Handlers.Command;

public class CreatePostRequestHandler : IRequestHandler<CreatePostRequest, BaseCommandResponse> {
    private readonly IUnitOfWork _unitOfWork;
     private readonly IMapper _mapper;
    public CreatePostRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) {
        _unitOfWork = unitOfWork;
           _mapper = mapper;
           
    }

    public async Task<BaseCommandResponse> Handle(CreatePostRequest request, CancellationToken cancellationToken) {
      
        var post = _mapper.Map<Domain.Post.Post>(request.postDto);
        var response =  await _unitOfWork.PostRepository.CreatePost(post);
         var validator = new PostDtoValidator(_unitOfWork);
            var validationResult = await validator.ValidateAsync(request.postDto);
          
             if (validationResult.IsValid == false)
            {
               return new BaseCommandResponse(){Id = response, Success = false, Message = validationResult.Errors.Select(q => q.ErrorMessage).ToList()[0]}; 
            }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new BaseCommandResponse(){Id = response, Success = true, Message = "Successfully created"}; 
    }
}
