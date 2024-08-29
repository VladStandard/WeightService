using Ws.Database.EntityFramework.Entities.Ref.Users;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users.Impl.Expressions;

public static class UserExpressions
{
    public static Expression<Func<UserEntity, UserDto>> ToDto =>
        user => new()
        {
            Id = user.Id,
        };
}