using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users.Common;

public interface IUserService : IDeleteService<Guid>
{
    #region Queries

    Task<UserDto> GetByIdAsync(Guid id);
    Task<List<UserDto>> GetAllByProductionSiteAsync(Guid productionSiteId);

    #endregion
}