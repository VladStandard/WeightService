using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Labels;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Generate.Features.Weight.Models;
using Ws.Labels.Service.Generate.Models;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Services;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Generate.Features.Weight;

internal class LabelWeightGenerator(CacheService cacheService, ILabelService labelService, ZplService zplService)
{
    public (Label, LabelZpl) GenerateLabel(GenerateWeightLabelDto dto)
    {
        if (!dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        TemplateFromCache templateFromCache =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
            throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

        BarcodeWeightLabel barcode = dto.ToBarcodeModel();

        #region label parse

        TemplateVariables data = new(
            pluName: dto.Plu.FullName,
            pluNumber: (ushort)dto.Plu.Number,
            pluDescription: dto.Plu.Description,
            lineNumber: dto.Line.Number,
            lineName: dto.Line.Name,
            lineAddress: dto.Line.Warehouse.ProductionSite.Address,
            productDt: dto.ProductDt,
            expirationDt: dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),
            bundleCount: (ushort)dto.Plu.PluNesting.BundleCount,
            kneading: (ushort)dto.Kneading,
            weight: dto.Weight,
            weightGross: dto.Weight + dto.Plu.GetWeightWithNesting,
            barcodeTop: barcode.GenerateBarcode(templateFromCache.BarcodeTopTemplate),
            barcodeRight: barcode.GenerateBarcode(templateFromCache.BarcodeRightTemplate),
            barcodeBottom: barcode.GenerateBarcode(templateFromCache.BarcodeBottomTemplate),
            palletOrder: 0,
            palletNumber: ""
        );

        if (templateFromCache.Template.Contains("storage_method"))
            templateFromCache.Template = templateFromCache.Template.Replace("storage_method",
                $"{TranslitUtil.Transliterate(dto.Plu.StorageMethod).ToLower()}_sql");

        string zpl = zplService.GenerateZpl(templateFromCache.Template, data);

        #endregion

        LabelZpl zplLabel = new()
        {
            Zpl = zpl,
            Width = templateFromCache.Width,
            Height = templateFromCache.Height,
            Rotate = templateFromCache.Rotate
        };

        Label labelSql = new()
        {
            Line = dto.Line,
            Plu = dto.Plu,

            BarcodeBottom = data.BarcodeBottom.Replace(">8", ""),
            BarcodeRight = data.BarcodeRight.Replace(">8", ""),
            BarcodeTop = data.BarcodeTop.Replace(">8", ""),

            ExpirationDt = dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),

            ProductDt = barcode.ProductDt,
            Kneading = dto.Kneading,

            WeightNet = dto.Weight,
            WeightTare = dto.Plu.GetWeightWithNesting,
            BundleCount = data.BundleCount
        };
        labelService.Create(labelSql, zplLabel);

        return (labelSql, zplLabel);
    }
}