using Application.DTOs.Post;
using Application.Responses;
using MediatR;

namespace Application.Features.Post.Requests.Command;
public record DeletePostRequest(DeletePostDto postDeleteDto) : IRequest<BaseCommandResponse>;