using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Features.Plus;
using Ws.Labels.Service.Features.Generate;
using Ws.Labels.Service.Features.Generate.Features.Piece.Dto;
using IArmService = Ws.Domain.Services.Features.Arms.IArmService;
using IPalletManService = Ws.Domain.Services.Features.PalletMen.IPalletManService;

namespace Ws.Desktop.Api.App.Features.Pallets.Impl;

public class PalletApiService(
    IPalletManService palletManService,
    IPluService pluService,
    IPrintLabelService printLabelService,
    IArmService armService
    ): IPalletApiService
{
    public List<PalletInfo> GetAllByArm(Guid armId)
    {
        using var context = new WsDbContext();

        List<PalletInfo> pallets = context.Pallets
            .Where(p => p.Arm.Id == armId)
            .GroupJoin(
                context.Labels,
                pallet => pallet.Id,
                label => label.PalletEntityId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .Select(result => new PalletInfo
            {
                Id = result.Pallet.Id,
                Number = result.Pallet.Number,
                PluName = result.Pallet.Plu.Name,
                PluNumber = (ushort)result.Pallet.Plu.Number,
                LabelCount = (uint)result.Labels.Count(),
                WeightNet = result.Labels.Sum(label => label.WeightNet),
                WeightBrutto = result.Labels.Sum(label => label.WeightTare + label.WeightNet),
                PalletMan = new()
                {
                    Name = result.Pallet.PalletMan.Name,
                    Surname = result.Pallet.PalletMan.Surname,
                    Patronymic = result.Pallet.PalletMan.Patronymic
                },
                WeightTray = result.Pallet.TrayWeight,
                Barcode = result.Pallet.Barcode,
                ProdDt = result.Pallet.ProductDt,
                CreateDt = result.Pallet.CreateDt,
            })
            .ToList();

        return pallets;
    }

    public PalletInfo CreatePiecePallet(Guid armId, PalletPieceCreateDto dto)
    {
        using var context = new WsDbContext();

        var plu = pluService.GetItemByUid(dto.PluId);
        List<PluCharacteristic> characteristic = plu.CharacteristicsWithNesting.ToList();

        var data = new GeneratePiecePalletDto
        {
            Plu = pluService.GetItemByUid(dto.PluId),
            Line = armService.GetItemByUid(armId),
            PalletMan = palletManService.GetItemByUid(dto.PalletManId),
            PluCharacteristic = characteristic.Single(i => i.Uid == dto.CharacteristicId),
            Kneading = (short)dto.Kneading,
            Weight = dto.WeightTray,
            ProductDt = dto.ProdDt,
            ExpirationDt = dto.ProdDt.AddDays(plu.ShelfLifeDays),
        };
        var palletId = printLabelService.GeneratePiecePallet(data, dto.LabelCount);

       return context.Pallets
            .Where(p => p.Id == palletId)
            .GroupJoin(
                context.Labels,
                pallet => pallet.Id,
                label => label.PalletEntityId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .Select(result => new PalletInfo
            {
                Id = result.Pallet.Id,
                Number = result.Pallet.Number,
                PluName = result.Pallet.Plu.Name,
                PluNumber = (ushort)result.Pallet.Plu.Number,
                LabelCount = (uint)result.Labels.Count(),
                WeightNet = result.Labels.Sum(label => label.WeightNet),
                WeightBrutto = result.Labels.Sum(label => label.WeightTare + label.WeightNet),
                PalletMan = new()
                {
                    Name = result.Pallet.PalletMan.Name,
                    Surname = result.Pallet.PalletMan.Surname,
                    Patronymic = result.Pallet.PalletMan.Patronymic
                },
                WeightTray = result.Pallet.TrayWeight,
                Barcode = result.Pallet.Barcode,
                ProdDt = result.Pallet.ProductDt,
                CreateDt = result.Pallet.CreateDt,
            })
            .Single();
    }
}