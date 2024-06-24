using System.Reflection;
using System.Text;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Generate.Common.BarcodeLabel;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Generate.Common;

public abstract class BarcodeGeneratorModel : IBarcodeLabel
{
    #region Line

    public required int LineNumber { get; init; }
    public required int LineCounter { get; init; }

    #endregion

    #region Plu

    public required short PluNumber { get; init; }
    public required string PluGtin { get; init; }

    #endregion

    #region BarcodeTemplates

    public required short Kneading { get; init; }
    public required DateTime ProductDt { get; init; } = DateTime.MinValue;

    #endregion

    public string GenerateBarcode(List<BarcodeItemCache> barcodeTemplate)
    {
        StringBuilder barcodeBuilder = new();
        bool firstSpecialConst = true;
        try
        {
            foreach (BarcodeItemCache item in barcodeTemplate)
            {
                PropertyInfo? propertyInfo = GetType().GetProperty(item.Property);
                object? value = propertyInfo?.GetValue(this);

                if (item.IsConst)
                {
                    if (item.Property.StartsWith('(') && firstSpecialConst == false)
                        item.Property = $">8{item.Property}";

                    barcodeBuilder.Append(item.Property);
                    firstSpecialConst = false;
                    continue;
                }

                switch (value)
                {
                    case int or short when Convert.ToInt32(value) > 0:
                        barcodeBuilder.AppendStrWithPadding(value.ToString(), item.Length);
                        break;
                    case string strValue when strValue.IsDigitsOnly():
                        barcodeBuilder.AppendStrWithPadding(strValue, item.Length);
                        break;
                    case float or decimal or double when Convert.ToDecimal(value) > 0:
                        barcodeBuilder.AppendStrWithPadding(Convert.ToDecimal(value).ToSepStr(), item.Length);
                        break;
                    case DateTime dateValue when item.FormatStr.IsDateFormat():
                        barcodeBuilder.Append(dateValue.ToString(item.FormatStr));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        } catch (Exception ex)
        {
            throw new LabelGenerateException(LabelGenExceptions.BarcodeError);
        }
        return barcodeBuilder.ToString();
    }
}