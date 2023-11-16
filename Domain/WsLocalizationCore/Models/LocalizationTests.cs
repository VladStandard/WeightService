namespace WsLocalizationCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class LocalizationTests : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public LocalizationTests()
    {
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), @"Locales\Tests.loc.json");
        if (File.Exists(fileName))
            LocalizationLoader.Instance.AddFile(fileName);
    }

    #endregion

    #region Public and private methods

    public string AllSettingsForAllDevicesWasAdded => Locale.Translate($"{LocalizationUtils.Tests}.{nameof(AllSettingsForAllDevicesWasAdded)}");
    public string NoDataInDb => Locale.Translate($"{LocalizationUtils.Tests}.{nameof(NoDataInDb)}");
    public string NoDataFor => Locale.Translate($"{LocalizationUtils.Tests}.{nameof(NoDataFor)}");
    public string Print => Locale.Translate($"{LocalizationUtils.Tests}.{nameof(Print)}");
    public string Records => Locale.Translate($"{LocalizationUtils.Tests}.{nameof(Records)}");
    public string SortingError => Locale.Translate($"{LocalizationUtils.Tests}.{nameof(SortingError)}");

    #endregion
}