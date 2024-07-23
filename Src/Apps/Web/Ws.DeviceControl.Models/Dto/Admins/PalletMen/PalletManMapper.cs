using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Commands.Update;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Models.Dto.Admins.PalletMen;

public static class PalletManMapper
{
    public static PalletManUpdateDto DtoToUpdateDto(PalletManDto item)
    {
        return new()
        {
            Id1C = item.Id1C,
            Name = item.Fio.Name,
            Surname = item.Fio.Surname,
            Patronymic = item.Fio.Patronymic,
            Password = item.Password,
            WarehouseId = item.Warehouse.Id
        };
    }
}
