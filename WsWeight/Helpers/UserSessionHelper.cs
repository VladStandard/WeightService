// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Models;
using DataCore.Sql.Core.Utils;
using DataCore.Sql.Fields;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Utils;
using MvvmHelpers;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using WsWeight.Plugins.Helpers;
using SqlQueries = DataCore.Sql.Core.Utils.SqlQueries;

namespace WsWeight.Helpers;

public class UserSessionHelper : BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static UserSessionHelper _instance;
#pragma warning restore CS8618
    public static UserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    public DataAccessHelper DataAccess => DataAccessHelper.Instance;
    public DataContextModel DataContext { get; } = new();
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
        Scale.PrinterMain is not null && Scale.PrinterMain.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
    public PrintBrand PrintBrandShipping =>
        Scale.PrinterShipping is not null && Scale.PrinterShipping.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
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
                DataAccess.LogInformation($"{LocaleCore.Scales.PluSet(value.Plu.IdentityValueId, value.Plu.Number, value.Plu.Name)}");
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
    public List<PluStorageMethodFkModel> PluStorageMethodFks { get; private set; }

    private PluNestingFkModel _pluNestingFk;

    [XmlElement]
    public PluNestingFkModel PluNestingFk
    {
        get => _pluNestingFk;
        set
        {
            _pluNestingFk = value;// ?? DataAccess.GetItemNewEmpty<PluNestingFkModel>();
            OnPropertyChanged();
        }
    }

    private List<PluNestingFkModel> _pluNestingFks;

    [XmlElement]
    public List<PluNestingFkModel> PluNestingFks
    {
        get => _pluNestingFks;
        private set
        {
            _pluNestingFks = value;
            if (PluNestingFks.Exists(x => !x.IsNew) && value.Exists(x => x.IsDefault))
                PluNestingFk = value.Find(x => x.IsDefault);
            else
                PluNestingFk = value.First();
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
            _scale = value ?? DataAccess.GetItemNewEmpty<ScaleModel>();
            _ = ProductionFacility;
            PluScale = DataAccess.GetItemNewEmpty<PluScaleModel>();
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
            _productionFacility = value ?? DataAccess.GetItemNewEmpty<ProductionFacilityModel>();
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
    public string DeviceName => NetUtils.GetLocalDeviceName(false);

    /// <summary>
    /// Constructor.
    /// </summary>
    public UserSessionHelper()
    {
        // Items.
        _pluNestingFk = DataAccess.GetItemNewEmpty<PluNestingFkModel>();
        _pluScale = DataAccess.GetItemNewEmpty<PluScaleModel>();
        _pluWeighing = DataAccess.GetItemNewEmpty<PluWeighingModel>();
        _deviceScaleFk = DataAccess.GetItemNewEmpty<DeviceScaleFkModel>();
        _productionFacility = DataAccess.GetItemNewEmpty<ProductionFacilityModel>();
        _scale = DataAccess.GetItemNewEmpty<ScaleModel>();
        PluStorageMethodFk = DataAccess.GetItemNewEmpty<PluStorageMethodFkModel>();
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

        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfigSection(false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(ScaleModel.Description), Direction = SqlOrderDirection.Asc });
        Scales = DataContext.GetListNotNullable<ScaleModel>(sqlCrudConfig);

        sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfigSection(false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(ProductionFacilityModel.Name), Direction =  SqlOrderDirection.Asc });
        ProductionFacilities = DataContext.GetListNotNullable<ProductionFacilityModel>(SqlCrudConfigUtils.GetCrudConfigSection(false));
    }

    private void SetScale(long scaleId, string productionFacilityName)
    {
        lock (_locker)
        {
            // Device.
            DeviceModel device = WpfUtils.SetNewDeviceWithQuestion(
                DeviceName, NetUtils.GetLocalIpAddress(), NetUtils.GetLocalMacAddress());
            // DeviceTypeFk.
            DeviceTypeFkModel deviceTypeFk = DataAccess.GetItemDeviceTypeFkNotNullable(device);
            if (deviceTypeFk.IsNew)
            {
                // DeviceType.
                DeviceTypeModel deviceType = DataAccess.GetItemDeviceTypeNotNullable("Monoblock");
                // DeviceTypeFk.
                deviceTypeFk.Device = device;
                deviceTypeFk.Type = deviceType;
                DataAccess.Save(deviceTypeFk);
            }
            // DeviceTypeFk.
            DeviceScaleFk = DataAccess.GetItemDeviceScaleFkNotNullable(deviceTypeFk.Device);
            // Scale.
            Scale = scaleId <= 0 ? DeviceScaleFk.Scale : DataAccess.GetScaleNotNullable(scaleId);
            // Area.
            ProductionFacility = DataAccess.GetProductionFacilityNotNullable(productionFacilityName);
            // Other.
            ProductDate = DateTime.Now;
            // Новыя серия, упаковка продукции, новая паллета.
            ProductSeries = new(Scale);
            WeighingSettings = new();
        }
    }

    public void SetBundleFk(Guid? uid)
    {
        if (uid is null)
            // Manual set by another place.
            PluNestingFk = DataAccess.GetItemNewEmpty<PluNestingFkModel>();
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
    /// Check PLU BundleFk is empty.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckPluBundleFkIsEmpty(IWin32Window owner)
    {
        //if (PluScale.Plu.IsCheckWeight && PluPackages.Count > 0 && PluPackage.IsNew)
        if (PluNestingFk.IsNew && PluNestingFks.Count > 1)
        {
            WpfUtils.ShowNewOperationControl(owner,
                LocaleCore.Scales.PluPackageNotSelect, true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check PLU is empty.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckPluIsEmpty(IWin32Window owner)
    {
        if (PluScale.IsNew)
        {
            WpfUtils.ShowNewOperationControl(owner,
                LocaleCore.Scales.PluNotSelect, true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check Massa-K device exists.
    /// </summary>
    /// <returns></returns>
    public bool CheckWeightMassaDeviceExists() => Debug.IsDebug || PluScale is { IsNew: false, Plu.IsCheckWeight: false } || true;

    /// <summary>
	/// Check Massa-K is stable.
	/// </summary>
	/// <param name="owner"></param>
	/// <returns></returns>
	public bool CheckWeightMassaIsStable(IWin32Window owner)
    {
        if (Debug.IsDebug) return true;

        if (PluScale.Plu.IsCheckWeight && !PluginMassa.IsStable)
        {
            WpfUtils.ShowNewOperationControl(owner,
                LocaleCore.Scales.MassaIsNotCalc + Environment.NewLine + LocaleCore.Scales.MassaWaitStable,
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check PLU GTIN.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckPluGtin(IWin32Window owner)
    {
        if (string.IsNullOrEmpty(PluScale.Plu.Gtin))
        {
            WpfUtils.ShowNewOperationControl(owner,
                LocaleCore.Scales.PluGtinIsNotSet,
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check printer connection.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnect(IWin32Window owner, PluginPrintModel managerPrint, bool isMain)
    {
        if (!managerPrint.Printer.IsPing)
        {
            WpfUtils.ShowNewOperationControl(owner, isMain
                ? LocaleCore.Print.DeviceMainIsUnavailable + Environment.NewLine + LocaleCore.Print.DeviceCheckConnect
                : LocaleCore.Print.DeviceShippingIsUnavailable + Environment.NewLine + LocaleCore.Print.DeviceCheckConnect,
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check printer status on ready.
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="managerPrint"></param>
    /// <param name="isMain"></param>
    /// <returns></returns>
    public bool CheckPrintStatusReady(IWin32Window owner, PluginPrintModel managerPrint, bool isMain)
    {
        if (!managerPrint.CheckDeviceStatus())
        {
            WpfUtils.ShowNewOperationControl(owner, isMain
                ? LocaleCore.Print.DeviceMainCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus()
                : LocaleCore.Print.DeviceShippingCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus(),
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check weight is negative.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightIsNegative(IWin32Window owner)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!PluScale.Plu.IsCheckWeight) return true;

        if (PluginMassa.WeightNet <= 0)
        {
            WpfUtils.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightIsZero, true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return false;
        }
        else
        {
            decimal weight = PluginMassa.WeightNet - (PluScale.IsNew ? 0 : PluNestingFk.WeightTare);
            if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
            {
                WpfUtils.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight), true, LogType.Warning,
                    new() { ButtonCancelVisibility = Visibility.Visible });
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Check weight is positive.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightIsPositive(IWin32Window owner)
    {
        if (!PluScale.Plu.IsCheckWeight) return true;

        decimal weight = PluginMassa.WeightNet - (PluScale.IsNew ? 0 : PluNestingFk.WeightTare);
        if (weight > LocaleCore.Scales.MassaThresholdValue)
        {
            DialogResult result = WpfUtils.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return result == DialogResult.Cancel;
        }
        return true;
    }

    /// <summary>
    /// Check weight thresholds.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightThresholds(IWin32Window owner)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!PluScale.Plu.IsCheckWeight) return true;

        if (PluNestingFk.WeightNom > 0 && PluNestingFk.WeightMin is not 0 && PluNestingFk.WeightMax is not 0)
        {
            if (!(PluWeighing.NettoWeight >= PluNestingFk.WeightMin && PluWeighing.NettoWeight <= PluNestingFk.WeightMax))
            {
                if (PluWeighing.IsNotNew)
                    WpfUtils.ShowNewOperationControl(owner,
                        LocaleCore.Scales.CheckWeightThresholds(PluWeighing.NettoWeight, PluScale.IsNew ? 0 : PluNestingFk.WeightMax,
                        PluScale.IsNew ? 0 : PluNestingFk.WeightNom,
                        PluScale.IsNew ? 0 : PluNestingFk.WeightMin),
                        true, LogType.Warning,
                        new() { ButtonCancelVisibility = Visibility.Visible });
                return false;
            }
        }
        return true;
    }

    public void PrintLabel(IWin32Window owner, bool isClearBuffer)
    {
        if (Scale is { IsOrder: true })
            throw new("Order under construct!");

        // #WS-T-710 Печать этикеток. Исправление счётчика этикеток
        //PluScale = DataAccess.GetItemNotNullable<PluScaleModel>(PluScale.IdentityValueUid);
        PluScale.Scale = Scale;
        TemplateModel template = DataAccess.GetItemTemplateNotNullable(PluScale);
        // Template isn't exist.
        if (template.IsNew)
        {
            WpfUtils.ShowNewOperationControl(owner,
                LocaleCore.Scales.PluTemplateNotSet,
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible });
            return;
        }

        // Template is exists!
        switch (PluScale.Plu.IsCheckWeight)
        {
            case true:
                PrintLabelCore(template, isClearBuffer);
                break;
            default:
                PrintLabelCount(template, isClearBuffer);
                break;
        }

        PluWeighing = new();
    }

    public void AddScaleCounter()
    {
        Scale.Counter++;
        DataAccess.UpdateForce(Scale);
    }

    /// <summary>
    /// Count label printing.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCount(TemplateModel template, bool isClearBuffer)
    {
        //// Указан номинальный вес.
        //bool isCheck = false;
        //if (CurrentPlu.NominalWeight > 0)
        //{
        //    if (Manager.Massa is not null)
        //        CurrentWeighingFact.NettoWeight = Manager.Massa.WeightNet - CurrentPlu.GoodsWeightTare;
        //    else
        //        CurrentWeighingFact.NettoWeight -= CurrentPlu.GoodsWeightTare;
        //    if (CurrentWeighingFact.NettoWeight >= CurrentPlu.LowerWeightThreshold &&
        //        CurrentWeighingFact.NettoWeight <= CurrentPlu.UpperWeightThreshold)
        //    {
        //        isCheck = true;
        //    }
        //}
        //else
        //    isCheck = true;
        //if (!isCheck)
        //{
        //    // WPF MessageBox.
        //    using WpfPageLoader wpfPageLoader = new(Page.MessageBox, false) { Width = 700, Height = 400 };
        //    wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.OperationControl;
        //    wpfPageLoader.MessageBox.Message =
        //        LocaleCore.Scales.WeightingControl + Environment.NewLine +
        //        $"Вес нетто: {CurrentWeighingFact.NettoWeight} кг" + Environment.NewLine +
        //        $"Номинальный вес: {CurrentPlu.NominalWeight} кг" + Environment.NewLine +
        //        $"Верхнее значение веса: {CurrentPlu.UpperWeightThreshold} кг" + Environment.NewLine +
        //        $"Нижнее значение веса: {CurrentPlu.LowerWeightThreshold} кг" + Environment.NewLine + Environment.NewLine +
        //        "Для продолжения печати нажмите Ignore.";
        //    wpfPageLoader.MessageBox.ButtonAbortVisibility = Visibility.Visible;
        //    wpfPageLoader.MessageBox.ButtonRetryVisibility = Visibility.Visible;
        //    wpfPageLoader.MessageBox.ButtonIgnoreVisibility = Visibility.Visible;
        //    wpfPageLoader.MessageBox.VisibilitySettings.Localization();
        //    wpfPageLoader.ShowDialog(owner);
        //    DialogResult result = wpfPageLoader.MessageBox.Result;
        //    wpfPageLoader.Close();
        //    wpfPageLoader.Dispose();
        //    if (result != DialogResult.Ignore)
        //        return;
        //}

        // Шаблон с указанием кол-ва и не весовой продукт.
        if (template.Data.Contains("^PQ1") && !PluScale.Plu.IsCheckWeight)
        {
            // Изменить кол-во этикеток.
            if (WeighingSettings.LabelsCountMain > 1)
                template.Data = template.Data.Replace("^PQ1", $"^PQ{WeighingSettings.LabelsCountMain}");
            // Печать этикетки.
            PrintLabelCore(template, isClearBuffer);
        }
        // Шаблон без указания кол-ва.
        else
        {
            for (int i = PluginPrintMain.LabelPrintedCount; i <= WeighingSettings.LabelsCountMain; i++)
            {
                // Печать этикетки.
                PrintLabelCore(template, isClearBuffer);
            }
        }
    }

    public void NewPluWeighing()
    {
        ProductSeriesModel productSeries = DataAccess.GetItemProductSeriesNotNullable(PluScale.Scale);

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
    /// Set fake data for PLU weighing.
    /// </summary>
    /// <param name="owner"></param>
    public void SetPluWeighingFake(IWin32Window owner)
    {
        if (!Debug.IsDebug) return;
        if (!PluScale.Plu.IsCheckWeight) return;
        if (PluginMassa.WeightNet > 0) return;

        DialogResult dialogResult = WpfUtils.ShowNewOperationControl(owner,
            LocaleCore.Print.QuestionUseFakeData,
            true, LogType.Question,
            new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
        if (dialogResult is DialogResult.Yes)
        {
            PluginMassa.WeightNet = StringUtils.NextDecimal(PluNestingFk.WeightMin, PluNestingFk.WeightMax);
            PluginMassa.IsWeightNetFake = true;
        }
    }

    /// <summary>
    /// Weight label printing.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCore(TemplateModel template, bool isClearBuffer)
    {
        try
        {
            PluLabelModel pluLabel = CreateAndSavePluLabel(template);
            CreateAndSaveBarCodes(pluLabel);

            // Print.
            if (isClearBuffer)
            {
                PluginPrintMain.ClearPrintBuffer();
                if (Scale.IsShipping)
                    PluginPrintShipping.ClearPrintBuffer();
            }

            // Send cmd to the print.
            if (Debug.IsDebug)
            {
                DialogResult dialogResult = WpfUtils.ShowNewOperationControl(
                    LocaleCore.Print.QuestionPrintSendCmd, true, LogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (dialogResult != DialogResult.Yes)
                    return;
            }

            // Send cmd to the print.
            PluginPrintMain.SendCmd(pluLabel);
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
            DataAccess.Save(PluWeighing);
        else
            DataAccess.UpdateForce(PluWeighing);
    }

    /// <summary>
    /// Create and save PLU label.
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    private PluLabelModel CreateAndSavePluLabel(TemplateModel template)
    {
        PluLabelModel pluLabel = new() { PluWeighing = PluWeighing, PluScale = PluScale, ProductDt = ProductDate };

        pluLabel.Xml = DataFormatUtils.SerializeAsXmlDocument<PluLabelModel>(pluLabel, true, true);
        
        XmlDocument xmlArea = DataFormatUtils.SerializeAsXmlDocument<ProductionFacilityModel>(ProductionFacility, true, true);
        pluLabel.Xml = DataFormatUtils.XmlMerge(pluLabel.Xml, xmlArea);

        PluLabelContextModel pluLabelContext = new(DataContext, pluLabel, PluNestingFk, pluLabel.PluScale, ProductionFacility, PluWeighing);
        XmlDocument xmlLabelContext = DataFormatUtils.SerializeAsXmlDocument<PluLabelContextModel>(pluLabelContext, true, true);
        pluLabel.Xml = DataFormatUtils.XmlMerge(pluLabel.Xml, xmlLabelContext);

        pluLabel.Zpl = DataFormatUtils.XsltTransformation(template.Data, pluLabel.Xml.OuterXml);
        pluLabel.Zpl = DataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        _ = DataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Save.
        DataAccess.Save(pluLabel);

        return pluLabel;
    }

    private Action<string> ActionReplaceStorageMethod(PluLabelModel pluLabel) =>
        zpl =>
        {
            // Patch for using table `PLUS_STORAGE_METHODS_FK`.
            if (PluStorageMethodFks.Any() && zpl.Contains("[@PLUS_STORAGE_METHODS_FK]"))
            {
                TemplateResourceModel resource = DataContext.GetPluStorageResource(pluLabel.PluScale.Plu);
                string resourceHex = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode);
                zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
            }
            pluLabel.Zpl = zpl;
        };

    private void CreateAndSaveBarCodes(PluLabelModel pluLabel)
    {
        BarCodeModel barCode = new() { PluLabel = pluLabel };
        barCode.SetBarCodeTop(pluLabel);
        barCode.SetBarCodeRight(pluLabel);
        barCode.SetBarCodeBottom(pluLabel);
        DataAccess.Save(barCode);
    }

    private void SetSqlPublish() =>
        PublishDescription = Debug.Config switch
        {
            Configuration.DevelopAleksandrov => LocaleCore.Sql.SqlServerDevelopAleksandrov,
            Configuration.DevelopMorozov => LocaleCore.Sql.SqlServerDevelopMorozov,
            Configuration.DevelopVS => LocaleCore.Sql.SqlServerVS,
            Configuration.ReleaseAleksandrov => LocaleCore.Sql.SqlServerReleaseAleksandrov,
            Configuration.ReleaseMorozov => LocaleCore.Sql.SqlServerReleaseMorozov,
            Configuration.ReleaseVS => LocaleCore.Sql.SqlServerReleaseVS,
            _ => throw new ArgumentOutOfRangeException()
        };

    public void SetPluScales()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(Scale, nameof(PluScaleModel.Scale), 
            false, false, false, false);
        sqlCrudConfig.AddFilters(new SqlFieldFilterModel { Name = nameof(PluScaleModel.IsActive), Value = true });
        sqlCrudConfig.AddOrders(new() { Name = nameof(PluScaleModel.Plu), Direction = SqlOrderDirection.Asc });
        sqlCrudConfig.IsResultOrder = true;
        PluScales = DataContext.GetListNotNullable<PluScaleModel>(sqlCrudConfig);
    }

    public void SetPluStorageMethodsFks()
    {
        SqlCrudConfigModel sqlCrudConfig = new(true, false, false, false);// { IsFillReferences = false };
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
        PluNestingFkModel pluNestingFk = DataAccess.GetItemNewEmpty<PluNestingFkModel>();
        pluNestingFk.PluBundle = DataAccess.GetItemNewEmpty<PluBundleFkModel>();
        pluNestingFk.PluBundle.Plu = DataAccess.GetItemNewEmpty<PluModel>();
        pluNestingFk.PluBundle.Bundle = DataAccess.GetItemNewEmpty<BundleModel>();
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
                SqlQueries.DbScales.Tables.PluNestingFks.GetList(true), new("P_UID", plu.IdentityValueUid), true);
            PluNestingFks = DataContext.GetListNotNullable<PluNestingFkModel>(sqlCrudConfig);
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