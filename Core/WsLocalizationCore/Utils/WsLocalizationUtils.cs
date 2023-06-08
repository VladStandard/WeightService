// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Utils;

/// <summary>
/// Константы локализации.
/// </summary>
public static class WsLocalizationUtils
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// ПО Печать этикеток.
    /// </summary>
    public static string AppLabelPrint => nameof(AppLabelPrint);

    #endregion

    #region Public and private methods

    /// <summary>
    /// Получить список языков.
    /// </summary>
    /// <returns></returns>
    public static List<string> GetListLanguages() =>
        (from object lang in Enum.GetValues(typeof(WsEnumLanguage)) select lang.ToString()).ToList();

    #endregion

}