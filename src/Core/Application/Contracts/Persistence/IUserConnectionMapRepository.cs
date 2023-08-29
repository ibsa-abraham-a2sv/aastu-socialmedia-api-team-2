using Application.DTOs.UserConnectionIdMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IUserConnectionMapRepository
    {
        Task AddUserConnectionMappingAsync(Guid userId, string connectionId);
        Task<CreateUserConnectionMappingDTO> GetUserConnectionMappingAsync(Guid userId);
        Task RemoveUserConnectionMappingAsync(Guid userId);
    }
}
