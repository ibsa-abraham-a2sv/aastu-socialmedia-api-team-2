using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Hashtag;
using Domain.Hashtag;
using MediatR;

namespace Application.Features.Hashtags.Requests.Queries
{
    public class GetHashtagByIdRequest : IRequest<HashtagDto>
    {
        public Guid Id { get; set; }
    }
}