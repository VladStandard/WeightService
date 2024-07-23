using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Labels;
using Ws.Labels.Service.Generate.Common;
using Ws.Labels.Service.Generate.Exceptions;
using Ws.Labels.Service.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Models.Variables;
using Ws.Labels.Service.Generate.Services;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Generate.Features.Weight;

internal class LabelWeightGenerator(CacheService cacheService, ILabelService labelService, ZplService zplService)
{
    public (Label, LabelZpl) GenerateLabel(GenerateWeightLabelDto dto)
    {
        if (!dto.Plu.IsCheckWeight)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = EnumHelper.GetEnumDescription(LabelGenExceptions.Invalid),
                ErrorInternalMessage = "Plu is piece, must be a weight"
            };

        TemplateFromCache templateFromCache =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = EnumHelper.GetEnumDescription(LabelGenExceptions.TemplateNotFound),
            };


        BarcodeModel barcode = dto.ToBarcodeModel();

        #region label parse

        BarcodeReadyModel barcodeTop = barcode.GenerateBarcode(templateFromCache.BarcodeTopTemplate);
        BarcodeReadyModel barcodeRight = barcode.GenerateBarcode(templateFromCache.BarcodeRightTemplate);
        BarcodeReadyModel barcodeBottom = barcode.GenerateBarcode(templateFromCache.BarcodeBottomTemplate);

        TemplateVars data = new(
            plu: new()
            {
                Name = dto.Plu.FullName,
                Number = (ushort)dto.Plu.Number,
                Description = dto.Plu.Description
            },
            arm: new()
            {
                Number = dto.Line.Number,
                Name = dto.Line.Name,
                Address = dto.Line.Warehouse.ProductionSite.Address
            },
            pallet: new()
            {
                Number = "1",
                Order = 0
            },
            productDt: dto.ProductDt,
            expirationDt: dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),
            bundleCount: (ushort)dto.Plu.PluNesting.BundleCount,
            kneading: (ushort)dto.Kneading,
            weightNet: dto.Weight,
            weightGross: dto.Weight + dto.Plu.GetWeightWithNesting,

            barcodeTop: barcodeTop,
            barcodeBottom: barcodeBottom,
            barcodeRight: barcodeRight
        );

        if (templateFromCache.Template.Contains("storage_method"))
            templateFromCache.Template = templateFromCache.Template.Replace("storage_method",
                $"{TranslitUtil.Transliterate(dto.Plu.StorageMethod).ToLower()}_sql");

        string zpl = zplService.GenerateZpl(templateFromCache, data);

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

            BarcodeBottom = barcodeBottom.Clean,
            BarcodeRight = barcodeRight.Clean,
            BarcodeTop = barcodeTop.Clean,

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