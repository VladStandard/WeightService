// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class WsLocalizationTests : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public WsLocalizationTests()
    {
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), @"Locales\Tests.loc.json");
        if (File.Exists(fileName))
            LocalizationLoader.Instance.AddFile(fileName);
    }

    #endregion

    #region Public and private methods

    public string AllSettingsForAllDevicesWasAdded => Locale.Translate($"{WsLocalizationUtils.Tests}.{nameof(AllSettingsForAllDevicesWasAdded)}");
    public string NoDataInDb => Locale.Translate($"{WsLocalizationUtils.Tests}.{nameof(NoDataInDb)}");
    public string NoDataFor => Locale.Translate($"{WsLocalizationUtils.Tests}.{nameof(NoDataFor)}");
    public string Print => Locale.Translate($"{WsLocalizationUtils.Tests}.{nameof(Print)}");
    public string Records => Locale.Translate($"{WsLocalizationUtils.Tests}.{nameof(Records)}");
    public string SortingError => Locale.Translate($"{WsLocalizationUtils.Tests}.{nameof(SortingError)}");

    #endregion
}