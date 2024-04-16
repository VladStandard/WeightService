using FluentValidation.Results;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Domain.Services.Features.Template;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Dto;
using Ws.Labels.Service.Features.PrintLabel.Exceptions;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Dto;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Models;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Validators;
using Ws.Labels.Service.Features.PrintLabel.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Weight;

internal class LabelWeightGenerator(IZplResourceService zplResourceService, ITemplateService templateService, IStorageMethodService storageMethodService, ILabelService labelService)
{
    public string GenerateLabel(LabelWeightDto labelDto)
    {
        string storageMethodBody = string.Empty;
        string templateBody = string.Empty;

        if (labelDto.Plu.IsCheckWeight == false)
            throw new LabelGenerateException("Плу не весовая");

        if (Guid.TryParse(labelDto.Plu.TemplateUid.ToString(), out Guid templateUid))
            templateBody = templateService.GetTemplateByUidFromCacheOrDb(templateUid) ?? string.Empty;

        if (storageMethodService.GetStorageByNameFromCacheOrDb(labelDto.Plu.StorageMethod) is {} storageMethod)
            storageMethodBody = storageMethod;

        if (templateBody == string.Empty) throw new();
        if (storageMethodBody == string.Empty) throw new();

        XmlWeightLabelModel labelXml = labelDto.AdaptToXmlWeightLabelModel();
        ValidationResult result = new XmlWeightLabelValidator().Validate(labelXml);
        if (!result.IsValid)
            throw new LabelGenerateException(result);

        ZplItemsDto zplItems = new()
        {
            Resources = zplResourceService.GetAllResourcesFromCacheOrDb(),
            Template = templateBody,
            StorageMethod = storageMethodBody,
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
        // labelService.Create(labelSql);

        return labelReady.Zpl;
    }
}