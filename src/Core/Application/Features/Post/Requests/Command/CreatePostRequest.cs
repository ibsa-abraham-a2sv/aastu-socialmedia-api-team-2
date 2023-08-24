// using Application.DTOs.Post;
// using Application.Responses;
// using MediatR;
// using System;
// using System.Collections.Generic;
// using System.Text;

// namespace Application.Features.Post.Requests.Command
// {
//     public class CreatePostRequest : IRequest<BaseCommandResponse>
//     {
//         public CreatePostRequest(PostDto postDto)
//         {
//             PostDto = postDto;
//         }

//         public PostDto PostDto { get; }

//     }
// }
using Application.DTOs.Post;
using Application.Responses;
using MediatR;

namespace Application.Features.Post.Requests.Command;
public record CreatePostRequest(PostDto postDto) : IRequest<BaseCommandResponse>;
