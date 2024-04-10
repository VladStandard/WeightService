using FluentValidation.Results;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Dto;
using Ws.Labels.Service.Features.PrintLabel.Exceptions;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Models;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Validators;
using Ws.Labels.Service.Features.PrintLabel.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Weight;

internal class LabelWeightGenerator(IZplResourceService zplResourceService, IPluService pluService, ILabelService labelService)
{
    public string GenerateLabel(LabelWeightDto labelDto)
    {
        if (labelDto.Plu.IsCheckWeight == false)
            throw new LabelGenerateException("Плу не весовая");

        XmlWeightLabelModel labelXml = labelDto.AdaptToXmlWeightLabelModel();
        ValidationResult result = new XmlWeightLabelValidator().Validate(labelXml);
        if (!result.IsValid)
            throw new LabelGenerateException(result);


        ZplItemsDto zplItems = new()
        {
            Resources = zplResourceService.GetAllCachedResources(),
            Template = pluService.GetPluCachedTemplate(labelDto.Plu),
            StorageMethod = labelDto.Plu.StorageMethod.Zpl,
        };

        LabelReadyDto labelReady = LabelGeneratorUtils.GetZpl(zplItems, labelXml);

        LabelEntity labelSql = new()
        {
            Zpl = labelReady.Zpl,
            BarcodeBottom = labelReady.BarcodeBottom,
            BarcodeRight = labelReady.BarcodeRight,
            BarcodeTop = labelReady.BarcodeTop,
            WeightNet = labelDto.Weight,
            WeightTare = labelDto.Plu.DefaultWeightTare,
            Kneading = labelDto.Kneading,
            ProductDt = labelDto.ProductDt,
            ExpirationDt = labelDto.ExpirationDt,
            Line = labelDto.Line,
            Plu = labelDto.Plu
        };
        labelService.Create(labelSql);

        return labelReady.Zpl;
    }
}