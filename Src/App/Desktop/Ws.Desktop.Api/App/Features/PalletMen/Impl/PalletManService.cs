using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Impl;

public class PalletManService : IPalletManService
{
    public List<PalletMan> GetAll()
    {
        using var context = new WsDbContext();
        var palletMen = context.PalletMen
            .Select(i => new PalletMan
            {
                Id = i.Id,
                Fio = new ()
                {
                    Name = i.Name,
                    Surname = i.Surname,
                    Patronymic = i.Patronymic
                },
                Password = i.Password
            })
            .ToList();
        return new(palletMen);
    }
}