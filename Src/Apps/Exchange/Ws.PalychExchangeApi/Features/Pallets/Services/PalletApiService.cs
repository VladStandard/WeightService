using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Print.Pallets;
using Ws.PalychExchangeApi.Features.Pallets.Common;
using Ws.PalychExchangeApi.Features.Pallets.Dto;

namespace Ws.PalychExchangeApi.Features.Pallets.Services;

internal class PalletApiService(WsDbContext context) : IPalletService
{
    public PalletUpdateStatus Update(PalletUpdateDto dto)
    {
        PalletUpdateStatus status = new() { IsSuccess = true };

        PalletEntity? pallet = context.Pallets.FirstOrDefault(i => i.Number == dto.Number);

        if (pallet == null)
        {
            status.IsSuccess = false;
            status.Message = $"Паллета {dto.Number}: не найдена";
        }
        else
        {
            pallet.IsShipped = dto.IsShipped;
            pallet.DeletedAt = dto.IsDelete ? DateTime.Now : null;
            context.SaveChanges();
        }

        return status;
    }
}