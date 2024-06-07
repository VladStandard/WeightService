using FluentValidation.Results;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallets;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Features.Generate.Features.Piece.Models;
using Ws.Labels.Service.Features.Generate.Models;
using LabelGeneratorUtils = Ws.Labels.Service.Features.Generate.Utils.LabelGeneratorUtils;

namespace Ws.Labels.Service.Features.Generate.Features.Piece;

internal class LabelPieceGenerator(IPalletService palletService)
{
    public void GeneratePiecePallet(GeneratePiecePalletDto generatePalletDto, int labelCount)
    {
        Template template = new();
        string storageMethodBody = string.Empty;

        if (labelCount > 240)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        // if (Guid.TryParse(generatePalletDto.Plu.TemplateUid.ToString(), out Guid templateUid))
        //     template = templateService.GetTemplateByUidFromCacheOrDb(templateUid) ?? new();

        // if (storageMethodService.GetStorageByNameFromCacheOrDb(generatePalletDto.Plu.StorageMethod) is { } storageMethod)
        //     storageMethodBody = storageMethod;

        if (template.IsNew) throw new();
        if (storageMethodBody == string.Empty) throw new();

        XmlPieceLabel labelXml = generatePalletDto.AdaptToXmlPieceLabel();
        ValidationResult result = new XmlLabelPiecePalletValidator().Validate(labelXml);

        if (!result.IsValid)
            throw new Exception();


        ZplPrintItems zplPrintItems = new()
        {
            Resources = [],
            Template = template.Body,
            StorageMethod = storageMethodBody
        };

        Pallet pallet = new()
        {
            Barcode = string.Empty,
            Weight = generatePalletDto.Weight,
            ProdDt = generatePalletDto.ProductDt,
            PalletMan = generatePalletDto.PalletMan
        };

        IList<Label> labels = [];
        for (int i = 0 ; i < labelCount ; i++)
        {
            labelXml = generatePalletDto.AdaptToXmlPieceLabel();

            Label label = GenerateLabel(generatePalletDto, zplPrintItems, labelXml);

            labels.Add(label);

            generatePalletDto = generatePalletDto with { ProductDt = generatePalletDto.ProductDt.AddSeconds(1) };
        }
        palletService.Create(pallet, labels);
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
            WeightNet = 0,
            WeightTare = generatePalletDto.Plu.GetWeightByCharacteristic(generatePalletDto.PluCharacteristic),
            Kneading = generatePalletDto.Kneading,
            ProductDt = generatePalletDto.ProductDt,
            ExpirationDt = generatePalletDto.ExpirationDt,
            Line = generatePalletDto.Line,
            Plu = generatePalletDto.Plu
        };
    }
}