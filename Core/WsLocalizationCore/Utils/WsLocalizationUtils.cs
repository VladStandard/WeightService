// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
namespace WsLocalizationCore.Utils;

/// <summary>
/// Константы локализации.
/// </summary>
public static class WsLocalizationUtils
{
    #region Public and private fields, properties, constructor

    public static string LabelPrint => nameof(LabelPrint);

    #endregion

    #region Public and private methods

    public static List<string> GetListLanguages()
    {
        List<string> result = new();
        foreach (var lang in Enum.GetValues(typeof(WsEnumLanguage)))
        {
            result.Add(lang.ToString());
        }
        return result;
    }

    #endregion

}