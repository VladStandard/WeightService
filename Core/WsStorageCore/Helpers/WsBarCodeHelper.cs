// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// Barcode helper.
/// </summary>
public class WsBarCodeHelper : IWsBarCodeHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsBarCodeHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsBarCodeHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    [XmlIgnore] private string TypeBarCodeGs128 => "GS1-128";
    [XmlIgnore] private string TypeBarCodeTop => "Interleaved 2 of 5 Bar Code";

    #endregion

    #region Public and private methods

    public int GetEanCheckDigit(string code)
    {
        if (code.Length != 12)
            throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 12 characters!");

        int sum = 0;
        // Calculate the checksum digit here.
        for (int i = code.Length; i >= 1; i--)
        {
            int digit = Convert.ToInt32(code.Substring(i - 1, 1));
            // This appears to be backwards but the EAN-13 checksum must be calculated this way to be compatible with UPC-A.
            if (i % 2 == 0) // odd
                sum += digit * 3;
            else            // even
                sum += digit * 1;
        }
        return (10 - sum % 10) % 10;
    }

    public int GetGtinCheckDigitV1(string code)
    {
        if (code.Length != 13)
            throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 13 characters!");

        int sum = 0;
        for (int i = 0; i < code.Length; i++)
        {
            int n = int.Parse(code.Substring(code.Length - 1 - i, 1));
            sum += i % 2 == 0 ? n * 3 : n;
        }
        return sum % 10 == 0 ? 0 : 10 - sum % 10;
    }

    public int GetGtinCheckDigitV2(string code)
    {
        if (code.Length != 13)
            throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 13 characters!");

        int sum = 0;
        char[]? list = code.Reverse().ToArray();
        for (int i = 0; i < list.Length; i++)
        {
            int n = (int)char.GetNumericValue(list[i]);
            sum += i % 2 == 0 ? n * 3 : n;
        }
        return sum % 10 == 0 ? 0 : 10 - sum % 10;
    }

    public int GetGtinCheckDigitV3(string code)
    {
        if (code.Length != 13)
            throw new ArgumentOutOfRangeException(nameof(code), "Value length must be 13 characters!");

        int sum = code.Reverse().Select((c, i) => (int)char.GetNumericValue(c) * (i % 2 == 0 ? 3 : 1)).Sum();
        return (10 - sum % 10) % 10;
    }

    public string GetGtinWithCheckDigit(string code, WsGtinVariant gtinVariant = WsGtinVariant.Var3)
    {
        if (string.IsNullOrEmpty(code))
            return string.Empty;

        return gtinVariant switch
        {
            WsGtinVariant.Var1 => $"{code}{GetGtinCheckDigitV3(code)}",
            WsGtinVariant.Var2 => $"{code}{GetGtinCheckDigitV2(code)}",
            _ => $"{code}{GetGtinCheckDigitV3(code)}"
        };
    }

    /// <summary>
    /// Set BarCodeTop from PluLabel.
    /// Example: 298987650000006722101713525011300335001
    /// </summary>
    /// <param name="barCode"></param>
    /// <param name="pluLabelContext"></param>
    public void SetBarCodeTop(WsSqlBarCodeModel barCode, WsPluLabelContextModel pluLabelContext)
    {
        barCode.TypeTop = TypeBarCodeTop;
        barCode.ValueTop = pluLabelContext.BarCodeTop;
    }

    /// <summary>
    /// Set BarCodeRight from PluLabel.
    /// Example: ;2999876500000067
    /// </summary>
    /// <param name="barCode"></param>
    /// <param name="pluLabelContext"></param>
    public void SetBarCodeRight(WsSqlBarCodeModel barCode, WsPluLabelContextModel pluLabelContext)
    {
        barCode.TypeRight = TypeBarCodeGs128;
        barCode.ValueRight = pluLabelContext.BarCodeRight;
    }

    ///  <summary>
    /// Set BarCodeBottom from PluLabel.
    ///  Example: ;0112600076000000310300033511221017102210
    ///  </summary>
    ///  <param name="barCode"></param>
    ///  <param name="pluLabelContext"></param>
    public void SetBarCodeBottom(WsSqlBarCodeModel barCode, WsPluLabelContextModel pluLabelContext)
    {
        barCode.TypeBottom = TypeBarCodeGs128;
        barCode.ValueBottom = pluLabelContext.BarCodeBottom;
    }

    //private string SetBarCodeInside(PluLabelModel pluLabel, PluLabelContextModel pluLabelContext, string template)
    //{
    //    string value = string.Empty;
    //    if (pluLabel.Zpl.Contains(template))
    //    {
    //        string zpl = pluLabel.Zpl;
    //        if (string.IsNullOrEmpty(zpl)) return value;
    //        if (string.IsNullOrEmpty(template)) return value;
    //        if (string.IsNullOrEmpty(SubTemplateFd)) return value;
    //        if (!zpl.Contains(SubTemplateFd)) return value;

    //        int start = zpl.IndexOf(template, StringComparison.Ordinal) + template.Length;
    //        zpl = zpl.Substring(start, pluLabel.Zpl.Length - start);
    //        zpl = zpl.Split('\n')[0];
    //        start = zpl.IndexOf(SubTemplateFd, StringComparison.Ordinal) + SubTemplateFd.Length;
    //        zpl = zpl.Substring(start, zpl.Length - start);
    //        value = zpl
    //            .TrimStart('\r', ' ', '\n', '\t', '>', ';')
    //            .TrimEnd('\r', ' ', '\n', '\t', '>', ';');
    //    }
    //    return value;
    //}

    #endregion
}