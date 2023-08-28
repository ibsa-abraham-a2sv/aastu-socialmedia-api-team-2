using Application.Contracts.Persistence;
using Application.DTOs.UserConnectionIdMap;
using Domain.UserConnectionIdMap;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistence.Repository
{
    public class UserConnectionMapRepository : IUserConnectionMapRepository
    {
        private readonly SocialSyncDbContext _dbContext;
        public UserConnectionMapRepository(SocialSyncDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserConnectionMappingAsync(Guid userId, string connectionId)
        {
            var mappingEntity = new UserConnectionMapping()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ConnectionId = connectionId
            };
            _dbContext.UserConnectionMapping.Add(mappingEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CreateUserConnectionMappingDTO> GetUserConnectionMappingAsync(Guid userId)
        {
            var mappingEntity = await _dbContext.UserConnectionMapping.FirstOrDefaultAsync(m => m.UserId == userId);

            if (mappingEntity == null)
                return null;

            return new CreateUserConnectionMappingDTO
            {
                UserId = mappingEntity.UserId,
                ConnectionId = mappingEntity.ConnectionId
            };
        }


        public async Task RemoveUserConnectionMappingAsync(Guid userId)
        {
            var mapping = await _dbContext.UserConnectionMapping.FirstOrDefaultAsync(m => m.UserId == userId);
            if (mapping != null)
            {
                _dbContext.UserConnectionMapping.Remove(mapping);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
