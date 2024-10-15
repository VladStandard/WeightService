using Ws.Tablet.Models.Features.Users;

namespace Ws.Tablet.Models.Api;

public interface ITabletUserApi
{
    [Get("/users")]
    Task<UserDto> GetUserByCode(ushort code);
}