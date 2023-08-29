using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Hashtag;
using MediatR;

namespace Application.Features.Hashtags.Requests.Queries
{
    public class GetHashtagByTagRequest : IRequest<HashtagDto>
    {
        public required string Tag { get; set; }
    }
}