using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Expressions;

internal static class PalletManExpressions
{
    public static Expression<Func<PalletManEntity, PalletMan>> ToDto => palletMan =>
        new()
        {
            Id = palletMan.Id,
            Fio = new(palletMan.Name, palletMan.Surname, palletMan.Patronymic),
            Password = palletMan.Password
        };
}