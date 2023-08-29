using Application.Contracts.Persistence;
using Application.DTOs.Comment.Validators;
using Application.Features.Comments.Requests.Commands;
using Application.Responses;
using AutoMapper;
using Domain.Comment;
using MediatR;

namespace Application.Features.Comments.Handlers.Commands
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<BaseCommandResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCommentDtoValidator(_unitOfWork);
            
            var validationResult = await validator.ValidateAsync(request.CreateCommentDto, cancellationToken);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var comment = _mapper.Map<Comment>(request.CreateCommentDto);
                comment.UserId = request.UserId;
                
                var post = await _unitOfWork.PostRepository.GetPost(comment.PostId);
                post.Comments.Add(comment);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = comment.Id;
            }

            return response;
        }
    }
}