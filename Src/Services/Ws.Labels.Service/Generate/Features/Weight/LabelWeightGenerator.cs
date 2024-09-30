using Ws.Barcodes.Features.Barcodes;
using Ws.Barcodes.Features.Templates;
using Ws.Barcodes.Features.Templates.Models;
using Ws.Barcodes.Features.Templates.Utils;
using Ws.Barcodes.Features.Templates.Variables;
using Ws.Barcodes.Shared.Models;
using Ws.Database.EntityFramework.Entities.Print.Labels;
using Ws.Labels.Service.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Generate.Services;
using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Generate.Features.Weight;

internal class LabelWeightGenerator(CacheService cacheService)
{
    public LabelEntity GenerateLabel(GenerateWeightLabelDto dto)
    {
        BarcodeBuilder barcode = dto.ToBarcodeBuilder();

        #region label parse

        TemplateInfo templateInfo =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateId ?? Guid.Empty, dto.Plu.StorageMethod);

        Dictionary<string, string> zplResources =
            cacheService.GetResourcesFromCacheOrDb(TemplateUtils.GetResourcesKeys(templateInfo.Template), templateInfo.Rotate);

        PrintSettings printSettings = new(templateInfo, zplResources);

        BarcodeResult barcodeTop = barcode.Build(templateInfo.BarcodeTopTemplate);
        BarcodeResult barcodeRight = barcode.Build(templateInfo.BarcodeRightTemplate);
        BarcodeResult barcodeBottom = barcode.Build(templateInfo.BarcodeBottomTemplate);

        decimal weightNet = dto.Plu.Weight;
        decimal weightTare = dto.Nesting.CalculateWeightTare(dto.Plu);

        TemplateVars data = new(
            plu: new(dto.Plu.FullName, (ushort)dto.Plu.Number, dto.Plu.Description),
            arm: new(dto.Line.Number, dto.Line.Name, dto.Line.Address),
            pallet: new(0,string.Empty),
            barcodes: new(barcodeTop, barcodeBottom, barcodeRight),
            productDt: dto.ProductDt,
            expirationDt: dto.ExpirationDt,

            kneading: (ushort)dto.Kneading,

            bundleCount: (ushort)dto.Nesting.BundleCount,
            weightNet: weightNet,
            weightGross: weightNet + weightTare
        );

        string zpl = ZplBuilder.GenerateZpl(printSettings, data);

        #endregion

        LabelEntity labelSql = new()
        {
            BarcodeBottom = barcodeBottom.Clean,
            BarcodeRight = barcodeRight.Clean,
            BarcodeTop = barcodeTop.Clean,

            ExpirationDt = dto.ExpirationDt,

            ProductDt = dto.ProductDt,
            Kneading = dto.Kneading,

            WeightNet = weightNet,
            WeightTare = weightTare,
            BundleCount = data.BundleCount,
            IsWeight = true,

            Zpl = new()
            {
                Zpl = zpl,
                Width = (short)printSettings.Settings.Width,
                Height = (short)printSettings.Settings.Height,
                Rotate = (short)printSettings.Settings.Rotate
            }
        };

        return labelSql;
    }
}