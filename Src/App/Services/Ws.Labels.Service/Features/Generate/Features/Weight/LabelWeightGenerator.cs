using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Labels;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Features.Generate.Features.Weight.Models;
using Ws.Labels.Service.Features.Generate.Models;
using Ws.Labels.Service.Features.Generate.Models.Cache;
using Ws.Labels.Service.Features.Generate.Services;
using Ws.Labels.Service.Features.Generate.Utils;

namespace Ws.Labels.Service.Features.Generate.Features.Weight;

internal class LabelWeightGenerator(
    CacheService cacheService,
    ILabelService labelService
    )
{
    #region Private

    private TemplateCache LoadTemplate(Guid? templateUid) =>
        cacheService.GetTemplateByUidFromCacheOrDb(templateUid ?? Guid.Empty) ??
               throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

    private string LoadStorageMethod(string name) =>
        cacheService.GetStorageByNameFromCacheOrDb(name) ??
            throw new LabelGenerateException(LabelGenExceptions.StorageMethodNotFound);

    private static void ValidateXmlWeightLabel(XmlWeightLabel model)
    {
        if (!new XmlWeightLabelValidator().Validate(model).IsValid)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);
    }

    #endregion

    public Label GenerateLabel(GenerateWeightLabelDto dto)
    {
        if (!dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        XmlWeightLabel labelXml = dto.ToXmlWeightLabel();

        ValidateXmlWeightLabel(labelXml);

        TemplateCache template = LoadTemplate(dto.Plu.TemplateUid);

        ZplPrintItems zplPrintItems = new()
        {
            Resources = cacheService.GetAllResourcesFromCacheOrDb(),
            Template = template.Body,
            StorageMethod = LoadStorageMethod(dto.Plu.StorageMethod)
        };

        labelXml.BarcodeBottomTemplate = template.BarcodeTopBody;
        labelXml.BarcodeRightTemplate = template.BarcodeRightBody;
        labelXml.BarcodeTopTemplate = template.BarcodeTopBody;


        ZplInfo ready = LabelGeneratorUtils.GetZpl(zplPrintItems, labelXml);

        Label labelSql = new()
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