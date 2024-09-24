using Ws.Barcodes;
using Ws.Barcodes.Models;
using Ws.Database.EntityFramework.Entities.Print.Labels;
using Ws.Database.EntityFramework.Entities.Print.LabelsZpl;
using Ws.Labels.Service.Api;
using Ws.Labels.Service.Api.Pallet.Input;
using Ws.Labels.Service.Api.Pallet.Output;
using Ws.Labels.Service.Generate.Exceptions;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Models.Variables;
using Ws.Labels.Service.Generate.Services;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Generate.Features.Piece;

public record PalletOutputData(Guid Id, string Number, List<LabelEntity> labels);

internal class LabelPieceGenerator(
    IPalychApi api,
    CacheService cacheService,
    ZplService zplService
    )
{
    public async Task<PalletOutputData> GeneratePiecePallet(GeneratePiecePalletDto dto, int labelCount)
    {
        if (labelCount is > 240 or < 1)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = EnumUtils.GetEnumDescription(LabelGenExceptionType.Invalid),
                ErrorInternalMessage = $"Label count must be between 240 or < 1. But {labelCount}"
            };

        TemplateFromCache templateFromCache =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateId ?? Guid.Empty) ??
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = EnumUtils.GetEnumDescription(LabelGenExceptionType.TemplateNotFound),
            };

        BarcodeBuilder barcodeTemplates = dto.ToBarcodeBuilder();


        if (templateFromCache.Template.Contains("storage_method"))
            templateFromCache.Template = templateFromCache.Template.Replace("storage_method",
            $"{TranslitUtil.Transliterate(dto.Plu.StorageMethod).ToLower()}_sql");

        // FOR TESTING VARS BEFORE 1C (don't touch)
        (LabelEntity, TemplateVars) testData = GenerateLabel(barcodeTemplates, 0, templateFromCache, dto);
        GenerateZpl(testData.Item1.Zpl, testData.Item2, "1234", templateFromCache);

        List<LabelCreateApiDto> labelsFor1C = [];
        List<(LabelEntity, TemplateVars)> labelsData = [];

        for (int i = 0 ; i < labelCount ; ++i)
            labelsData.Add(GenerateLabel(barcodeTemplates, i, templateFromCache, dto));

        foreach ((LabelEntity label, _) in labelsData)
            labelsFor1C.Add(new()
            {
                BarcodeTop = label.BarcodeTop,
                BarcodeRight = label.BarcodeRight,
                BarcodeBottom = label.BarcodeBottom,
                NetWeightKg = label.WeightNet,
                GrossWeightKg = label.WeightNet + label.WeightTare,
                Kneading = (ushort)label.Kneading
            });

        PalletCreateApiDto data = new()
        {
            Organization = "ООО Владимирский стандарт",
            PluUid = dto.Plu.Id,
            PalletManUid = dto.Pallet.PalletManId1C,
            WarehouseUid = dto.Pallet.WarehouseId1C,
            CharacteristicUid = dto.Nesting.Id,
            Barcode = dto.Pallet.Barcode,
            ArmNumber = (uint)dto.Line.Number,
            TrayWeightKg = dto.TrayWeight,
            Labels = labelsFor1C,
            ProductDt = dto.ProductDt,
            CreatedAt = DateTime.Now,
            NetWeightKg = labelsFor1C.Sum(i => i.NetWeightKg),
            GrossWeightKg = labelsFor1C.Sum(i => i.GrossWeightKg)
        };


        PalletResponseDto response = await api.CreatePallet(data);
        if (response.Successes.Count > 0)
        {
            PalletSuccess success = response.Successes.First();

            PalletOutputData output = new(success.Uid, success.Number, []);

            foreach ((LabelEntity, TemplateVars) variable in labelsData)
            {
                variable.Item1.PalletId = output.Id;
                GenerateZpl(variable.Item1.Zpl, variable.Item2, output.Number, templateFromCache);
                output.labels.Add(variable.Item1);
            }
            return output;
        }
        throw new ApiExceptionServer
        {

            ErrorDisplayMessage = EnumUtils.GetEnumDescription(LabelGenExceptionType.ExchangeFailed),
            ErrorInternalMessage = response.Errors.First().Message
        };
    }

    private (LabelEntity, TemplateVars) GenerateLabel(BarcodeBuilder barcodeTemplates, int index, TemplateFromCache templateFromCache, GeneratePiecePalletDto dto)
    {
        BarcodeBuilder barcode = barcodeTemplates with
        {
            LineCounter = (uint)(dto.Line.Counter + index + 1),
            ProductDt = barcodeTemplates.ProductDt.AddSeconds(index)
        };

        BarcodeResult barcodeTop = barcode.Build(templateFromCache.BarcodeTopTemplate);
        BarcodeResult barcodeRight = barcode.Build(templateFromCache.BarcodeRightTemplate);
        BarcodeResult barcodeBottom = barcode.Build(templateFromCache.BarcodeBottomTemplate);

        decimal weightNet = dto.Nesting.CalculateWeightNet(dto.Plu);
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
                Order = (ushort)(index + 1)
            },
            productDt: dto.ProductDt,
            expirationDt: dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),

            bundleCount: (ushort)dto.Nesting.BundleCount,
            kneading: (ushort)dto.Kneading,
            weightNet: weightNet,
            weightGross: weightNet + weightTare,

            barcodeTop: barcodeTop,
            barcodeBottom: barcodeBottom,
            barcodeRight: barcodeRight
        );

        string zpl = zplService.GenerateZpl(templateFromCache, data);

        LabelEntity label = new()
        {
            BarcodeBottom = barcodeBottom.Clean,
            BarcodeRight = barcodeRight.Clean,
            BarcodeTop = barcodeTop.Clean,
            WeightNet = weightNet,
            WeightTare = weightTare,
            Kneading = dto.Kneading,
            ProductDt = dto.ProductDt,
            ExpirationDt = dto.ExpirationDt,
            LineId = dto.Line.Id,
            PluId = dto.Plu.Id,
            BundleCount = (ushort)dto.Nesting.BundleCount,
            IsWeight = false,
            Zpl = new()
            {
                Height = templateFromCache.Height,
                Width = templateFromCache.Width,
                Rotate = templateFromCache.Rotate,
                Zpl = zpl
            }
        };

        return (label, data);
    }

    private void GenerateZpl(LabelZplEntity labelZpl, TemplateVars vars, string palletNumber, TemplateFromCache templateFromCache)
    {
        vars.Pallet = vars.Pallet with { Number = palletNumber };
        labelZpl.Zpl = zplService.GenerateZpl(templateFromCache, vars);
    }
}