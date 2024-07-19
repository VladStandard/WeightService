using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;

public static class PalletManExpressions
{
    public static Expression<Func<PalletManEntity, PalletManDto>> ToDto =>
        palletMan => new()
        {
            Id = palletMan.Id,
            Id1C = palletMan.Uid1C,
            Fio = new()
            {
                Name = palletMan.Name,
                Surname = palletMan.Surname,
                Patronymic = palletMan.Patronymic
            },
            Password = palletMan.Password,
            Warehouse = new()
            {
                Id = palletMan.Warehouse.Id,
                Name = palletMan.Warehouse.Name
            },
            CreateDt = palletMan.CreateDt,
            ChangeDt = palletMan.ChangeDt
        };
}