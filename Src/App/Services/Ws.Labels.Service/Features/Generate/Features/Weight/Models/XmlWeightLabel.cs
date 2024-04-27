using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using Microsoft.IdentityModel.Tokens;
using Ws.Domain.Models.ValueTypes;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Features.Generate.Common.XmlBarcode;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;

namespace Ws.Labels.Service.Features.Generate.Features.Weight.Models;

[Serializable]
public class XmlWeightLabel : XmlLabelBaseModel, IXmlBarcodeWeightXml
{
    [XmlIgnore] public required decimal Weight { get; set; }

    [XmlElement]
    public string WeightStr
    {
        get => Weight.ToStrWithSep(",");
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeTop
    {
        get => GenerateBarcode(BarcodeTopTemplate);
        set => _ = value;
    }

    [XmlElement]
    public override string BarCodeRight
    {
        get => GenerateBarcode(BarcodeRightTemplate);
        set => _ = value;
    }

    [XmlElement] public override string BarCodeBottom
    {
        get => GenerateBarcode(BarcodeBottomTemplate);
        set => _ = value;
    }

    protected string GenerateBarcode(List<BarcodeItem> barcodeTemplate)
    {
        StringBuilder barcodeBuild = new();
        try
        {

            foreach (BarcodeItem item in barcodeTemplate)
            {
                PropertyInfo? propertyInfo = GetType().GetProperty(item.Property);
                object? value = propertyInfo?.GetValue(this);

                if (item.IsConst)
                {
                    barcodeBuild.Append(item.Property);
                    continue;
                }

                switch (value)
                {
                    case int intValue:
                        barcodeBuild.Append(intValue.ToStrLenWithZero(item.Length));
                        break;
                    case short shortValue:
                        barcodeBuild.Append(shortValue.ToStrLenWithZero(item.Length));
                        break;
                    case string strValue:
                        barcodeBuild.Append(strValue.ToLenWithZero(item.Length));
                        break;
                    case decimal decimalValue:
                        barcodeBuild.Append(decimalValue.ToStrWithLen(item.Length));
                        break;
                    case DateTime dateValue:
                        if (item.FormatStr.IsNullOrEmpty())
                            barcodeBuild.Append(dateValue.ToString("yyMMdd"));
                        else
                            barcodeBuild.Append(dateValue.ToString(item.FormatStr));

                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        } catch (Exception ex)
        {
            throw new LabelGenerateException(LabelGenExceptionEnum.Invalid);
        }
        return barcodeBuild.ToString();
    }
}