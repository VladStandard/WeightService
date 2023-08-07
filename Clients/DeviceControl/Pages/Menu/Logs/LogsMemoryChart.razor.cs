// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.Logs;

public sealed partial class LogsMemoryChart : SectionBase<WsSqlViewLogMemoryModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlViewLogMemoryRepository LogMemoryRepository { get; } = new();
    private Interpolation WsInterpolation { get; set; } = Interpolation.Line;
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
    private List<WsSqlAppModel> Apps { get; set; }
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
    private List<WsSqlDeviceModel> Devices { get; set; }

    private string _timeInterval = WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalToday;
    private string TimeInterval
    {
        get => _timeInterval;
        set
        {
            _timeInterval = value;
            SetSqlSectionCast();
        }
    }
    private IEnumerable<string> TimeIntervals { get; } = WsSqlEnumUtils.GetEnumerableTimeIntervals();
    private string MemoryAppColor { get; set; } = WsDebugHelper.Instance.IsDevelop ? Color.Blue.Name : Color.Black.Name;
    private IEnumerable<string> MemoryAppColors { get; } = WsSqlEnumUtils.GetEnumerableColors().Select(x => x.Name);
    private string MemoryTotalColor { get; set; } = WsDebugHelper.Instance.IsDevelop ? Color.Orange.Name : Color.LightBlue.Name;
    private IEnumerable<string> MemoryTotalColors { get; } = WsSqlEnumUtils.GetEnumerableColors().Select(x => x.Name);

    public LogsMemoryChart() : base()
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
        List<WsSqlDeviceModel> devices = ContextManager.DeviceRepository.GetList(sqlCrudConfig);
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
        SqlSectionCast = LogMemoryRepository.GetList(
            App.Name, Device.Name, WsSqlEnumUtils.GetTimeInterval(TimeInterval), 0);
    }

    #endregion
}