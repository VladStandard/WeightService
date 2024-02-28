using FluentValidation.Results;
using Ws.Database.Core.Utils;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Common;
using Ws.Labels.Service.Features.PrintLabel.Exceptions;
using Ws.Labels.Service.Features.PrintLabel.Weight.Dto;
using Ws.Labels.Service.Features.PrintLabel.Weight.Models;
using Ws.Labels.Service.Features.PrintLabel.Weight.Validators;

namespace Ws.Labels.Service.Features.PrintLabel.Weight;

public class LabelWeightGenerator(IZplResourceService zplResourceService)
{
    public string GenerateLabel(LabelWeightDto labelDto)
    {
        if (labelDto.Nesting.Plu.IsCheckWeight == false)
            throw new LabelGenerateException("Плу не весовая");

        XmlWeightLabelModel labelXml = labelDto.AdaptToXmlWeightLabelModel();
        ValidationResult result = new XmlWeightLabelValidator().Validate(labelXml);
        if (!result.IsValid)
            throw new LabelGenerateException(result);


        LabelReadyDto labelReady = new LabelGenerator(zplResourceService)
            .GetZpl(labelDto.Template, labelDto.Nesting.Plu, labelXml);

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
        SqlCoreHelper.Save(labelSql);

        return labelReady.Zpl;
    }
}