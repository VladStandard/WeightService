namespace WsLocalizationCore.DeviceControlModels;

public class WsLocalizationDeviceControl: WsLocalizationBase
{
    protected WsLocalizationDeviceControl()
    {
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), @"Locales\DeviceControl.loc.json");
        if (File.Exists(fileName))
            LocalizationLoader.Instance.AddFile(fileName);
    }
}