using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Ws.Domain.Models.ValueTypes;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Features.Generate.Models.XmlLabelBase;

public abstract partial class XmlLabelBaseModel
{
    [XmlIgnore] public required short Kneading { get; set; }
    [XmlIgnore] public required DateTime ExpirationDt { get; set; } = DateTime.MinValue;
    [XmlIgnore] public required DateTime ProductDt { get; set; } = DateTime.MinValue;

    [XmlIgnore] public List<BarcodeItem> BarcodeRightTemplate { get; set; } = [];
    [XmlIgnore] public List<BarcodeItem> BarcodeBottomTemplate { get; set; } = [];
    [XmlIgnore] public List<BarcodeItem> BarcodeTopTemplate { get; set; } = [];

    protected string GenerateBarcode(List<BarcodeItem> barcodeTemplate)
    {
        StringBuilder barcodeBuilder = new();
        try
        {
            foreach (BarcodeItem item in barcodeTemplate)
            {
                PropertyInfo? propertyInfo = GetType().GetProperty(item.Property);
                object? value = propertyInfo?.GetValue(this);

                if (item.IsConst)
                {
                    barcodeBuilder.Append(item.Property);
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
        } catch (Exception)
        {
            throw new LabelGenerateException(LabelGenExceptionEnum.BarcodeError);
        }
        return barcodeBuilder.ToString();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}