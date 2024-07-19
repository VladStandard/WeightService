using Ws.Database.EntityFramework.Entities.Ref.Users;
using Ws.DeviceControl.Api.App.Features.Admins.Users.Common;
using Ws.DeviceControl.Api.App.Features.Admins.Users.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users.Impl;

public class UserApiService(WsDbContext dbContext) : IUserService
{
    #region Queries

    public Task<List<UserDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Users
            .AsNoTracking()
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .Select(UserExpressions.ToDto)
            .ToListAsync();
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        UserEntity? user = await dbContext.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException();
        return UserExpressions.ToDto.Compile().Invoke(user);
    }

    #endregion
}