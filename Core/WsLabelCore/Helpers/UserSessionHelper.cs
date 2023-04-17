// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.BarcodePrintUtils.Utils;
using MvvmHelpers;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using MDSoft.WinFormsUtils;
using WsDataCore.Utils;
using WsPrintCore.Zpl;
using WsStorageCore.Enums;
using WsStorageCore.TableDirectModels;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleFkModels.PlusStorageMethodsFks;
using WsStorageCore.TableScaleFkModels.PlusWeighingsFks;
using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.ProductSeries;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.Utils;

namespace WsLabelCore.Helpers;

#nullable enable
/// <summary>
/// User session.
/// </summary>
public sealed class UserSessionHelper : BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static UserSessionHelper _instance;
#pragma warning restore CS8618
    public static UserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private WsBarCodeHelper BarCode => WsBarCodeHelper.Instance;
    public WsDataContextModel DataContext { get; } = new();
    public DebugHelper Debug => DebugHelper.Instance;
    public PluginLabelsHelper PluginLabels => PluginLabelsHelper.Instance;
    public PluginMassaHelper PluginMassa => PluginMassaHelper.Instance;
    public PluginMemoryHelper PluginMemory => PluginMemoryHelper.Instance;
    public PluginPrintModel PluginPrintMain { get; } = new();
    public PluginPrintModel PluginPrintShipping { get; } = new();

    private ProductSeriesDirect _productSeries;
    [XmlElement]
    public ProductSeriesDirect ProductSeries
    {
        get => _productSeries;
        set
        {
            _productSeries = value;
            OnPropertyChanged();
        }
    }
    public PrintBrand PrintBrandMain =>
        Scale.PrinterMain is not null && Scale.PrinterMain.PrinterType.Name.Contains("TSC ") ? PrintBrand.Tsc : PrintBrand.Zebra;
    public PrintBrand PrintBrandShipping =>
        Scale.PrinterShipping is not null && Scale.PrinterShipping.PrinterType.Name.Contains("TSC ") ? PrintBrand.Tsc : PrintBrand.Zebra;
    private PluWeighingModel _pluWeighing;
    [XmlElement]
    public PluWeighingModel PluWeighing
    {
        get => _pluWeighing;
        private set
        {
            _pluWeighing = value;
            OnPropertyChanged();
        }
    }
    private WeighingSettingsModel _weighingSettings;
    [XmlElement]
    public WeighingSettingsModel WeighingSettings
    {
        get => _weighingSettings;
        set
        {
            _weighingSettings = value;
            OnPropertyChanged();
        }
    }
    public Stopwatch StopwatchMain { get; set; } = new();
    private PluScaleModel _pluScale;
    [XmlElement]
    public PluScaleModel PluScale
    {
        get => _pluScale;
        set
        {
            _pluScale = value;
            if (value.IsNotNew)
                DataContext.DataAccess.SaveLogInformation($"{LocaleCore.Scales.PluSet(value.Plu.IdentityValueId, value.Plu.Number, value.Plu.Name)}");
            PluginPrintMain.LabelPrintedCount = 1;
            PluginPrintShipping.LabelPrintedCount = 1;
            SetPluNestingFks(value.Plu);
            SetPluStorageMethodFks(value.Plu);
            OnPropertyChanged();
        }
    }

    public readonly ushort PageColumnCount = 4;
    public readonly ushort PageSize = 20;
    public readonly ushort PageRowCount = 5;
    public int PageNumber { get; set; }
    public List<PluScaleModel> PluScales { get; private set; }
    private List<PluStorageMethodFkModel> PluStorageMethodFks { get; set; }

    private PluNestingFkModel _pluNestingFk;
    public PluNestingFkModel PluNestingFk { get => _pluNestingFk; set { _pluNestingFk = value; OnPropertyChanged(); } }

    private List<PluNestingFkModel> _pluNestingFks;
    public List<PluNestingFkModel> PluNestingFks
    {
        get => _pluNestingFks;
        private set
        {
            _pluNestingFks = value;
            PluNestingFk = !_pluNestingFks.Any()
                ? DataContext.DataAccess.GetItemNewEmpty<PluNestingFkModel>()
                : _pluNestingFks.Exists(x => !x.IsNew && x.IsDefault)
                    ? value.Find(x => !x.IsNew && x.IsDefault)
                    : value.First();
            OnPropertyChanged();
        }
    }

    [XmlElement] public PluStorageMethodFkModel PluStorageMethodFk { get; set; }

    private DeviceScaleFkModel _deviceScaleFk;
    [XmlElement]
    public DeviceScaleFkModel DeviceScaleFk
    {
        get => _deviceScaleFk;
        private set
        {
            _deviceScaleFk = value;
            OnPropertyChanged();
        }
    }
    private ScaleModel _scale;
    [XmlElement]
    public ScaleModel Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            _ = ProductionFacility;
            PluScale = DataContext.DataAccess.GetItemNewEmpty<PluScaleModel>();
            OnPropertyChanged();
        }
    }
    private List<ScaleModel> _scales;
    [XmlElement]
    public List<ScaleModel> Scales
    {
        get => _scales;
        set
        {
            _scales = value;
            OnPropertyChanged();
        }
    }

    private ProductionFacilityModel _productionFacility;
    [XmlElement]
    public ProductionFacilityModel ProductionFacility
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
    private List<ProductionFacilityModel> _productionFacilities;
    [XmlElement]
    public List<ProductionFacilityModel> ProductionFacilities
    {
        get => _productionFacilities;
        set
        {
            _productionFacilities = value;
            OnPropertyChanged();
        }
    }
    private string _publishDescription;
    [XmlElement]
    public string PublishDescription
    {
        get => _publishDescription;
        set
        {
            _publishDescription = value;
            OnPropertyChanged();
        }
    }

    private DateTime ProductDateMaxValue => DateTime.Now.AddDays(+31);
    private DateTime ProductDateMinValue => DateTime.Now.AddDays(-31);
    private DateTime _productDate;
    public DateTime ProductDate
    {
        get => _productDate;
        set
        {
            _productDate = value;
            OnPropertyChanged();
        }
    }
    private readonly object _locker = new();
    public string DeviceName => MdNetUtils.GetLocalDeviceName(false);

    /// <summary>
    /// Constructor.
    /// </summary>
    public UserSessionHelper()
    {
        // Items.
        _pluNestingFk = DataContext.DataAccess.GetItemNewEmpty<PluNestingFkModel>();
        _pluScale = DataContext.DataAccess.GetItemNewEmpty<PluScaleModel>();
        _pluWeighing = DataContext.DataAccess.GetItemNewEmpty<PluWeighingModel>();
        _deviceScaleFk = DataContext.DataAccess.GetItemNewEmpty<DeviceScaleFkModel>();
        _productionFacility = DataContext.DataAccess.GetItemNewEmpty<ProductionFacilityModel>();
        _scale = DataContext.DataAccess.GetItemNewEmpty<ScaleModel>();
        PluStorageMethodFk = DataContext.DataAccess.GetItemNewEmpty<PluStorageMethodFkModel>();
        // Lists.
        _pluNestingFks = new();
        _productionFacilities = new();
        _productSeries = new();
        _scales = new();
        PluScales = new();
        PluStorageMethodFks = new();
        // Strings
        _publishDescription = string.Empty;
        // Others.
        _weighingSettings = new();
    }

    #endregion

    #region Public and private methods

    public void PluginsClose()
    {
        PluginMemory.Close();
        PluginMassa.Close();
        PluginPrintMain.Close();
        PluginPrintShipping.Close();
        PluginLabels.Close();
    }

    public void SetMain(long scaleId = -1, string productionFacilityName = "")
    {
        SetSqlPublish();
        SetScale(scaleId, productionFacilityName);

        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(ScaleModel.Description), Direction = WsSqlOrderDirection.Asc });
        Scales = DataContext.GetListNotNullableScales(sqlCrudConfig);

        sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(ProductionFacilityModel.Name), Direction = WsSqlOrderDirection.Asc });
        ProductionFacilities = DataContext.GetListNotNullableProductionFacilities(WsSqlCrudConfigUtils.GetCrudConfigSection(false));
    }

    private void SetScale(long scaleId, string productionFacilityName)
    {
        lock (_locker)
        {
            // Device.
            DeviceModel device = DataContext.GetItemDeviceNotNullable(DeviceName);
            device = WpfUtils.SetNewDeviceWithQuestion(device, MdNetUtils.GetLocalIpAddress(), MdNetUtils.GetLocalMacAddress());
            // DeviceTypeFk.
            DeviceTypeFkModel deviceTypeFk = DataContext.GetItemDeviceTypeFkNotNullable(device);
            if (deviceTypeFk.IsNew)
            {
                // DeviceType.
                DeviceTypeModel deviceType = DataContext.GetItemDeviceTypeNotNullable("Monoblock");
                // DeviceTypeFk.
                deviceTypeFk.Device = device;
                deviceTypeFk.Type = deviceType;
                DataContext.DataAccess.Save(deviceTypeFk);
            }
            // DeviceTypeFk.
            DeviceScaleFk = DataContext.DataAccess.GetItemDeviceScaleFkNotNullable(deviceTypeFk.Device);
            // Scale.
            Scale = scaleId <= 0 ? DeviceScaleFk.Scale : DataContext.DataAccess.GetScaleNotNullable(scaleId);
            // Area.
            ProductionFacility = DataContext.DataAccess.GetProductionFacilityNotNullable(productionFacilityName);
            // Other.
            ProductDate = DateTime.Now;
            // Новая серия, упаковка продукции, новая паллета.
            ProductSeries = new(Scale);
            WeighingSettings = new();
        }
    }

    public void SetBundleFk(Guid? uid)
    {
        if (uid is null)
            // Manual set by another place.
            PluNestingFk = DataContext.DataAccess.GetItemNewEmpty<PluNestingFkModel>();
        else
            // PluBundlesFks set default BundleFk.
            _ = PluNestingFks;
    }

    public void NewPallet()
    {
        PluginPrintMain.LabelPrintedCount = 1;
        ProductSeries.Load();
    }

    public void RotateProductDate(DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.Left:
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue)
                    ProductDate = ProductDateMinValue;
                break;
            }
            case DirectionEnum.Right:
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue)
                    ProductDate = ProductDateMaxValue;
                break;
            }
        }
    }

    /// <summary>
    /// Проверить наличие вложенности ПЛУ.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPluNestingFkIsEmpty(Label fieldWarning)
    {
        if (PluNestingFk.IsNew && PluNestingFks.Count > 1)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluPackageNotSelect);
            DataContext.DataAccess.SaveLogError(LocaleCore.Scales.PluPackageNotSelect);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPluIsEmpty(Label fieldWarning)
    {
        if (PluScale.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluNotSelect);
            DataContext.DataAccess.SaveLogError(LocaleCore.Scales.PluNotSelect);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить наличие весовой платформы Масса-К.
    /// </summary>
    /// <returns></returns>
    public bool CheckWeightMassaDeviceExists() => PluScale is { IsNew: false, Plu.IsCheckWeight: false } || true;

    /// <summary>
    /// Проверить стабилизацию весовой платформы Масса-К.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightMassaIsStable(Label fieldWarning)
    {
        if (PluScale.Plu.IsCheckWeight && !PluginMassa.IsStable)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, $"{LocaleCore.Scales.MassaIsNotCalc} {LocaleCore.Scales.MassaWaitStable}");
            DataContext.DataAccess.SaveLogError($"{LocaleCore.Scales.MassaIsNotCalc} {LocaleCore.Scales.MassaWaitStable}");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить ГТИН ПЛУ.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPluGtin(Label fieldWarning)
    {
        if (string.IsNullOrEmpty(PluScale.Plu.Gtin))
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluGtinIsNotSet);
            DataContext.DataAccess.SaveLogError(LocaleCore.Scales.PluGtinIsNotSet);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить подключение и готовность принтера.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <param name="managerPrint"></param>
    /// <param name="isMain"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnectAndReady(Label fieldWarning, PluginPrintModel managerPrint, bool isMain)
    {
        // Подключение.
        if (managerPrint.Printer.PingStatus != IPStatus.Success)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = isMain
                ? $"{LocaleCore.Print.DeviceMainIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}"
                : $"{LocaleCore.Print.DeviceShippingIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            DataContext.DataAccess.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!managerPrint.CheckDeviceStatus())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = isMain
                ? $"{LocaleCore.Print.DeviceMainCheckStatus} {managerPrint.GetDeviceStatus()}"
                : $"{LocaleCore.Print.DeviceShippingCheckStatus} {managerPrint.GetDeviceStatus()}";
            MdInvokeControl.SetText(fieldWarning, message);
            DataContext.DataAccess.SaveLogError(message);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить отрицательный вес.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightIsNegative(Label fieldWarning)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!PluScale.Plu.IsCheckWeight) return true;

        if (PluginMassa.WeightNet <= 0)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightIsZero);
            DataContext.DataAccess.SaveLogError(LocaleCore.Scales.CheckWeightIsZero);
            return false;
        }

        decimal weight = PluginMassa.WeightNet - (PluScale.IsNew ? 0 : PluNestingFk.WeightTare);
        if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightThreshold(weight));
            DataContext.DataAccess.SaveLogError(LocaleCore.Scales.CheckWeightThreshold(weight));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить положительный вес.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightIsPositive(Label fieldWarning)
    {
        if (!PluScale.Plu.IsCheckWeight) return true;

        decimal weight = PluginMassa.WeightNet - (PluScale.IsNew ? 0 : PluNestingFk.WeightTare);
        if (weight > LocaleCore.Scales.MassaThresholdValue)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightThreshold(weight));
            DataContext.DataAccess.SaveLogError(LocaleCore.Scales.CheckWeightThreshold(weight));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить границы веса.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightThresholds(Label fieldWarning)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!PluScale.Plu.IsCheckWeight) return true;

        if (PluNestingFk is { WeightNom: > 0, WeightMin: not 0, WeightMax: not 0 })
        {
            if (!(PluWeighing.NettoWeight >= PluNestingFk.WeightMin && PluWeighing.NettoWeight <= PluNestingFk.WeightMax))
            {
                if (PluWeighing.IsNotNew)
                {
                    MdInvokeControl.SetVisible(fieldWarning, true);
                    string message = LocaleCore.Scales.CheckWeightThresholds(PluWeighing.NettoWeight,
                        PluScale.IsNew ? 0 : PluNestingFk.WeightMax,
                        PluScale.IsNew ? 0 : PluNestingFk.WeightNom,
                        PluScale.IsNew ? 0 : PluNestingFk.WeightMin);
                    MdInvokeControl.SetText(fieldWarning, message);
                    DataContext.DataAccess.SaveLogError(message);
                }
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Печать этикетки.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <param name="isClearBuffer"></param>
    public void PrintLabel(Label fieldWarning, bool isClearBuffer)
    {
        if (Scale is { IsOrder: true })
            throw new("Order under construct!");

        // #WS-T-710 Печать этикеток. Исправление счётчика этикеток
        PluScale.Scale = Scale;
        // Получить шаблон этикетки.
        TemplateModel template = DataContext.DataAccess.GetItemTemplateNotNullable(PluScale);
        // Проверить наличие шаблона этикетки.
        if (template.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluTemplateNotSet);
            DataContext.DataAccess.SaveLogError(LocaleCore.Scales.PluTemplateNotSet);
            return;
        }
        // Выбор типа ПЛУ.
        switch (PluScale.Plu.IsCheckWeight)
        {
            // Весовая ПЛУ.
            case true:
                PrintLabelCore(template, isClearBuffer);
                break;
            // Штучная ПЛУ.
            default:
                PrintLabelCount(template, isClearBuffer);
                break;
        }

        PluWeighing = new();
    }

    /// <summary>
    /// Инкремент счётчика этикеток.
    /// </summary>
    public void AddScaleCounter()
    {
        Scale.Counter++;
        DataContext.DataAccess.UpdateForce(Scale);
    }

    /// <summary>
    /// Печать этикетки штучной ПЛУ.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCount(TemplateModel template, bool isClearBuffer)
    {
        // Шаблон с указанием кол-ва и не весовой продукт.
        if (template.Data.Contains("^PQ1") && !PluScale.Plu.IsCheckWeight)
        {
            // Изменить кол-во этикеток.
            if (WeighingSettings.LabelsCountMain > 1)
                template.Data = template.Data.Replace("^PQ1", $"^PQ{WeighingSettings.LabelsCountMain}");
            // Печать этикетки ПЛУ.
            PrintLabelCore(template, isClearBuffer);
        }
        // Шаблон без указания кол-ва.
        else
        {
            for (int i = PluginPrintMain.LabelPrintedCount; i <= WeighingSettings.LabelsCountMain; i++)
            {
                // Печать этикетки ПЛУ.
                PrintLabelCore(template, isClearBuffer);
            }
        }
    }

    /// <summary>
    /// Создать новое взвешивание ПЛУ.
    /// </summary>
    public void NewPluWeighing()
    {
        ProductSeriesModel productSeries = DataContext.DataAccess.GetItemProductSeriesNotNullable(PluScale.Scale);

        PluWeighing = new()
        {
            PluScale = PluScale,
            Kneading = WeighingSettings.Kneading,
            NettoWeight = PluScale.Plu.IsCheckWeight ? PluginMassa.WeightNet - PluNestingFk.WeightTare : PluNestingFk.WeightNom,
            WeightTare = PluNestingFk.WeightTare,
            Series = productSeries,
        };

        // Save or update weighing products.
        SaveOrUpdatePluWeighing();
    }

    /// <summary>
    /// Задать фейк данные веса ПЛУ для режима разработки.
    /// </summary>
    /// <param name="owner"></param>
    public void SetPluWeighingFakeForDevelop(IWin32Window owner)
    {
        if (!PluScale.Plu.IsCheckWeight) return;
        if (PluginMassa.WeightNet > 0) return;

        DialogResult dialogResult = WpfUtils.ShowNewOperationControl(owner,
            LocaleCore.Print.QuestionUseFakeData,
            true, LogType.Question,
            new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
        if (dialogResult is DialogResult.Yes)
        {
            PluginMassa.WeightNet = StrUtils.NextDecimal(PluNestingFk.WeightMin, PluNestingFk.WeightMax);
            PluginMassa.IsWeightNetFake = true;
        }
    }

    /// <summary>
    /// Печать этикетки ПЛУ.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCore(TemplateModel template, bool isClearBuffer)
    {
        try
        {
            (PluLabelModel PluLabel, WsPluLabelContextModel PluLabelContext) pluLabelWithContext = CreateAndSavePluLabel(template);
            CreateAndSaveBarCodes(pluLabelWithContext.PluLabel, pluLabelWithContext.PluLabelContext);

            // Print.
            if (isClearBuffer)
            {
                PluginPrintMain.ClearPrintBuffer();
                if (Scale.IsShipping)
                    PluginPrintShipping.ClearPrintBuffer();
            }

            // Send cmd to the print.
            if (Debug.IsDevelop)
            {
                DialogResult dialogResult = WpfUtils.ShowNewOperationControl(
                    LocaleCore.Print.QuestionPrintSendCmd, true, LogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (dialogResult != DialogResult.Yes)
                    return;
            }

            // Send cmd to the print.
            PluginPrintMain.SendCmd(pluLabelWithContext.PluLabel);
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true, true);
        }
    }

    /// <summary>
    /// Save or update weighing products.
    /// </summary>
    private void SaveOrUpdatePluWeighing()
    {
        if (!PluWeighing.PluScale.Plu.IsCheckWeight) return;

        if (PluWeighing.IsNew)
            DataContext.DataAccess.Save(PluWeighing);
        else
            DataContext.DataAccess.UpdateForce(PluWeighing);
    }

    /// <summary>
    /// Create PluLabel from Template.
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    private (PluLabelModel, WsPluLabelContextModel) CreateAndSavePluLabel(TemplateModel template)
    {
        PluLabelModel pluLabel = new() { PluWeighing = PluWeighing, PluScale = PluScale, ProductDt = ProductDate };

        pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<PluLabelModel>(pluLabel, true, true);

        XmlDocument xmlArea = WsDataFormatUtils.SerializeAsXmlDocument<ProductionFacilityModel>(ProductionFacility, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlArea);

        WsPluLabelContextModel pluLabelContext = new(pluLabel, PluNestingFk, pluLabel.PluScale, ProductionFacility, PluWeighing);
        XmlDocument xmlLabelContext = WsDataFormatUtils.SerializeAsXmlDocument<WsPluLabelContextModel>(pluLabelContext, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlLabelContext);

        pluLabel.Zpl = WsDataFormatUtils.XsltTransformation(template.Data, pluLabel.Xml.OuterXml);
        pluLabel.Zpl = WsDataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        _ = DataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Save.
        DataContext.DataAccess.Save(pluLabel);

        return (pluLabel, pluLabelContext);
    }

    /// <summary>
    /// Replace temperature storage method.
    /// </summary>
    /// <param name="pluLabel"></param>
    /// <returns></returns>
    private Action<string> ActionReplaceStorageMethod(PluLabelModel pluLabel) =>
        zpl =>
        {
            // Patch for using table `PLUS_STORAGE_METHODS_FK`.
            if (PluStorageMethodFks.Any() && zpl.Contains("[@PLUS_STORAGE_METHODS_FK]"))
            {
                TemplateResourceModel resource = DataContext.GetPluStorageResource(pluLabel.PluScale.Plu);
                string resourceHex = ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode);
                zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
            }
            pluLabel.Zpl = zpl;
        };

    /// <summary>
    /// Create BarCode from PluLabel.
    /// </summary>
    /// <param name="pluLabel"></param>
    /// <param name="pluLabelContext"></param>
    private void CreateAndSaveBarCodes(PluLabelModel pluLabel, WsPluLabelContextModel pluLabelContext)
    {
        BarCodeModel barCode = new(pluLabel);
        BarCode.SetBarCodeTop(barCode, pluLabelContext);
        BarCode.SetBarCodeRight(barCode, pluLabelContext);
        BarCode.SetBarCodeBottom(barCode, pluLabelContext);
        DataContext.DataAccess.Save(barCode);
    }

    private void SetSqlPublish() =>
        PublishDescription = Debug.Config switch
        {
            WsConfiguration.DevelopAleksandrov => LocaleCore.Sql.SqlServerDevelopAleksandrov,
            WsConfiguration.DevelopMorozov => LocaleCore.Sql.SqlServerDevelopMorozov,
            WsConfiguration.DevelopVS => LocaleCore.Sql.SqlServerVS,
            WsConfiguration.ReleaseAleksandrov => LocaleCore.Sql.SqlServerReleaseAleksandrov,
            WsConfiguration.ReleaseMorozov => LocaleCore.Sql.SqlServerReleaseMorozov,
            WsConfiguration.ReleaseVS => LocaleCore.Sql.SqlServerReleaseVS,
            _ => throw new ArgumentOutOfRangeException()
        };

    public void SetPluScales()
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(Scale, nameof(PluScaleModel.Scale),
            false, false, false, false);
        sqlCrudConfig.AddFilters(new SqlFieldFilterModel { Name = nameof(PluScaleModel.IsActive), Value = true });
        sqlCrudConfig.AddOrders(new() { Name = nameof(PluScaleModel.Plu), Direction = WsSqlOrderDirection.Asc });
        sqlCrudConfig.IsResultOrder = true;
        PluScales = DataContext.GetListNotNullablePlusScales(sqlCrudConfig);
    }

    public void SetPluStorageMethodsFks()
    {
        SqlCrudConfigModel sqlCrudConfig = new(true, false, false, false, false);// { IsFillReferences = false };
        PluStorageMethodFks = DataContext.UpdatePluStorageMethodFks(sqlCrudConfig);
    }

    public List<PluScaleModel> GetCurrentPlus()
    {
        IEnumerable<PluScaleModel> plusSkip = PluScales.Skip(PageNumber * PageSize);
        IEnumerable<PluScaleModel> plusTake = plusSkip.Take(PageSize);
        return plusTake.ToList();
    }

    private void SetNewPluNestingFks()
    {
        PluNestingFkModel pluNestingFk = DataContext.DataAccess.GetItemNewEmpty<PluNestingFkModel>();
        pluNestingFk.PluBundle = DataContext.DataAccess.GetItemNewEmpty<PluBundleFkModel>();
        pluNestingFk.PluBundle.Plu = DataContext.DataAccess.GetItemNewEmpty<PluModel>();
        pluNestingFk.PluBundle.Bundle = DataContext.DataAccess.GetItemNewEmpty<BundleModel>();
        PluNestingFks = new() { pluNestingFk };
    }

    private void SetPluNestingFks(PluModel plu)
    {
        if (plu.IsNew)
        {
            SetNewPluNestingFks();
        }
        else
        {
            SqlCrudConfigModel sqlCrudConfig = new(
                WsSqlQueriesScales.Tables.PluNestingFks.GetList(true), new("P_UID", plu.IdentityValueUid), true);
            PluNestingFks = DataContext.GetListNotNullablePlusNestingFks(sqlCrudConfig);
        }
    }

    private void SetPluStorageMethodFks(PluModel plu)
    {
        if (plu.IsNotExists) return;
        PluStorageMethodFk = DataContext.GetPluStorageMethodFk(plu);
        if (PluStorageMethodFk.IsNotExists)
            PluStorageMethodFk.FillProperties();
    }

    #endregion
}