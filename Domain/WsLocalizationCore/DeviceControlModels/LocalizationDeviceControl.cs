namespace WsLocalizationCore.DeviceControlModels;

public class LocalizationDeviceControl: LocalizationBase
{
    protected LocalizationDeviceControl()
    {
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), @"Locales\DeviceControl.loc.json");
        if (File.Exists(fileName))
            LocalizationLoader.Instance.AddFile(fileName);
    }
}