using System.Linq.Expressions;
using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Expressions;


internal static class PalletManExpressions
{
    public static Expression<Func<PalletManEntity, PalletMan>> ToDto => palletMan =>
        new()
        {
            Id = palletMan.Id,
            Fio = new()
            {
                Name = palletMan.Name,
                Surname = palletMan.Surname,
                Patronymic = palletMan.Patronymic
            },
            Password = palletMan.Password
        };
}