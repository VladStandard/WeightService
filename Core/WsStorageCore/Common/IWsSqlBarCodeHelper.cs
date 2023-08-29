// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlBarCodeHelper
{
    int GetEanCheckDigit(string code);
    int GetGtinCheckDigitV1(string code);
    int GetGtinCheckDigitV2(string code);
    int GetGtinCheckDigitV3(string code);
    string GetGtinWithCheckDigit(string code, WsSqlEnumGtinVariant gtinVariant = WsSqlEnumGtinVariant.Var3);
}