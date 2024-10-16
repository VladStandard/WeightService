using Ws.Mobile.Models.Features.Users;

namespace Ws.Mobile.Models.Api;

public interface ITabletUserApi
{
    [Get("/users")]
    Task<UserDto> GetUserByCode(ushort code);
}