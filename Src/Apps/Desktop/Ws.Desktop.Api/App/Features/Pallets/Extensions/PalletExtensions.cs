using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Entities.Print;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Extensions;

internal static class PalletExtensions
{
    public static IQueryable<PalletInfo> ToPalletInfo(this IQueryable<PalletEntity> query, DbSet<LabelEntity> labelContext)
    {
        return query
            .GroupJoin(
                labelContext,
                pallet => pallet.Id,
                label => label.PalletEntityId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .Select(result => new PalletInfo
            {
                Id = result.Pallet.Id,
                Arm = result.Pallet.Arm.Name,
                Warehouse = result.Pallet.Arm.Warehouse.Name,
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
                Kneadings = result.Labels.Select(i => (ushort)i.Kneading)
                    .ToHashSet(),
            });
    }

    public static IQueryable<LabelInfo> ToLabelInfo(this IQueryable<PalletEntity> query, DbSet<LabelEntity> labelContext)
    {
        return query
            .GroupJoin(
                labelContext,
                pallet => pallet.Id,
                label => label.PalletEntityId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .SelectMany(
                result => result.Labels,
                (result, label) => new LabelInfo
                {
                    Zpl = label.Zpl
                });
    }
}