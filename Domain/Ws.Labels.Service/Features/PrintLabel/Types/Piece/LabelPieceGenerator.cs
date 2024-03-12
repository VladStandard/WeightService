using FluentValidation.Results;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;
using Ws.Domain.Services.Features.Pallet;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Dto;
using Ws.Labels.Service.Features.PrintLabel.Exceptions;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Models;
using Ws.Labels.Service.Features.PrintLabel.Types.Piece.Validators;
using Ws.Labels.Service.Features.PrintLabel.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Piece;

internal class LabelPieceGenerator(IZplResourceService zplResourceService, ILabelService labelService, IPalletService palletService)
{
    public void GeneratePiecePallet(LabelPiecePalletDto labelPalletDto, int labelCount)
    {
        if (labelCount > 240)
            throw new LabelGenerateException("Превышен размер паллеты");
        
        if (labelPalletDto.Nesting.Plu.IsCheckWeight)
            throw new LabelGenerateException("Плу весовая");
        
        XmlPieceLabelModel labelXml = labelPalletDto.AdaptToXmlPieceLabelModel();
        ValidationResult result = new XmlLabelPiecePalletValidator().Validate(labelXml);
        
        if (!result.IsValid)
            throw new LabelGenerateException(result);

        PalletEntity pallet = new()
        {
            Barcode = string.Empty,
            ProdDt = labelPalletDto.ProductDt,
            PalletMan = labelPalletDto.PalletMan,
        };
        palletService.Create(pallet);
        
        ZplItemsDto zplItems = new(
            labelPalletDto.Template, 
            labelPalletDto.Nesting.Plu.StorageMethod.Zpl, 
            zplResourceService.GetAllCachedResources()
        );
        
        for (int i = 0; i < labelCount; i++)
        {
            labelXml = labelPalletDto.AdaptToXmlPieceLabelModel();
            
            if (!result.IsValid)
                throw new LabelGenerateException(result);
            
            LabelEntity label = GenerateLabel(labelPalletDto, zplItems, labelXml);
            label.Pallet = pallet;
            
            labelService.Create(label);
            labelPalletDto = labelPalletDto with { ProductDt = labelPalletDto.ProductDt.AddSeconds(1) };
        }
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
            WeightTare = labelPalletDto.Nesting.WeightTare,
            Kneading = labelPalletDto.Kneading,
            ProductDt = labelPalletDto.ProductDt,
            ExpirationDt = labelPalletDto.ExpirationDt,
            Line = labelPalletDto.Line,
            Plu = labelPalletDto.Nesting.Plu
        };
    }
}