using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Ws.Labels.Service.Extensions;
using Ws.Labels.Service.Generate.Exceptions;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Extensions;
using Ws.Shared.Utils;

namespace Ws.Labels.Service.Generate.Common;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
public partial record BarcodeReadyModel
{
    [GeneratedRegex(@"[^0-9]")]
    private static partial Regex NonDigitRegex();

    // ReSharper disable once NotAccessedField.Global
    public required string Zpl;

    public required string Friendly;

    public string Clean => NonDigitRegex().Replace(Friendly, "");
}

public partial record BarcodeModel : IBarcodeLabel
{
    [GeneratedRegex(@"[^0-9\(\)]")]
    private static partial Regex NotFriendlyChars();

    public BarcodeReadyModel GenerateBarcode(List<BarcodeItemTemplateFromCache> barcodeTemplate)
    {
        StringBuilder barcodeZplBuilder = new();
        try
        {
            foreach (BarcodeItemTemplateFromCache item in barcodeTemplate)
            {
                PropertyInfo? propertyInfo = GetType().GetProperty(item.Property);
                object? value = propertyInfo?.GetValue(this);

                if (item.IsConst)
                {
                    barcodeZplBuilder.Append(item.Property);
                    continue;
                }

                switch (value)
                {
                    case int or short when Convert.ToInt32(value) > 0:
                        barcodeZplBuilder.AppendStrWithPadding(value.ToString(), item.Length);
                        break;
                    case string strValue when strValue.IsDigitsOnly():
                        barcodeZplBuilder.AppendStrWithPadding(strValue, item.Length);
                        break;
                    case float or decimal or double when Convert.ToDecimal(value) > 0:
                        barcodeZplBuilder.AppendStrWithPadding(Convert.ToDecimal(value).ToSepStr(), item.Length);
                        break;
                    case DateTime dateValue when item.FormatStr.IsDateFormat():
                        barcodeZplBuilder.Append(dateValue.ToString(item.FormatStr));
                        break;
                    default:
                       throw new ApiExceptionServer
                       {
                           ErrorDisplayMessage = EnumUtils.GetEnumDescription(LabelGenExceptions.BarcodeInvalid),
                        };
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = EnumUtils.GetEnumDescription(LabelGenExceptions.BarcodeInvalid),
                ErrorInternalMessage = ex.Message,
            };
        }

        string barcodeFriendlyBuilder = NotFriendlyChars().Replace(barcodeZplBuilder.ToString(), "");

        return new()
        {
            Zpl = barcodeZplBuilder.Replace("(", "").Replace(")", "").ToString(),
            Friendly = barcodeFriendlyBuilder
        };
    }

    #region Line

    public required int LineNumber { get; init; }
    public required int LineCounter { get; init; }

    #endregion

    #region Plu

    public required short PluNumber { get; init; }
    public required string PluGtin { get; init; }
    public required string PluEan13 { get; init; }

    #endregion

    #region BarcodeTemplates

    public required short Kneading { get; init; }
    public required DateTime ExpirationDt { get; init; }
    public required DateTime ProductDt { get; init; }
    public required short BundleCount { get; init; }
    public required decimal WeightNet { get; init; }
    public required int ExpirationDay { get; init; }

    #endregion
}