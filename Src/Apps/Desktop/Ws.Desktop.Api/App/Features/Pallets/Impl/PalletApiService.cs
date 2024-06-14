using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.Plus;
using Ws.Labels.Service.Features.Generate;
using Ws.Labels.Service.Features.Generate.Features.Piece.Dto;

namespace Ws.Desktop.Api.App.Features.Pallets.Impl;

public class PalletApiService(
    WsDbContext dbContext,
    IPluService pluService,
    IArmService armService,
    IPalletManService palletManService,
    IPrintLabelService printLabelService
    ): IPalletApiService
{
    public List<PalletInfo> GetAllByDate(Guid armId, DateTime startTime, DateTime endTime)
    {
        List<PalletInfo> pallets = dbContext.Pallets
            .Where(p => p.Arm.Id == armId && p.CreateDt > startTime && p.CreateDt < endTime)
            .GroupJoin(
                dbContext.Labels,
                pallet => pallet.Id,
                label => label.PalletEntityId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .OrderByDescending (result => result.Pallet.CreateDt)
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

    public List<LabelInfo> GetAllZplByArm(Guid armId, Guid palletId)
    {
        List<LabelInfo> labels = dbContext.Pallets
            .Where(p => p.Arm.Id == armId && p.Id == palletId)
            .GroupJoin(
                dbContext.Labels,
                pallet => pallet.Id,
                label => label.PalletEntityId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .SelectMany(
                result => result.Labels,
                (result, label) => new LabelInfo
                {
                    Zpl = label.Zpl
                })
            .ToList();
        return labels;
    }

    public PalletInfo CreatePiecePallet(Guid armId, PalletPieceCreateDto dto)
    {
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

       return dbContext.Pallets
            .Where(p => p.Id == palletId)
            .GroupJoin(
                dbContext.Labels,
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

    public PalletInfo GetByNumber(Guid armId, uint number)
    {
        return dbContext.Pallets
            .Where(p => p.Arm.Id == armId && p.Number == number)
            .GroupJoin(
                dbContext.Labels,
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