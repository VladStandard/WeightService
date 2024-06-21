using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Generate.Models.XmlLabelBase;

public abstract partial class BarcodeLabelLabel
{
    private string? _barCodeTop;
    private string? _barCodeRight;
    private string? _barCodeBottom;

    public required short Kneading { get; set; }
    public required DateTime ProductDt { get; set; } = DateTime.MinValue;

    public List<BarcodeItemCache> BarcodeRightTemplate { get; set; } = [];
    public List<BarcodeItemCache> BarcodeBottomTemplate { get; set; } = [];
    public List<BarcodeItemCache> BarcodeTopTemplate { get; set; } = [];

    protected string GenerateBarcode(List<BarcodeItemCache> barcodeTemplate)
    {
        StringBuilder barcodeBuilder = new();
        try
        {
            foreach (BarcodeItemCache item in barcodeTemplate)
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
        } catch (Exception ex)
        {
            throw new LabelGenerateException(LabelGenExceptions.BarcodeError);
        }
        return barcodeBuilder.ToString();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}