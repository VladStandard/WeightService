using FluentValidation.Results;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallet;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Domain.Services.Features.Template;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Dto;
using Ws.Labels.Service.Features.PrintLabel.Exceptions;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Models;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Validators;
using Ws.Labels.Service.Features.PrintLabel.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Piece;

internal class LabelPieceGenerator(IZplResourceService zplResourceService, ITemplateService templateService,
    IStorageMethodService storageMethodService, IPalletService palletService)
{
    public void GeneratePiecePallet(LabelPiecePalletDto labelPalletDto, int labelCount)
    {
        string templateBody = string.Empty;
        string storageMethodBody = string.Empty;

        if (labelCount > 240)
            throw new LabelGenerateException("Превышен размер паллеты");

        if (labelPalletDto.Plu.IsCheckWeight)
            throw new LabelGenerateException("Плу весовая");

        if (Guid.TryParse(labelPalletDto.Plu.TemplateUid.ToString(), out Guid templateUid))
            templateBody = templateService.GetTemplateByUidFromCacheOrDb(templateUid) ?? string.Empty;

        if (storageMethodService.GetStorageByNameFromCacheOrDb(labelPalletDto.Plu.StorageMethod) is {} storageMethod)
            storageMethodBody = storageMethod;

        if (templateBody == string.Empty) throw new();
        if (storageMethodBody == string.Empty) throw new();

        XmlPieceLabelModel labelXml = labelPalletDto.AdaptToXmlPieceLabelModel();
        ValidationResult result = new XmlLabelPiecePalletValidator().Validate(labelXml);

        if (!result.IsValid)
            throw new LabelGenerateException(result);


        ZplItemsDto zplItems = new()
        {
            Resources = zplResourceService.GetAllResourcesFromCacheOrDb(),
            Template = templateBody,
            StorageMethod = storageMethodBody,
        };

        PalletEntity pallet = new()
        {
            Barcode = string.Empty,
            Weight = labelPalletDto.Weight,
            ProdDt = labelPalletDto.ProductDt,
            PalletMan = labelPalletDto.PalletMan,
        };

        IList<LabelEntity> labels = [];
        for (int i = 0 ; i < labelCount ; i++)
        {
            labelXml = labelPalletDto.AdaptToXmlPieceLabelModel();

            LabelEntity label = GenerateLabel(labelPalletDto, zplItems, labelXml);

            labels.Add(label);

            labelPalletDto = labelPalletDto with { ProductDt = labelPalletDto.ProductDt.AddSeconds(1) };
        }
        palletService.Create(pallet, labels);
    }

    private static LabelEntity GenerateLabel(LabelPiecePalletDto labelPalletDto, ZplItemsDto zplItems,
        XmlPieceLabelModel labelXml)
    {
        LabelReadyDto labelReady = LabelGeneratorUtils.GetZpl(zplItems, labelXml);
        return new()
        {
            Zpl = labelReady.Zpl,
            BarcodeBottom = labelReady.BarcodeBottom,
            BarcodeRight = labelReady.BarcodeRight,
            BarcodeTop = labelReady.BarcodeTop,
            WeightNet = 0,
            WeightTare = labelPalletDto.Plu.GetWeightWithCharacteristic(labelPalletDto.Characteristic),
            Kneading = labelPalletDto.Kneading,
            ProductDt = labelPalletDto.ProductDt,
            ExpirationDt = labelPalletDto.ExpirationDt,
            Line = labelPalletDto.Line,
            Plu = labelPalletDto.Plu
        };
    }
}