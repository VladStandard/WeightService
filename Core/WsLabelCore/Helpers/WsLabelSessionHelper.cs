// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Helpers;

#nullable enable
/// <summary>
/// User session.
/// </summary>
public sealed class WsLabelSessionHelper : BaseViewModel, INotifyPropertyChanged
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsLabelSessionHelper _instance;
#pragma warning restore CS8618
    public static WsLabelSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private DebugHelper Debug => DebugHelper.Instance;
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public WsPluginPrintModel PluginPrintMain { get; } = new();
    public WsPluginPrintModel PluginPrintShipping { get; } = new();
    private ProductSeriesDirect ProductSeries { get; set; } = new();
    public PrintBrand PrintBrandMain =>
        Scale.PrinterMain is not null && Scale.PrinterMain.PrinterType.Name.Contains("TSC ") ? PrintBrand.Tsc : PrintBrand.Zebra;
    public PrintBrand PrintBrandShipping =>
        Scale.PrinterShipping is not null && Scale.PrinterShipping.PrinterType.Name.Contains("TSC ") ? PrintBrand.Tsc : PrintBrand.Zebra;
    public WsSqlPluWeighingModel PluWeighing { get; set; }
    public WsWeighingSettingsModel WeighingSettings { get; set; }
    private WsSqlPluScaleModel _pluScale;
    public WsSqlPluScaleModel PluScale
    {
        get => _pluScale;
        set
        {
            _pluScale = value;
            // Журналирование смены ПЛУ на линии.
            ContextManager.ContextItem.SaveLogInformation($"{LocaleCore.Scales.SetPlu(_pluScale.Plu.Number, _pluScale.Plu.Name)}");
            if (_pluScale.IsNotNew)
            {
                WsSqlBundleModel bundle = ContextManager.ContextBundle.GetItem(_pluScale.Plu);
                if (bundle.IsExists)
                {
                    _pluScale.Plu.PackageTypeGuid = bundle.IdentityValueUid;
                    _pluScale.Plu.PackageTypeName = bundle.Name;
                    _pluScale.Plu.PackageTypeWeight = bundle.Weight;
                }
                WsSqlClipModel clip = WsSqlContextManagerHelper.Instance.ContextClip.GetItem(_pluScale.Plu);
                if (clip.IsExists)
                {
                    _pluScale.Plu.ClipTypeGuid = clip.IdentityValueUid;
                    _pluScale.Plu.ClipTypeName = clip.Name;
                    _pluScale.Plu.ClipTypeWeight = clip.Weight;
                }
            }
            PluginPrintMain.LabelPrintedCount = 1;
            PluginPrintShipping.LabelPrintedCount = 1;
            ContextCache.LoadLocalViewPlusNesting((ushort)value.Plu.Number);
            ViewPluNesting = ContextCache.LocalViewPlusNesting.Any() 
                ? ContextCache.LocalViewPlusNesting.First(item => item.IsDefault) ?? new() : new();
            OnPropertyChanged();
        }
    }
    public ushort PlusPageColumnCount => 4;
    public ushort PlusPageSize => 16;
    public ushort PlusPageRowCount => 4;
    public int PlusPageNumber { get; set; }
    public WsSqlDeviceScaleFkModel DeviceScaleFk { get; set; }
    private WsSqlProductionFacilityModel _productionFacility;
    public WsSqlProductionFacilityModel ProductionFacility
    {
        get =>
            _productionFacility.IsNotNew
                ? _productionFacility : Scale.WorkShop is not null ? Scale.WorkShop.ProductionFacility : _productionFacility;
        set
        {
            _productionFacility = value;
            OnPropertyChanged();
        }
    }
    
    private WsSqlScaleModel _scale;
    public WsSqlScaleModel Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            // Журналирование смены линии.
            ContextManager.ContextItem.SaveLogInformation($"{LocaleCore.Scales.SetLine(_scale.IdentityValueId, _scale.Description)}");
            // Сбросить ПЛУ линии.
            PluScale = ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluScaleModel>();
            // Сбросить вложенность.
            ViewPluNesting = ContextManager.ContextPluNesting.GetNewView();
            OnPropertyChanged();
        }
    }
    public string PublishDescription { get; set; } = "";
    private DateTime ProductDateMaxValue => DateTime.Now.AddDays(+31);
    private DateTime ProductDateMinValue => DateTime.Now.AddDays(-31);
    public DateTime ProductDate { get; set; }
    public string DeviceName => MdNetUtils.GetLocalDeviceName(false);

    private WsSqlViewPluNestingModel _viewPluNesting;
    public WsSqlViewPluNestingModel ViewPluNesting
    {
        get => _viewPluNesting;
        set
        {
            _viewPluNesting = value;
            // Журналирование смены вложенности ПЛУ.
            ContextManager.ContextItem.SaveLogInformation(
                $"{LocaleCore.Scales.SetPluNesting(_viewPluNesting.PluNumber, _viewPluNesting.PluName, _viewPluNesting.BundleCount)}");
            OnPropertyChanged();
        }
    }
    private readonly object _locker = new();

    public WsLabelSessionHelper()
    {
        // Items.
        _pluScale = ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluScaleModel>();
        PluWeighing = ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluWeighingModel>();
        DeviceScaleFk = ContextManager.AccessItem.GetItemNewEmpty<WsSqlDeviceScaleFkModel>();
        _productionFacility = ContextManager.AccessItem.GetItemNewEmpty<WsSqlProductionFacilityModel>();
        _scale = ContextManager.AccessItem.GetItemNewEmpty<WsSqlScaleModel>();
        // Others.
        WeighingSettings = new();
        _viewPluNesting = ContextManager.ContextPluNesting.GetNewView();
    }

    #endregion

    #region Public and private methods

    public int GetPlusPageCount() =>
            ContextCache.LocalViewPlusLines.Where(item => item.IsActive).ToList().Count / PlusPageSize;

    public void RotateProductDate(WsEnumDirection direction)
    {
        switch (direction)
        {
            case WsEnumDirection.Left:
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue) ProductDate = ProductDateMinValue;
                break;
            }
            case WsEnumDirection.Right:
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue) ProductDate = ProductDateMaxValue;
                break;
            }
        }
    }

    public void NewPallet()
    {
        PluginPrintMain.LabelPrintedCount = 1;
        ProductSeries.Load();
    }

    /// <summary>
    /// Настроить сессию.
    /// </summary>
    /// <param name="scaleId"></param>
    /// <param name="area"></param>
    public void SetSessionForLabelPrint(long scaleId = -1, WsSqlProductionFacilityModel? area = null)
    {
        lock (_locker)
        {
            SetSqlPublish();
            // Device.
            WsSqlDeviceModel device = ContextManager.ContextItem.GetItemDeviceNotNullable(DeviceName);
            device = WsWinFormNavigationUtils.SetNewDeviceWithQuestion(device, MdNetUtils.GetLocalIpAddress(), MdNetUtils.GetLocalMacAddress());
            // DeviceTypeFk.
            WsSqlDeviceTypeFkModel deviceTypeFk = ContextManager.ContextItem.GetItemDeviceTypeFkNotNullable(device);
            if (deviceTypeFk.IsNew)
            {
                // DeviceType.
                WsSqlDeviceTypeModel deviceType = ContextManager.ContextItem.GetItemDeviceTypeNotNullable("Monoblock");
                // DeviceTypeFk.
                deviceTypeFk.Device = device;
                deviceTypeFk.Type = deviceType;
                ContextManager.AccessItem.Save(deviceTypeFk);
            }
            // DeviceTypeFk.
            DeviceScaleFk = ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(deviceTypeFk.Device);
            // Scale.
            Scale = scaleId <= 0 ? DeviceScaleFk.Scale : ContextManager.ContextItem.GetScaleNotNullable(scaleId);
            // Area.
            //ProductionFacility = ContextManager.ContextItem.GetProductionFacilityNotNullable(productionFacilityName);
            ProductionFacility = area ?? ContextManager.ContextItem.GetProductionFacilityNotNullable(string.Empty);
            // Other.
            ProductDate = DateTime.Now;
            // Новая серия, упаковка продукции, новая паллета.
            ProductSeries = new(Scale);
            WeighingSettings = new();
        }
    }

    /// <summary>
    /// Задать настройки публикации.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void SetSqlPublish() =>
        PublishDescription = Debug.Config switch
        {
            WsEnumConfiguration.DevelopAleksandrov => LocaleCore.Sql.SqlServerDevelopAleksandrov,
            WsEnumConfiguration.DevelopMorozov => LocaleCore.Sql.SqlServerDevelopMorozov,
            WsEnumConfiguration.DevelopVS => LocaleCore.Sql.SqlServerVS,
            WsEnumConfiguration.ReleaseAleksandrov => LocaleCore.Sql.SqlServerReleaseAleksandrov,
            WsEnumConfiguration.ReleaseMorozov => LocaleCore.Sql.SqlServerReleaseMorozov,
            WsEnumConfiguration.ReleaseVS => LocaleCore.Sql.SqlServerReleaseVS,
            _ => throw new ArgumentOutOfRangeException()
        };

    #endregion
}