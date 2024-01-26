using FluentValidation.Results;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Plu;
using Ws.Labels;
using Ws.Labels.Dto;
using Ws.LabelsService.Features.PrintLabel.Dto;
using Ws.LabelsService.Features.PrintLabel.Exceptions;
using Ws.LabelsService.Features.PrintLabel.Validators;

namespace Ws.LabelsService.Features.PrintLabel;

public class PrintLabelService(IPluService pluService) : IPrintLabelService
{
    public string GenerateLabel(LabelInfoDto labelInfo)
    {
        LabelInfoValidator validator = new();
        PluLineEntity pluLine = pluService.GetPluLineByPlu1СAndLineName(labelInfo.Plu1СGuid, labelInfo.LineName);
        
        if (pluLine.IsNew) throw new();
        
        ValidationResult result = validator.Validate(labelInfo);
        if (!result.IsValid) throw new LabelException(result);
        LabelDto label = LabelGenerator.GenerateLabel(labelInfo);

        PalletEntity pallet = new()
        {
            Kneading = labelInfo.Kneading,
            ProductDt = labelInfo.ProductDt,
            ExpirationDt = labelInfo.ExpirationDt,
            Line = pluLine.Line,
            Plu = pluLine.Plu,
        };
        SqlCoreHelper.Instance.Save(pallet);

        LabelEntity labelSql = new()
        {
            Zpl = label.Context,
            BarcodeBottom = label.BarcodeBottom,
            BarcodeRight = label.BarcodeRight,
            BarcodeTop = label.BarcodeTop,
            WeightNet = labelInfo.Weight,
            WeightTare = labelInfo.WeightTare,
            Pallet = pallet
        };
        SqlCoreHelper.Instance.Save(labelSql); 
        
        return label.Context;
    }
}