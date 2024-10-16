using System.Net;
using Ws.Mobile.Api.App.Features.Users.Common;
using Ws.Mobile.Models.Features.Users;
using Ws.Shared.Exceptions;

namespace Ws.Mobile.Api.App.Features.Users.Impl;

internal sealed class UserApiService : IUserService
{
    #region Queries

    public UserDto GetByCode(string code)
    {
        Dictionary<string, UserDto> users = new()
        {
            { "1234", new()
                {
                    Id = Guid.NewGuid(),
                    Fio = new("Александров",
                    "Даниил",
                    "Дмитриевич"),
                    WarehouseName = "Склад птички"
                }
            },
            { "4321", new()
                {
                    Id = Guid.NewGuid(),
                    Fio = new("Власов",
                    "Артем",
                    "Алексеевич"),
                    WarehouseName = "Склад сосиски"
                }
            },
        };
        return users.TryGetValue(code, out UserDto? user) ? user : throw new ApiInternalException
        {
            ErrorDisplayMessage = "Пользователь не найден",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    #endregion

}