using Ws.Tablet.Api.App.Features.Pallets.Common;
using Ws.Tablet.Models.Features.Pallets.Input;
using Ws.Tablet.Models.Features.Pallets.Output;

namespace Ws.Tablet.Api.App.Features.Pallets.Impl;

internal sealed class PalletApiService : IPalletService
{
    #region Commands

    public PalletDto Create(PalletCreateDto palletCreateDto)
    {
        return new()
        {
            DocumentBarcode = palletCreateDto.DocumentBarcode,
            PalletBarcode = "123456789",
            ZplLabel = "123456789",
            Batches = palletCreateDto.Batches.ConvertAll(i => new BatchDto
            {
                PluName = $"Сосиска {i.PluId}",
                Date = i.Date,
                Weight = i.Weight
            }).ToList(),
            User = new("Семенов", "Павел", "Анатольевич"),
            WarehouseName = "Склад Собаки",
            CreateDt = DateTime.Now
        };
    }

    #endregion
}