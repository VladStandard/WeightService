namespace WsLocalizationCore.Utils;

public static class LocalizationUtils
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// ПО Печать этикеток.
    /// </summary>
    public static string AppLabelPrint => nameof(AppLabelPrint);
    public static string Tests => nameof(Tests);

    #endregion

    #region Public and private methods

    /// <summary>
    /// Получить список языков.
    /// </summary>
    /// <returns></returns>
    public static List<string> GetListLanguages() =>
        (from object lang in Enum.GetValues(typeof(EnumLanguage)) select lang.ToString()).ToList();
    

    #endregion
}