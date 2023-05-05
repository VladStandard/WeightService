// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// Barcode interface.
/// </summary>
public interface IWsSqlBarCodeHelper
{
    int GetEanCheckDigit(string code);
    int GetGtinCheckDigitV1(string code);
    int GetGtinCheckDigitV2(string code);
    int GetGtinCheckDigitV3(string code);
    string GetGtinWithCheckDigit(string code, WsSqlGtinVariant gtinVariant = WsSqlGtinVariant.Var3);
}