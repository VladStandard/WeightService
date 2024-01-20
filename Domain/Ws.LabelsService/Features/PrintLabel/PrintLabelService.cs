using FluentValidation.Results;
using Ws.Labels;
using Ws.Labels.Dto;
using Ws.LabelsService.Features.PrintLabel.Dto;
using Ws.LabelsService.Features.PrintLabel.Exceptions;
using Ws.LabelsService.Features.PrintLabel.Validators;
using Ws.Services.Features.Plu;
using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Entities.SchemaPrint.Pallets;
using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Helpers;

namespace Ws.LabelsService.Features.PrintLabel;

public class PrintLabelService : IPrintLabelService
{
    public string GenerateLabel(LabelInfoDto labelInfo)
    {
        LabelInfoValidator validator = new();
        SqlPluLineEntity pluLine = new PluService().GetPluLineByPlu1СAndLineName(labelInfo.Plu1СGuid, labelInfo.LineName);
        
        if (pluLine.IsNew) throw new();
        
        ValidationResult result = validator.Validate(labelInfo);
        if (!result.IsValid) throw new LabelException(result);
        LabelDto label = LabelGenerator.GenerateLabel(labelInfo);

        SqlPalletEntity pallet = new()
        {
            Kneading = labelInfo.Kneading,
            ProductDt = labelInfo.ProductDt,
            ExpirationDt = labelInfo.ExpirationDt,
            Line = pluLine.Line,
            Plu = pluLine.Plu,
        };
        SqlCoreHelper.Instance.Save(pallet);

        SqlLabelEntity labelSql = new()
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