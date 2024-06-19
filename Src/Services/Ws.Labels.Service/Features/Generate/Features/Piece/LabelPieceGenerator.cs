using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallets;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Features.Generate.Features.Piece.Models;
using Ws.Labels.Service.Features.Generate.Models;
using Ws.Labels.Service.Features.Generate.Models.Cache;
using Ws.Labels.Service.Features.Generate.Services;
using LabelGeneratorUtils = Ws.Labels.Service.Features.Generate.Utils.LabelGeneratorUtils;

namespace Ws.Labels.Service.Features.Generate.Features.Piece;

internal class LabelPieceGenerator(IPalletService palletService, CacheService cacheService)
{
    public Guid GeneratePiecePallet(GeneratePiecePalletDto dto, int labelCount)
    {
        if (dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        if (labelCount > 240)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        XmlPieceLabel labelXml = dto.AdaptToXmlPieceLabel();
        if (!new XmlLabelPiecePalletValidator().Validate(labelXml).IsValid)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        TemplateCache template = cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
                                 throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

        string storageMethod = cacheService.GetStorageByNameFromCacheOrDb(dto.Plu.StorageMethod) ??
                               throw new LabelGenerateException(LabelGenExceptions.StorageMethodNotFound);


        ZplPrintItems zplPrintItems = new()
        {
            Resources = cacheService.GetAllResourcesFromCacheOrDb(),
            Template = template.Body,
            StorageMethod = storageMethod
        };

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
        for (int i = 0 ; i < labelCount ; i++)
        {
            labelXml = dto.AdaptToXmlPieceLabel();
            labelXml.BarcodeBottomTemplate = template.BarcodeBottomBody;
            labelXml.BarcodeRightTemplate = template.BarcodeRightBody;
            labelXml.BarcodeTopTemplate = template.BarcodeTopBody;

            Label label = GenerateLabel(dto, zplPrintItems, labelXml);

            labels.Add(label);

            dto = dto with { ProductDt = dto.ProductDt.AddSeconds(1) };
        }
        palletService.Create(pallet, labels);
        return pallet.Uid;
    }

    private static Label GenerateLabel(GeneratePiecePalletDto generatePalletDto, ZplPrintItems zplPrintItems, XmlPieceLabel labelXml)
    {
        ZplInfo ready = LabelGeneratorUtils.GetZpl(zplPrintItems, labelXml);

        return new()
        {
            Zpl = ready.Zpl,
            BarcodeBottom = ready.BarcodeBottom,
            BarcodeRight = ready.BarcodeRight,
            BarcodeTop = ready.BarcodeTop,
            WeightNet = generatePalletDto.Plu.Weight,
            WeightTare = generatePalletDto.Plu.GetTareWeightByCharacteristic(generatePalletDto.PluCharacteristic),
            Kneading = generatePalletDto.Kneading,
            ProductDt = generatePalletDto.ProductDt,
            ExpirationDt = generatePalletDto.ExpirationDt,
            Line = generatePalletDto.Line,
            Plu = generatePalletDto.Plu,
            BundleCount = (ushort)generatePalletDto.PluCharacteristic.BundleCount
        };
    }
}