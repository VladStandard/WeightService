using FluentValidation.Results;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallet;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Domain.Services.Features.Template;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Features.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Features.Piece.Models;
using Ws.Labels.Service.Features.PrintLabel.Models;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Validators;
using Ws.Labels.Service.Features.PrintLabel.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Features.Piece;

internal class LabelPieceGenerator(IZplResourceService zplResourceService, ITemplateService templateService,
    IStorageMethodService storageMethodService, IPalletService palletService)
{
    public void GeneratePiecePallet(GeneratePiecePalletDto generatePalletDto, int labelCount)
    {
        string templateBody = string.Empty;
        string storageMethodBody = string.Empty;

        if (labelCount > 240)
            throw new Exception("Превышен размер паллеты");


        if (Guid.TryParse(generatePalletDto.Plu.TemplateUid.ToString(), out Guid templateUid))
            templateBody = templateService.GetTemplateByUidFromCacheOrDb(templateUid) ?? string.Empty;

        if (storageMethodService.GetStorageByNameFromCacheOrDb(generatePalletDto.Plu.StorageMethod) is {} storageMethod)
            storageMethodBody = storageMethod;

        if (templateBody == string.Empty) throw new();
        if (storageMethodBody == string.Empty) throw new();

        XmlPieceLabelModel labelXml = generatePalletDto.AdaptToXmlPieceLabelModel();
        ValidationResult result = new XmlLabelPiecePalletValidator().Validate(labelXml);

        if (!result.IsValid)
            throw new Exception();


        ZplPrintItems zplPrintItems = new()
        {
            Resources = zplResourceService.GetAllResourcesFromCacheOrDb(),
            Template = templateBody,
            StorageMethod = storageMethodBody,
        };

        PalletEntity pallet = new()
        {
            Barcode = string.Empty,
            Weight = generatePalletDto.Weight,
            ProdDt = generatePalletDto.ProductDt,
            PalletMan = generatePalletDto.PalletMan,
        };

        IList<LabelEntity> labels = [];
        for (int i = 0 ; i < labelCount ; i++)
        {
            labelXml = generatePalletDto.AdaptToXmlPieceLabelModel();

            LabelEntity label = GenerateLabel(generatePalletDto, zplPrintItems, labelXml);

            labels.Add(label);

            generatePalletDto = generatePalletDto with { ProductDt = generatePalletDto.ProductDt.AddSeconds(1) };
        }
        palletService.Create(pallet, labels);
    }

    private static LabelEntity GenerateLabel(GeneratePiecePalletDto generatePalletDto, ZplPrintItems zplPrintItems, XmlPieceLabelModel labelXml)
    {
        ZplInfo ready = LabelGeneratorUtils.GetZpl(zplPrintItems, labelXml);

        return new()
        {
            Zpl = ready.Zpl,
            BarcodeBottom = ready.BarcodeBottom,
            BarcodeRight = ready.BarcodeRight,
            BarcodeTop = ready.BarcodeTop,
            WeightNet = 0,
            WeightTare = generatePalletDto.Plu.GetWeightWithCharacteristic(generatePalletDto.Characteristic),
            Kneading = generatePalletDto.Kneading,
            ProductDt = generatePalletDto.ProductDt,
            ExpirationDt = generatePalletDto.ExpirationDt,
            Line = generatePalletDto.Line,
            Plu = generatePalletDto.Plu
        };
    }
}