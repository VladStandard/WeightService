using Scriban;
using Scriban.Runtime;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallets;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Features.Piece.Models;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Services;
using Template = Scriban.Template;

namespace Ws.Labels.Service.Generate.Features.Piece;

internal class LabelPieceGenerator(IPalletService palletService, CacheService cacheService)
{
    public Guid GeneratePiecePallet(GeneratePiecePalletDto dto, int labelCount)
    {
        if (dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        if (labelCount > 240)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        PieceGeneratorModel generatorModel = dto.AdaptToBarcodeModel();

        TemplateFromCache templateFrom = cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
                                 throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

        string storageMethod = cacheService.GetStorageByNameFromCacheOrDb(dto.Plu.StorageMethod) ??
                               throw new LabelGenerateException(LabelGenExceptions.StorageMethodNotFound);

        Pallet pallet = new()
        {
            Barcode = new Random().Next(0, 1000001).ToString(),
            Weight = dto.Weight,
            ProdDt = dto.ProductDt,
            PalletMan = dto.PalletMan,
            Arm = dto.Line,
            Counter = new Random().Next(0, 1000001),
            Number = new Random().Next(0, 1000001),
        };

        IList<Label> labels = [];

        #region label parse

        Dictionary<string, string> dataDict = new();
        foreach ((string? s, string? val) in cacheService.GetResourcesFromCacheOrDb(new()))
        {
            string key = $"{s.ToLower()}_sql";
            dataDict.Add(key, val);
        }

        dataDict.Add("plus_storage_methods_fk_sql", storageMethod);

        #endregion

        // for (int i = 0 ; i < labelCount ; i++)
        // {
        //     generatorModel = dto.AdaptToXmlPieceLabel();
        //     generatorModel.BarcodeTopTemplate = templateFrom.BarcodeTopBody;
        //     generatorModel.BarcodeRightTemplate = templateFrom.BarcodeRightBody;
        //     generatorModel.BarcodeBottomTemplate = templateFrom.BarcodeBottomBody;
        //
        //
        //     Label label = GenerateLabel(dto, generatorModel, templateFrom.Body, dataDict);
        //
        //     labels.Add(label);
        //
        //     dto = dto with { ProductDt = dto.ProductDt.AddSeconds(1) };
        // }
        // palletService.Create(pallet, labels);
        // return pallet.Uid;
        return Guid.Empty;
    }

    private static Label GenerateLabel(GeneratePiecePalletDto generatePalletDto,  PieceGeneratorModel generatorModel, string template,  Dictionary<string, string> dataDict)
    {
        TemplateContext context = new()
        {
            StrictVariables = true
        };

        ScriptObject scriptObject1 = new();
        scriptObject1.Import(generatorModel);
        scriptObject1.Import(dataDict);

        context.PushGlobal(scriptObject1);

        Template? labelTemp = Template.Parse(template);
        // ZplInfo ready = new(labelTemp.Render(context), labelLabel);
        //
        // return new()
        // {
        //     Zpl = ready.Zpl,
        //     BarcodeBottom = ready.BarcodeBottom,
        //     BarcodeRight = ready.BarcodeRight,
        //     BarcodeTop = ready.BarcodeTop,
        //     WeightNet = generatePalletDto.Plu.Weight,
        //     WeightTare = generatePalletDto.Plu.GetTareWeightByCharacteristic(generatePalletDto.PluCharacteristic),
        //     Kneading = generatePalletDto.Kneading,
        //     ProductDt = generatePalletDto.ProductDt,
        //     ExpirationDt = generatePalletDto.ExpirationDt,
        //     Line = generatePalletDto.Line,
        //     Plu = generatePalletDto.Plu,
        //     BundleCount = (ushort)generatePalletDto.PluCharacteristic.BundleCount
        // };
        return new();
    }
}