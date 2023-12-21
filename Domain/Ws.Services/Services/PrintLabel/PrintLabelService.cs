using FluentValidation.Results;
using Ws.Labels;
using Ws.Labels.Dto;
using Ws.Services.Dto;
using Ws.Services.Exceptions;
using Ws.Services.Services.Plu;
using Ws.Services.Validators;
using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Entities.SchemaPrint.Pallets;
using Ws.StorageCore.Entities.SchemaScale.PlusScales;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Services.PrintLabel;

public class PrintLabelService : IPrintLabelService
{
    public string GenerateLabel(LabelInfoDto labelInfo)
    {
        LabelInfoValidator validator = new();
        SqlPluScaleEntity pluLine = new PluService().GetPluLineByPlu1сAndLineName(labelInfo.Plu1СGuid, labelInfo.LineName);
        
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
            WeightTare = labelInfo.WeightTare
        };
        SqlCoreHelper.Instance.Save(labelSql); 
        
        return label.Context;
    }
}