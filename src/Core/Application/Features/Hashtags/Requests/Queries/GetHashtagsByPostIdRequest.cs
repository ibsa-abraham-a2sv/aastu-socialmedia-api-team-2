using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Hashtag;
using MediatR;

namespace Application.Features.Hashtags.Requests.Queries
{
    public class GetHashtagsByPostIdRequest : IRequest<List<HashtagDto>>
    {
        public Guid PostId {get; set;}
        public int PageIndex {get; set;}
        public int PageSize {get; set;}
    }
}