using FluentValidation.Results;
using Ws.Labels;
using Ws.Labels.Dto;
using Ws.Services.Dto;
using Ws.Services.Exceptions;
using Ws.Services.Services.Plu;
using Ws.Services.Validators;
using Ws.StorageCore.Entities.SchemaScale.BarCodes;
using Ws.StorageCore.Entities.SchemaScale.PlusLabels;
using Ws.StorageCore.Entities.SchemaScale.PlusScales;
using Ws.StorageCore.Entities.SchemaScale.PlusWeightings;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Services.PrintLabel;

public class PrintLabelService : IPrintLabelService
{
    public string GenerateLabel(LabelInfoDto labelInfo)
    {
        LabelInfoValidator validator = new();
        SqlPluScaleEntity pluLine = new PluService().GetPluLineByPlu1СAndLineName(labelInfo.Plu1СGuid, labelInfo.LineName);
        
        if (pluLine.IsNew) throw new();
        
        ValidationResult result = validator.Validate(labelInfo);
        if (!result.IsValid) throw new LabelException(result);
        LabelDto label = LabelGenerator.GenerateLabel(labelInfo);


        SqlPluWeighingEntity? weight = null;
        if (labelInfo.IsCheckWeight)
        {
            weight = new()
            {
                Kneading = labelInfo.Kneading,
                PluScale = pluLine,
                NettoWeight = labelInfo.Weight,
                WeightTare = labelInfo.WeightTare
            };
            SqlCoreHelper.Instance.Save(weight); 
        }
        
        SqlPluLabelEntity labelSql = new()
        {
            Zpl = label.Context,
            ProductDt = labelInfo.ProductDt,
            ExpirationDt = labelInfo.ExpirationDt,
            PluScale = pluLine,
            PluWeighing = weight
        };
        SqlCoreHelper.Instance.Save(labelSql); 
        
        SqlBarCodeEntity barCode = new()
        {
            PluLabel = labelSql,
            ValueTop = label.BarcodeTop,
            ValueBottom = label.BarcodeBottom,
            ValueRight = label.BarcodeRight
        };
        SqlCoreHelper.Instance.Save(barCode);
        
        return label.Context;
    }
}