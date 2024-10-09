using Ws.Database.Entities.Print.Labels;
using Ws.Database.Entities.Print.LabelsZpl;
using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Impl.Expressions;

internal static class LabelExpressions
{
    public static Expression<Func<LabelEntity, LabelDto>> ToLabelDto =>
        label => new()
        {
            Id = label.Id,
            IsWeight = label.IsWeight,
            BundleCount = (byte)label.BundleCount,
            Kneading = (ushort)label.Kneading,
            WeightNet = label.WeightNet,
            WeightTare = label.WeightTare,
            Arm = new()
            {
                Id = label.Line.Id,
                Name = label.Line.Name
            },
            Warehouse = new()
            {
                Id = label.Line.Warehouse.Id,
                Name = label.Line.Warehouse.Name,
            },
            ProductionSite = new()
            {
                Id = label.Line.Warehouse.ProductionSite.Id,
                Name = label.Line.Warehouse.ProductionSite.Name,
            },
            Plu = label.Plu != null
                ? new()
                {
                    Id = label.Plu.Id,
                    Name = label.Plu.Number + " | " + label.Plu.Name
                }
                : null,
            Pallet = label.PalletId != null
                ? new()
                {
                    Id = label.Pallet.Id,
                    Name = label.Pallet.Number
                }
                : null,
            BarcodeTop = label.BarcodeTop,
            BarcodeBottom = label.BarcodeBottom,
            BarcodeRight = label.BarcodeRight,
            ProductDt = DateOnly.FromDateTime(label.ProductDt),
            ExpirationDt = DateOnly.FromDateTime(label.ExpirationDt),
            CreateDt = label.CreateDt,
        };

    public static Expression<Func<LabelZplEntity, ZplDto>> ToZplDto =>
        zpl => new()
        {
            Width = (ushort)zpl.Width,
            Height = (ushort)zpl.Height,
            Rotate = (ushort)zpl.Rotate,
            Zpl = zpl.Zpl
        };
}