using Ws.Database.EntityFramework.Entities.Print.Labels;
using Ws.Labels.Service.Generate.Common;
using Ws.Labels.Service.Generate.Exceptions;
using Ws.Labels.Service.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Models.Variables;
using Ws.Labels.Service.Generate.Services;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Generate.Features.Weight;

internal class LabelWeightGenerator(CacheService cacheService, ZplService zplService)
{
    public LabelEntity GenerateLabel(GenerateWeightLabelDto dto)
    {
        TemplateFromCache templateFromCache =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateId ?? Guid.Empty) ??
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = EnumHelper.GetEnumDescription(LabelGenExceptions.TemplateNotFound),
            };

        BarcodeModel barcode = dto.ToBarcodeModel();

        #region label parse

        BarcodeReadyModel barcodeTop = barcode.GenerateBarcode(templateFromCache.BarcodeTopTemplate);
        BarcodeReadyModel barcodeRight = barcode.GenerateBarcode(templateFromCache.BarcodeRightTemplate);
        BarcodeReadyModel barcodeBottom = barcode.GenerateBarcode(templateFromCache.BarcodeBottomTemplate);

        decimal weightNet = dto.Plu.Weight;
        decimal weightTare = dto.Nesting.CalculateWeightTare(dto.Plu);

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
                Address = dto.Line.Address
            },
            pallet: new()
            {
                Number = string.Empty,
                Order = 0
            },
            productDt: dto.ProductDt,
            expirationDt: dto.ExpirationDt,
            bundleCount: (ushort)dto.Nesting.BundleCount,
            kneading: (ushort)dto.Kneading,
            weightNet: weightNet,
            weightGross: weightNet + weightTare,

            barcodeTop: barcodeTop,
            barcodeBottom: barcodeBottom,
            barcodeRight: barcodeRight
        );

        if (templateFromCache.Template.Contains("storage_method"))
            templateFromCache.Template = templateFromCache.Template.Replace("storage_method",
                $"{TranslitUtil.Transliterate(dto.Plu.StorageMethod).ToLower()}_sql");

        string zpl = zplService.GenerateZpl(templateFromCache, data);

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
                Width = templateFromCache.Width,
                Height = templateFromCache.Height,
                Rotate = templateFromCache.Rotate
            }
        };

        return labelSql;
    }
}