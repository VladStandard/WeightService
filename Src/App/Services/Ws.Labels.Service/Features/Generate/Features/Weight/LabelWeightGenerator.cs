using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Domain.Services.Features.Template;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Features.Generate.Features.Weight.Models;
using Ws.Labels.Service.Features.Generate.Models;
using Ws.Labels.Service.Features.Generate.Utils;

namespace Ws.Labels.Service.Features.Generate.Features.Weight;

internal class LabelWeightGenerator(
    IZplResourceService zplResourceService,
    ITemplateService templateService,
    IStorageMethodService storageMethodService,
    ILabelService labelService
    )
{
    #region Private

    private string LoadTemplate(Guid? templateUid) =>
        templateService.GetTemplateByUidFromCacheOrDb(templateUid ?? Guid.Empty) ??
               throw new LabelGenerateException(LabelGenExceptionEnum.TemplateNotFound);

    private string LoadStorageMethod(string name) =>
        storageMethodService.GetStorageByNameFromCacheOrDb(name) ??
            throw new LabelGenerateException(LabelGenExceptionEnum.StorageMethodNotFound);

    private static void ValidateXmlWeightLabel(XmlWeightLabel model)
    {
        if (!new XmlWeightLabelValidator().Validate(model).IsValid)
            throw new LabelGenerateException(LabelGenExceptionEnum.Invalid);
    }

    #endregion

    public LabelEntity GenerateLabel(GenerateWeightLabelDto dto)
    {
        if (!dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptionEnum.Invalid);

        XmlWeightLabel labelXml = dto.ToXmlWeightLabel();
        ValidateXmlWeightLabel(labelXml);

        ZplPrintItems zplPrintItems = new()
        {
            Resources = zplResourceService.GetAllResourcesFromCacheOrDb(),
            Template = LoadTemplate(dto.Plu.TemplateUid),
            StorageMethod = LoadStorageMethod(dto.Plu.StorageMethod)
        };

        ZplInfo ready = LabelGeneratorUtils.GetZpl(zplPrintItems, labelXml);

        LabelEntity labelSql = new()
        {
            Zpl = ready.Zpl,
            BarcodeBottom = ready.BarcodeBottom,
            BarcodeRight = ready.BarcodeRight,
            BarcodeTop = ready.BarcodeTop,
            ProductDt = labelXml.ProductDt,
            ExpirationDt = labelXml.ExpirationDt,
            WeightNet = labelXml.Weight,
            Kneading = labelXml.Kneading,
            WeightTare = dto.Plu.GetWeightWithNesting,
            Line = dto.Line,
            Plu = dto.Plu
        };
        labelService.Create(labelSql);

        return labelSql;
    }
}