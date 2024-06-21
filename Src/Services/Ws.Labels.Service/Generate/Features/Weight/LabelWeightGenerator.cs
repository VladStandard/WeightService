using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Labels;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Generate.Features.Weight.Models;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Models.Print;
using Ws.Labels.Service.Generate.Services;

namespace Ws.Labels.Service.Generate.Features.Weight;

internal class LabelWeightGenerator(CacheService cacheService, ILabelService labelService, ZplService zplService)
{
    #region Public

    public Label GenerateLabel(GenerateWeightLabelDto dto)
    {
        if (!dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        BarcodeWeightLabel labelLabelBarcode = dto.ToXmlWeightLabel();

        TemplateCache template = cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
                                 throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

        string storageMethod = cacheService.GetStorageByNameFromCacheOrDb(dto.Plu.StorageMethod) ??
                               throw new LabelGenerateException(LabelGenExceptions.StorageMethodNotFound);


        labelLabelBarcode.BarcodeTopTemplate = template.BarcodeTopBody;
        labelLabelBarcode.BarcodeRightTemplate = template.BarcodeRightBody;
        labelLabelBarcode.BarcodeBottomTemplate = template.BarcodeBottomBody;

        #region label parse

        Dictionary<string, string> variables = new() { { "plus_storage_methods", storageMethod } };

        PrintLabelModel data = new(
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

            barcodeTop: labelLabelBarcode.BarCodeTop,
            barcodeRight: labelLabelBarcode.BarCodeRight,
            barcodeBottom: labelLabelBarcode.BarCodeBottom
        );

        string zpl = zplService.GenerateZpl(template.Body, data, variables);

        #endregion

        Label labelSql = new()
        {
            Zpl = zpl,

            Line = dto.Line,
            Plu = dto.Plu,

            BarcodeBottom = data.BarcodeBottom,
            BarcodeRight = data.BarcodeRight,
            BarcodeTop = data.BarcodeTop,
            ExpirationDt = dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),

            ProductDt = labelLabelBarcode.ProductDt,
            Kneading = dto.Kneading,

            WeightNet = dto.Weight,
            WeightTare = dto.Plu.GetWeightWithNesting,
        };
        labelService.Create(labelSql);

        return labelSql;
    }

    #endregion
}