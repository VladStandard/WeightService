using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.Database.EntityFramework.Entities.Ref.Users;
using Ws.DeviceControl.Api.App.Features.Admins.Users.Common;
using Ws.DeviceControl.Api.App.Features.Admins.Users.Impl.Expressions;
using Ws.DeviceControl.Models.Features.Admins.Users.Commands.Update;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users.Impl;

internal sealed class UserApiService(WsDbContext dbContext, UserHelper userHelper) : IUserService
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
    public async Task<UserDto> SaveOrUpdateUser(Guid uid, UserUpdateDto updateDto)
    {
        UserEntity? user = await dbContext.Users.FindAsync(uid);

        ProductionSiteEntity site = await dbContext.ProductionSites.SafeGetById(updateDto.ProductionSiteId, "Не найдено");

        if (user is null)
        {
            user = new()
            {
                Id = uid,
                ProductionSite = site
            };

            await dbContext.AddAsync(user);
        }
        else
            user.ProductionSite = site;

        await dbContext.SaveChangesAsync();
        return UserExpressions.ToDto.Compile().Invoke(user);
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        UserEntity user = await dbContext.Users.SafeGetById(id, "Пользователь не найден");
        return UserExpressions.ToDto.Compile().Invoke(user);
    }

    #endregion

    #region Commands

    public Task DeleteAsync(Guid id) => dbContext.Users.SafeDeleteAsync(i => i.Id == id);

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(UserEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}