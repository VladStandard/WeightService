using System.Text;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallets;
using Ws.Labels.Service.Api;
using Ws.Labels.Service.Api.Pallet.Input;
using Ws.Labels.Service.Api.Pallet.Output;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Generate.Common;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Models;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Services;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Generate.Features.Piece;


internal class LabelPieceGenerator(
    IPalletService palletService,
    IPalychApi api,
    CacheService cacheService,
    ZplService zplService
    )
{
    public async Task<Guid> GeneratePiecePallet(GeneratePiecePalletDto dto, int labelCount, uint counter)
    {
        if (dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        if (labelCount is > 240 or < 2)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        TemplateFromCache templateFromCache =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
            throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

        BarcodeModel barcodeTemplates = dto.ToBarcodeModel();

        StringBuilder builder = new();

        builder.Append("001460910023");
        builder.AppendStrWithPadding($"{counter + 1}", 7);

        Pallet pallet = new()
        {
            Barcode = builder.ToString(),
            Weight = dto.Weight,
            ProdDt = dto.ProductDt,
            PalletMan = dto.PalletMan,
            Arm = dto.Line,
            Counter = counter+1
        };


        // FOR TESTING VARS BEFORE 1C (don't touch)
        (Label, LabelZpl, TemplateVariables) testData = GenerateLabel(barcodeTemplates, 0, templateFromCache, dto);
        GenerateZpl(testData.Item2, testData.Item3, "1234", templateFromCache);

        if (templateFromCache.Template.Contains("storage_method"))
        {
            templateFromCache.Template = templateFromCache.Template.Replace("storage_method",
                $"{TranslitUtil.Transliterate(dto.Plu.StorageMethod).ToLower()}_sql");
        }

        List<LabelCreateApiDto> labelsData = [];

        List<(Label, LabelZpl, TemplateVariables)> labelsFor1C = [];
        List<(Label, LabelZpl)> labelsForDb = [];

        for (int i = 0 ; i < labelCount ; ++i)
        {
            dto.Line.Counter += 1;
            labelsFor1C.Add(GenerateLabel(barcodeTemplates, i, templateFromCache, dto));
        }

        foreach ((Label label, _, _) in labelsFor1C)
        {
            labelsData.Add(new()
            {
                BarcodeTop = label.BarcodeTop,
                BarcodeRight = label.BarcodeRight,
                BarcodeBottom = label.BarcodeBottom,
                NetWeightKg = label.WeightNet,
                GrossWeightKg = label.WeightGross,
                Kneading = (ushort)label.Kneading
            });
        }

        PalletCreateApiDto data = new()
        {
            Organization = "ООО Владимирский стандарт",
            PluUid = dto.Plu.Uid,
            PalletManUid = dto.PalletMan.Uid1C,
            WarehouseUid = dto.Line.Warehouse.Uid1C,
            CharacteristicUid = dto.PluCharacteristic.Uid,
            Barcode = pallet.Barcode,
            ArmNumber = (uint)dto.Line.Number,
            TrayWeightKg = dto.Weight,
            Labels = labelsData,
            ProductDt = pallet.ProdDt,
            CreatedAt = DateTime.Now,
        };


        PalletResponseDto response = await api.CreatePallet(data);
        if (response.Successes.Count > 0)
        {
            PalletSuccess success = response.Successes.First();
            pallet.Uid = success.Uid;
            pallet.Number = success.Number;

            foreach ((Label, LabelZpl, TemplateVariables) variable in labelsFor1C)
            {
                GenerateZpl(variable.Item2, variable.Item3,  pallet.Number, templateFromCache);
                labelsForDb.Add((variable.Item1, variable.Item2));
            }
            palletService.Create(pallet, labelsForDb);

        }
        else
            throw new LabelGenerateException(LabelGenExceptions.ExchangeFailed);

        return pallet.Uid;
    }

    private (Label, LabelZpl, TemplateVariables) GenerateLabel(
        BarcodeModel barcodeTemplates, int index,
        TemplateFromCache templateFromCache, GeneratePiecePalletDto dto)
    {
        BarcodeModel barcode = barcodeTemplates with
        {
            LineCounter =  dto.Line.Counter,
            ProductDt = barcodeTemplates.ProductDt.AddSeconds(index)
        };

        BarcodeReadyModel barcodeTop = barcode.GenerateBarcode(templateFromCache.BarcodeTopTemplate);
        BarcodeReadyModel barcodeRight = barcode.GenerateBarcode(templateFromCache.BarcodeRightTemplate);
        BarcodeReadyModel barcodeBottom = barcode.GenerateBarcode(templateFromCache.BarcodeBottomTemplate);

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
            weight: dto.Plu.Weight*dto.PluCharacteristic.BundleCount,
            weightGross: dto.Plu.GetWeightByCharacteristic(dto.PluCharacteristic),

            barcodeTop: barcodeTop,
            barcodeBottom: barcodeBottom,
            barcodeRight: barcodeRight,
            palletOrder: (ushort)(index + 1),
            palletNumber: "21"
        );

        string zpl = zplService.GenerateZpl(templateFromCache, data);

        Label label = new()
        {
            BarcodeBottom = barcodeBottom.Clean,
            BarcodeRight = barcodeRight.Clean,
            BarcodeTop = barcodeTop.Clean,
            WeightNet = dto.Plu.Weight,
            WeightTare = dto.Plu.GetTareWeightByCharacteristic(dto.PluCharacteristic),
            Kneading = dto.Kneading,
            ProductDt = dto.ProductDt,
            ExpirationDt = dto.ExpirationDt,
            Line = dto.Line,
            Plu = dto.Plu,
            BundleCount = dto.PluCharacteristic.BundleCount
        };

        LabelZpl labelZpl = new()
        {
            Height = templateFromCache.Height,
            Width = templateFromCache.Width,
            Rotate = templateFromCache.Rotate,
            Zpl = zpl
        };

        return (label, labelZpl, data);
    }

    private void GenerateZpl(LabelZpl labelZpl, TemplateVariables vars, string palletNumber, TemplateFromCache templateFromCache)
    {
        vars.PalletNumber = palletNumber;
        labelZpl.Zpl = zplService.GenerateZpl(templateFromCache, vars);
    }
}