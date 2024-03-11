using FluentValidation.Results;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Common;
using Ws.Labels.Service.Features.PrintLabel.Dto;
using Ws.Labels.Service.Features.PrintLabel.Exceptions;
using Ws.Labels.Service.Features.PrintLabel.Weight.Dto;
using Ws.Labels.Service.Features.PrintLabel.Weight.Models;
using Ws.Labels.Service.Features.PrintLabel.Weight.Validators;

namespace Ws.Labels.Service.Features.PrintLabel.Weight;

internal class LabelWeightGenerator(IZplResourceService zplResourceService, ILabelService labelService)
{
    public string GenerateLabel(LabelWeightDto labelDto)
    {
        if (labelDto.Nesting.Plu.IsCheckWeight == false)
            throw new LabelGenerateException("Плу не весовая");

        XmlWeightLabelModel labelXml = labelDto.AdaptToXmlWeightLabelModel();
        ValidationResult result = new XmlWeightLabelValidator().Validate(labelXml);
        if (!result.IsValid)
            throw new LabelGenerateException(result);


        ZplItemsDto zplItems = new(
            labelDto.Template, 
            labelDto.Nesting.Plu.StorageMethod.Zpl, 
            zplResourceService.GetAllCachedResources()
        );

        LabelReadyDto labelReady = LabelGenerator.GetZpl(zplItems, labelXml);

        LabelEntity labelSql = new()
        {
            Zpl = labelReady.Zpl,
            BarcodeBottom = labelReady.BarcodeBottom,
            BarcodeRight = labelReady.BarcodeRight,
            BarcodeTop = labelReady.BarcodeTop,
            WeightNet = labelDto.Weight,
            WeightTare = labelDto.Nesting.WeightTare,
            Kneading = labelDto.Kneading,
            ProductDt = labelDto.ProductDt,
            ExpirationDt = labelDto.ExpirationDt,
            Line = labelDto.Line,
            Plu = labelDto.Nesting.Plu
        };
        labelService.Create(labelSql);

        return labelReady.Zpl;
    }
}