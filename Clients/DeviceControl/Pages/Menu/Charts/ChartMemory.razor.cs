// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.Charts;

public sealed partial class ChartMemory : SectionBase<WsSqlViewLogMemoryModel>
{
    #region Public and private fields, properties, constructor

    private Interpolation WsInterpolation { get; set; } = Interpolation.Step;
    private WsSqlAppModel _app;
    private WsSqlAppModel App
    {
        get => _app;
        set
        {
            _app = value;
            SetSqlSectionCast();
        }
    }
    private List<WsSqlAppModel> Apps { get; }
    private WsSqlDeviceModel _device;
    private WsSqlDeviceModel Device
    {
        get => _device;
        set
        {
            _device = value;
            SetSqlSectionCast();
        }
    }
    private List<WsSqlDeviceModel> Devices { get; }

    private string _timeInterval = WsLocaleCore.DeviceControl.ChartTimeIntervalToday;
    private string TimeInterval
    {
        get => _timeInterval;
        set
        {
            _timeInterval = value;
            if (_timeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalYear ||
                _timeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalAll)
                JsService.ShowAlert(WsLocaleCore.DeviceControl.ChartTimeIntervalAllAlert).ConfigureAwait(true);
            SetSqlSectionCast();
        }
    }
    private IEnumerable<string> TimeIntervals { get; } = WsSqlEnumUtils.GetEnumerableTimeIntervals();
    private string MemoryAppColor { get; set; } = WsDebugHelper.Instance.IsDevelop ? Color.Gold.Name : Color.DarkGray.Name;
    private IEnumerable<string> MemoryAppColors { get; } = WsSqlEnumUtils.GetEnumerableColors().Select(x => x.Name);
    private string MemoryFreeColor { get; set; } = WsDebugHelper.Instance.IsDevelop ? Color.Gold.Name : Color.DarkGray.Name;
    private IEnumerable<string> MemoryFreeColors { get; } = WsSqlEnumUtils.GetEnumerableColors().Select(x => x.Name);

    public ChartMemory() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();

        // Приложение по-умолчанию.
        _app = ContextManager.AppRepository.GetNewItem();
        Apps = new() { App };
        // Устройство по-умолчанию.
        _device = ContextManager.DeviceRepository.GetCurrentDevice();
        Devices = new() { Device };

        // Загрузить приложения из БД.
        WsSqlCrudConfigModel sqlCrudConfig = new(SqlCrudConfigSection) { SelectTopRowsCount = 0 };
        List<WsSqlAppModel> apps = ContextManager.AppRepository.GetList(sqlCrudConfig);
        if (!apps.Any())
            apps = new() { App };
        else
        {
            WsSqlAppModel? app = apps.Find(item => item.Name.Equals(WsLocalizationUtils.DeviceControlAppName));
            if (app is not null && app.IsExists)
                _app = app;
        }
        Apps.Clear();
        Apps.Add(ContextManager.AppRepository.GetNewItem());
        Apps.AddRange(apps);
        // Загрузить устройства из БД.
        List<WsSqlDeviceModel> devices = ContextManager.DeviceRepository.GetEnumerable(sqlCrudConfig).ToList();
        if (!devices.Any())
            devices = new() { Device };
        else
        {
            WsSqlDeviceModel? device = devices.Find(item => item.Name.Equals(WsLocalizationUtils.DeviceControlAppName));
            if (device is not null && device.IsExists)
                _device = device;
        }
        Devices.Clear();
        Devices.Add(ContextManager.DeviceRepository.GetNewItem());
        Devices.AddRange(devices);

        // Лог памяти.
        new WsSqlLogMemoryRepository().Save();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ContextManager.ViewLogMemoryRepository.GetList(
            App.Name, Device.Name, WsSqlEnumUtils.GetTimeInterval(TimeInterval), 0).ToList();
    }

    #endregion
}
