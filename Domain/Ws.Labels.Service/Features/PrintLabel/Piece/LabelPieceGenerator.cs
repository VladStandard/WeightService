using FluentValidation.Results;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Print;
using Ws.Labels.Service.Features.PrintLabel.Common;
using Ws.Labels.Service.Features.PrintLabel.Exceptions;
using Ws.Labels.Service.Features.PrintLabel.Piece.Dto;
using Ws.Labels.Service.Features.PrintLabel.Piece.Models;
using Ws.Labels.Service.Features.PrintLabel.Piece.Validators;

namespace Ws.Labels.Service.Features.PrintLabel.Piece;

public class LabelPieceGenerator
{
    public string GenerateLabel(LabelPieceDto labelDto)
    {
        if (labelDto.Nesting.Plu.IsCheckWeight)
            throw new LabelGenerateException("Плу весовая");

        XmlPieceLabelModel labelXml = labelDto.AdaptToXmlPieceLabelModel();
        ValidationResult result = new XmlLabelPieceValidator().Validate(labelXml);
        if (!result.IsValid)
            throw new LabelGenerateException(result);


        LabelReadyDto labelReady = LabelGenerator.GetZpl(labelDto.Template, labelDto.Nesting.Plu, labelXml);

        LabelEntity labelSql = new()
        {
            Zpl = labelReady.Zpl,
            BarcodeBottom = labelReady.BarcodeBottom,
            BarcodeRight = labelReady.BarcodeRight,
            BarcodeTop = labelReady.BarcodeTop,
            WeightNet = 0,
            WeightTare = labelDto.Nesting.WeightTare,
            Kneading = labelDto.Kneading,
            ProductDt = labelDto.ProductDt,
            ExpirationDt = labelDto.ExpirationDt,
            Line = labelDto.Line,
            Plu = labelDto.Nesting.Plu
        };
        SqlCoreHelper.Instance.Save(labelSql);

        return labelReady.Zpl;
    }
    
    public void GeneratePiecePallet(LabelPieceDto labelDto, PalletEntity pallet, int labelCount)
    {
        if (labelDto.Nesting.Plu.IsCheckWeight)
            throw new LabelGenerateException("Плу весовая");

        XmlPieceLabelModel labelXml = labelDto.AdaptToXmlPieceLabelModel();
        ValidationResult result = new XmlLabelPieceValidator().Validate(labelXml);
        if (!result.IsValid)
            throw new LabelGenerateException(result);
        
        for (int i = 0; i < labelCount; i++)
        {
            LabelReadyDto labelReady = LabelGenerator.GetZpl(labelDto.Template, labelDto.Nesting.Plu, labelXml);
    
            LabelEntity labelSql = new()
            {
                Zpl = labelReady.Zpl,
                BarcodeBottom = labelReady.BarcodeBottom,
                BarcodeRight = labelReady.BarcodeRight,
                BarcodeTop = labelReady.BarcodeTop,
                WeightNet = 0,
                Pallet = pallet,
                WeightTare = labelDto.Nesting.WeightTare,
                Kneading = labelDto.Kneading,
                ProductDt = labelDto.ProductDt,
                ExpirationDt = labelDto.ExpirationDt,
                Line = labelDto.Line,
                Plu = labelDto.Nesting.Plu
            };
            SqlCoreHelper.Instance.Save(labelSql);

            labelDto.ProductDt = labelDto.ProductDt.AddSeconds(1);
        }
    }
}