namespace DeviceControl.Pages.Menu.Charts;

public sealed partial class ChartReports : SectionBase<WsSqlViewLogDeviceAggrModel>
{
    #region Public and private fields, properties, constructor

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

    private string _timeInterval = WsLocaleCore.DeviceControl.ChartTimeIntervalToday;
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
    private IEnumerable<string> Colors { get; set; } = new List<string>();

    private WsSqlViewLogDeviceAggrModel[][] Items { get; set; } = Array.Empty<WsSqlViewLogDeviceAggrModel[]>();

    public ChartReports() : base()
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
        IEnumerable<WsSqlViewLogDeviceAggrModel> items = ContextManager.ViewLogDeviceAggrRepository.GetList(
            App.Name, Device.Name, WsSqlEnumUtils.GetTimeInterval(TimeInterval), 0);
        //if (TimeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalAll)
        //    list = list.Select(x => new WsSqlViewLogDeviceAggrModel(x, DateTime.Today));
        //else if (TimeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalToday)
        //    list = list.Select(x => new WsSqlViewLogDeviceAggrModel(x, DateTime.Today));
        //else if (TimeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalMonth)
        //    list = list.Select(x => new WsSqlViewLogDeviceAggrModel(x, 
        //        WsDataFormatUtils.GetDateTimeMonth(x.CreateDt)));
        //else if (TimeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalYear)
        //    list = list.Select(x => new WsSqlViewLogDeviceAggrModel(x));
        SqlSectionCast = items.ToList();
        Items = items
            .Select((item, index) => new { Item = item, Index = index })
            .GroupBy(x => x.Index / SqlSectionCast.Count, x => x.Item)
            .Select(group => group.ToArray())
            .ToArray();
        
        // Fill random colors.
        Colors = items.Select(_ =>
        {
            Random random = new();
            // Generate random color components (R, G, B) between 0 and 255
            int red = random.Next(256);
            int green = random.Next(256);
            int blue = random.Next(256);
            // Create a new Color instance with the random components
            Color randomColor = Color.FromArgb(red, green, blue);
            return randomColor.Name;
        });
    }

    #endregion
}
