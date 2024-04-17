using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Label;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Domain.Services.Features.Template;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight.Dto.PrintWeightPlu;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight.Models.XmlWeightLabel;
using Ws.Labels.Service.Features.PrintLabel.Models;
using Ws.Labels.Service.Features.PrintLabel.Utils;

namespace Ws.Labels.Service.Features.PrintLabel.Features.Weight;

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
               throw new LabelWeightGenerateException(LabelGenExceptionEnum.TemplateNotFound);

    private string LoadStorageMethod(string name) =>
        storageMethodService.GetStorageByNameFromCacheOrDb(name) ??
            throw new LabelWeightGenerateException(LabelGenExceptionEnum.StorageMethodNotFound);

    private static void ValidateXmlWeightLabel(XmlWeightLabelModel model)
    {
        if (!new XmlWeightLabelValidator().Validate(model).IsValid)
            throw new LabelWeightGenerateException(LabelGenExceptionEnum.Invalid);
    }

    #endregion

    public LabelEntity GenerateLabel(GenerateWeightLabelDto dto)
    {
        if (!dto.Plu.IsCheckWeight)
            throw new LabelWeightGenerateException(LabelGenExceptionEnum.Invalid);

        XmlWeightLabelModel labelXml = dto.ToXmlWeightLabel();
        ValidateXmlWeightLabel(labelXml);

        ZplPrintItems zplPrintItems = new()
        {
            Resources = zplResourceService.GetAllResourcesFromCacheOrDb(),
            Template = LoadTemplate(dto.Plu.TemplateUid),
            StorageMethod = LoadStorageMethod(dto.Plu.StorageMethod),
        };

        ZplInfo ready = LabelGeneratorUtils.GetZpl(zplPrintItems, labelXml);

        LabelEntity labelSql = new()
        {
            Zpl = ready.Zpl,
            BarcodeBottom = ready.BarcodeBottom,
            BarcodeRight = ready.BarcodeRight,
            BarcodeTop = ready.BarcodeTop,
            WeightNet = dto.Weight,
            WeightTare = dto.Plu.DefaultWeightTare,
            Kneading = dto.Kneading,
            ProductDt = dto.ProductDt,
            Line = dto.Line,
            Plu = dto.Plu
        };
        // labelService.Create(labelSql);

        return labelSql;
    }
}