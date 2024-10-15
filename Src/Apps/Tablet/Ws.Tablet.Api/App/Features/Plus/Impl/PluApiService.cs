using System.Net;
using Ws.Shared.Exceptions;
using Ws.Tablet.Api.App.Features.Plus.Common;
using Ws.Tablet.Models.Features.Plus;

namespace Ws.Tablet.Api.App.Features.Plus.Impl;

internal sealed class PluApiService : IPluService
{
    #region Queries

    public PluDto GetByNumber(uint number)
    {
        List<PluDto> plus =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Number = 1234,
                Name = "Сосиска по Китайски"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Number = 4321,
                Name = "Сарделька по Вьетнамски"
            },
        ];

        return plus.Find(i => i.Number == number) ?? throw new ApiInternalException
        {
            ErrorDisplayMessage = "Плу не найден",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    #endregion

}