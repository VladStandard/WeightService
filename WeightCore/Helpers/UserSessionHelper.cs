// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Files;
using DataCore.Helpers;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Settings;
using DataCore.Sql.Core;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NestingFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Utils;
using MDSoft.BarcodePrintUtils;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using WeightCore.Gui;
using WeightCore.Managers;

namespace WeightCore.Helpers;

public class UserSessionHelper : BaseViewModel
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
	private static UserSessionHelper _instance;
#pragma warning restore CS8618
	public static UserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties

	private AppVersionHelper AppVersion => AppVersionHelper.Instance;
	private SqlConnectFactory SqlConnect => SqlConnectFactory.Instance;
	public DataAccessHelper DataAccess => DataAccessHelper.Instance;
    public DataContextModel DataContext { get; } = new();
	public DebugHelper Debug => DebugHelper.Instance;
	private FileLoggerHelper FileLogger => FileLoggerHelper.Instance;
	public ManagerControllerHelper ManagerControl => ManagerControllerHelper.Instance;

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
				DataAccess.LogInformation($"{LocaleCore.Scales.PluSet(value.Plu.IdentityValueId, value.Plu.Number, value.Plu.Name)}",
				DeviceScaleFk.Device.Name, nameof(WeightCore));
			ManagerControl.PrintMain.LabelsCount = 1;
			ManagerControl.PrintShipping.LabelsCount = 1;

			// Fix here.
			//PluNestingFks = DataContext.GetListNotNullable<PluNestingFkModel>(
			//    SqlCrudConfigUtils.GetCrudConfig(value.Plu, nameof(PluScaleModel.Plu), 
			//    new(), false, false, true, true));
			if (value.Plu.IsNew)
			{
				PluNestingFkModel pluNestingFk = DataAccess.GetItemNewEmpty<PluNestingFkModel>();
				pluNestingFk.PluBundle = DataAccess.GetItemNewEmpty<PluBundleFkModel>();
				pluNestingFk.PluBundle.Plu = DataAccess.GetItemNewEmpty<PluModel>();
				pluNestingFk.PluBundle.Bundle = DataAccess.GetItemNewEmpty<BundleModel>();
                PluNestingFks = new() { pluNestingFk };
            }
			else
			{
				DataCore.Sql.Models.SqlCrudConfigModel sqlCrudConfigList = SqlCrudConfigUtils.GetCrudConfigComboBox();
				List<PluNestingFkModel> pluNestingFks = DataContext.GetListNotNullable<PluNestingFkModel>(sqlCrudConfigList);
				PluNestingFkModel pluNestingFk = pluNestingFks.Find(x => x.IsNew);
                List<PluNestingFkModel> temp = new() { pluNestingFk };
				temp.AddRange(pluNestingFks.Where(x => Equals(x.PluBundle.Plu.IdentityValueUid, value.Plu.IdentityValueUid)).ToList());
                PluNestingFks = temp;
			}

			OnPropertyChanged();
		}
	}
    
    private PluNestingFkModel _pluNestingFk;

    [XmlElement]
    public PluNestingFkModel PluNestingFk
    {
        get => _pluNestingFk;
        set
        {
            _pluNestingFk = value ?? DataAccess.GetItemNewEmpty<PluNestingFkModel>();
            OnPropertyChanged();
        }
    }

    private List<PluNestingFkModel> _pluNestingFks;

    [XmlElement]
    public List<PluNestingFkModel> PluNestingFks
    {
        get => _pluNestingFks;
        set
        {
            _pluNestingFks = value;
			if (value.Exists(x => !x.IsNew) && value.Exists(x => x.IsDefault))
				PluNestingFk = value.Find(x => x.IsDefault);
			else
                PluNestingFk = value.First();
            OnPropertyChanged();
        }
    }

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
	private PublishType _publishType = PublishType.Unknown;
	[XmlElement]
	public PublishType PublishType
	{
		get => _publishType;
		set
		{
			_publishType = value;
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
	private string _sqlInstance;
	[XmlElement]
	private string SqlInstance
	{
		get => _sqlInstance;
		set
		{
			_sqlInstance = value;
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
        // Lists.
        _pluNestingFks = new();
        _productionFacilities = new();
        _productSeries = new();
        _scales = new();
		// Strings
        _sqlInstance = string.Empty;
		_publishDescription = string.Empty;
		// Others.
		_weighingSettings = new();
	}

	#endregion

	#region Public and private methods

	public void SetMain(long scaleId = -1, string productionFacilityName = "")
	{
        SetSqlPublish();
        SetScale(scaleId, productionFacilityName);
		Scales = DataContext.GetListNotNullable<ScaleModel>(SqlCrudConfigUtils.GetCrudConfigSection(false));
		ProductionFacilities = DataContext.GetListNotNullable<ProductionFacilityModel>(SqlCrudConfigUtils.GetCrudConfigSection(false));
	}

	private void SetScale(long scaleId, string productionFacilityName)
	{
		lock (_locker)
		{
            // Device.
            DeviceModel device = GuiUtils.WpfForm.SetNewDeviceWithQuestion(
				DeviceName, NetUtils.GetLocalIpAddress(), NetUtils.GetLocalMacAddress());

            // DeviceTypeFk.
            DeviceTypeFkModel deviceTypeFk = DataAccess.GetItemDeviceTypeFkNotNullable(device);
            if (deviceTypeFk.IsNew)
            {
                // DeviceType.
                DeviceTypeModel deviceType = DataAccess.GetItemDeviceTypeNotNullable("Monoblock");
                //FileLogger.StoreMessage($"{nameof(deviceType)}: {deviceType}");
                // DeviceTypeFk.
                deviceTypeFk.Device = device;
                deviceTypeFk.Type = deviceType;
                DataAccess.Save(deviceTypeFk);
            }
			FileLogger.StoreMessage($"{nameof(deviceTypeFk)}: {deviceTypeFk}");
			FileLogger.StoreMessage($"{nameof(deviceTypeFk.Device)}: {deviceTypeFk.Device}");

			// DeviceTypeFk.
            DeviceScaleFk = DataAccess.GetItemDeviceScaleFkNotNullable(deviceTypeFk.Device);
            FileLogger.StoreMessage($"{nameof(DeviceScaleFk)}: {DeviceScaleFk}");
            FileLogger.StoreMessage($"{nameof(DeviceScaleFk.Scale)}: {DeviceScaleFk.Scale}");
            
			// Scale.
			Scale = scaleId <= 0 ? DeviceScaleFk.Scale : DataAccess.GetScaleNotNullable(scaleId);

            // Area.
            ProductionFacility = DataAccess.GetProductionFacilityNotNullable(productionFacilityName);

			// Other.
			AppVersion.AppDescription = $"{AppVersion.AppTitle}.  {Scale.Description}.";
			ProductDate = DateTime.Now;
			// начинается новыя серия, упаковки продукции, новая паллета
			ProductSeries = new(Scale);
			//ProductSeries.Load();
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
		ManagerControl.PrintMain.LabelsCount = 1;
		ProductSeries.Load();
		//if (Manager is null || Manager.Print is null)
		//    return;
		//Manager.Print.ClearPrintBuffer(true, LabelsCurrent);
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
			GuiUtils.WpfForm.ShowNewOperationControl(owner,
				LocaleCore.Scales.PluPackageNotSelect, true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
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
			GuiUtils.WpfForm.ShowNewOperationControl(owner,
				LocaleCore.Scales.PluNotSelect, true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
			return false;
		}
		return true;
	}

	/// <summary>
	/// Check Massa-K device exists.
	/// </summary>
	/// <param name="owner"></param>
	/// <returns></returns>
	public bool CheckWeightMassaDeviceExists(IWin32Window owner)
	{
        if (Debug.IsDebug) return true;

        if (!PluScale.IsNew && !PluScale.Plu.IsCheckWeight) return true;
		if (ManagerControl.Massa is null)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner,
				LocaleCore.Scales.MassaIsNotFound, true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
			return false;
		}
		return true;
	}

	/// <summary>
	/// Check Massa-K is stable.
	/// </summary>
	/// <param name="owner"></param>
	/// <returns></returns>
	public bool CheckWeightMassaIsStable(IWin32Window owner)
	{
        if (Debug.IsDebug) return true;

        if (PluScale.Plu.IsCheckWeight && !ManagerControl.Massa.MassaStable.IsStable)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner,
				LocaleCore.Scales.MassaIsNotCalc + Environment.NewLine + LocaleCore.Scales.MassaWaitStable,
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
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
			GuiUtils.WpfForm.ShowNewOperationControl(owner,
				LocaleCore.Scales.PluGtinIsNotSet,
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
			return false;
		}
		return true;
	}

	/// <summary>
	/// Check printer connection.
	/// </summary>
	/// <param name="owner"></param>
	/// <returns></returns>
	public bool CheckPrintIsConnect(IWin32Window owner, ManagerPrint managerPrint, bool isMain)
	{
		if (!managerPrint.Printer.IsPing)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, isMain
				? LocaleCore.Print.DeviceMainIsUnavailable + Environment.NewLine + LocaleCore.Print.DeviceCheckConnect
				: LocaleCore.Print.DeviceShippingIsUnavailable + Environment.NewLine + LocaleCore.Print.DeviceCheckConnect,
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
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
	public bool CheckPrintStatusReady(IWin32Window owner, ManagerPrint managerPrint, bool isMain)
	{
		if (!managerPrint.CheckDeviceStatus())
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, isMain
				? LocaleCore.Print.DeviceMainCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus()
				: LocaleCore.Print.DeviceShippingCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus(),
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
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
		if (!PluScale.Plu.IsCheckWeight) return true;

		decimal weight = ManagerControl.Massa.WeightNet - (PluScale.IsNew ? 0 : PluNestingFk.WeightTare);
		if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner,
				LocaleCore.Scales.CheckWeightThreshold(weight), true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
			return false;
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

		decimal weight = ManagerControl.Massa.WeightNet - (PluScale.IsNew ? 0 : PluNestingFk.WeightTare);
		if (weight > LocaleCore.Scales.MassaThresholdValue)
		{
			DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				DeviceScaleFk.Device.Name, nameof(WeightCore));
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
		if (!PluScale.Plu.IsCheckWeight) return true;

        if (PluNestingFk.Nesting.WeightNom > 0 && PluNestingFk.Nesting.WeightMin is not 0 && PluNestingFk.Nesting.WeightMax is not 0)
		{
			if (!(PluWeighing.NettoWeight >= PluNestingFk.Nesting.WeightMin && PluWeighing.NettoWeight <= PluNestingFk.Nesting.WeightMax))
			{
				if (PluWeighing.IsNotNew)
					GuiUtils.WpfForm.ShowNewOperationControl(owner,
						LocaleCore.Scales.CheckWeightThresholds(PluWeighing.NettoWeight, PluScale.IsNew ? 0 : PluNestingFk.Nesting.WeightMax,
						PluScale.IsNew ? 0 : PluNestingFk.Nesting.WeightNom,
						PluScale.IsNew ? 0 : PluNestingFk.Nesting.WeightMin),
						true, LogTypeEnum.Warning,
						new() { ButtonCancelVisibility = Visibility.Visible },
						DeviceScaleFk.Device.Name, nameof(WeightCore));
				return false;
			}
		}
		return true;
	}

	public void PrintLabel(IWin32Window owner, bool isClearBuffer)
	{
		if (Scale is { IsOrder: true })
		{
			throw new("Order under construct!");
			//Order.FactBoxCount = Order.FactBoxCount >= 100 ? 1 : Order.FactBoxCount + 1;
		}

        // #WS-T-710
        //PluScale = DataAccess.GetItemNotNullable<PluScaleModel>(PluScale.IdentityValueUid);
        PluScale.Scale = Scale;
        TemplateModel template = DataAccess.GetItemTemplateNotNullable(PluScale);
		// Template isn't exist.
        if (template.IsNew)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner,
                LocaleCore.Scales.PluTemplateNotSet,
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                DeviceScaleFk.Device.Name, nameof(WeightCore));
            return;
        }
		// Temlate is exists!
		else
        {
			switch (PluScale.Plu.IsCheckWeight)
			{
				case true:
					PrintLabelCore(template, isClearBuffer);
					break;
				default:
					PrintLabelCount(template, isClearBuffer);
					break;
			}
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
		if (template.ImageData.ValueUnicode.Contains("^PQ1") && !PluScale.Plu.IsCheckWeight)
		{
			// Изменить кол-во этикеток.
			if (WeighingSettings.LabelsCountMain > 1)
				template.ImageData.ValueUnicode = template.ImageData.ValueUnicode.Replace(
					"^PQ1", $"^PQ{WeighingSettings.LabelsCountMain}");
			// Печать этикетки.
			PrintLabelCore(template, isClearBuffer);
		}
		// Шаблон без указания кол-ва.
		else
		{
			for (int i = ManagerControl.PrintMain.LabelsCount; i <= WeighingSettings.LabelsCountMain; i++)
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
			NettoWeight = PluScale.Plu.IsCheckWeight ? ManagerControl.Massa.WeightNet - PluNestingFk.WeightTare : PluNestingFk.Nesting.WeightNom,
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
		if (ManagerControl.Massa.WeightNet > 0) return;

		DialogResult dialogResult = GuiUtils.WpfForm.ShowNewOperationControl(owner,
			LocaleCore.Print.QuestionUseFakeData,
			true, LogTypeEnum.Question,
			new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
			DeviceScaleFk.Device.Name, nameof(WeightCore));
		if (dialogResult is DialogResult.Yes)
		{
            ManagerControl.Massa.WeightNet = StringUtils.NextDecimal(PluNestingFk.Nesting.WeightMin, PluNestingFk.Nesting.WeightMax);
            ManagerControl.Massa.IsWeightNetFake = true;
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
				ManagerControl.PrintMain.ClearPrintBuffer();
				if (Scale.IsShipping)
					ManagerControl.PrintShipping.ClearPrintBuffer();
			}

			// Send cmd to the print.
			if (Debug.IsDebug)
			{
				DialogResult dialogResult = GuiUtils.WpfForm.ShowNewOperationControl(
					LocaleCore.Print.QuestionPrintSendCmd, true, LogTypeEnum.Question,
					new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
					DeviceScaleFk.Device.Name, nameof(WeightCore));
				if (dialogResult != DialogResult.Yes)
					return;
			}

			// Send cmd to the print.
			ManagerControl.PrintMain.SendCmd(pluLabel);
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex, true, true, true);
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
			DataAccess.Update(PluWeighing);
	}

	/// <summary>
	/// Create and save PLU label.
	/// </summary>
	/// <param name="template"></param>
	/// <returns></returns>
	private PluLabelModel CreateAndSavePluLabel(TemplateModel template)
	{
		PluLabelModel pluLabel = new()
		{
			PluWeighing = PluWeighing,
			PluScale = PluScale,
			ProductDt = ProductDate,
		};

		XmlDocument xmlArea = ProductionFacility.SerializeAsXmlDocument<ProductionFacilityModel>(true, true);
		pluLabel.Xml = pluLabel.SerializeAsXmlDocument<PluLabelModel>(true, true);
		pluLabel.Xml = DataFormatUtils.XmlMerge(pluLabel.Xml, xmlArea);
		pluLabel.Zpl = DataFormatUtils.XsltTransformation(template.ImageData.ValueUnicode, pluLabel.Xml.OuterXml);
		pluLabel.Zpl = DataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
		pluLabel.Zpl = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(pluLabel.Zpl);
		pluLabel.Zpl = DataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl);

		// Merge.
		//pluLabel.Zpl = zplArea + Environment.NewLine + pluLabel.Zpl;

		// Save.
		DataAccess.Save(pluLabel);

		return pluLabel;
	}

	private void CreateAndSaveBarCodes(PluLabelModel pluLabel)
	{
		BarCodeModel barCode = new() { PluLabel = pluLabel };
		barCode.SetBarCodeTop(pluLabel);
		barCode.SetBarCodeRight(pluLabel);
		barCode.SetBarCodeBottom(pluLabel);
		DataAccess.Save(barCode);
	}

	private void SetSqlPublish()
	{
		PublishType = PublishType.Unknown;
		PublishDescription = "Неизвестный сервер";
		SqlInstance = GetSqlInstanceString();
		SetSqlPublishFromInstance();
	}

	private void SetSqlPublishFromInstance()
	{
		switch (SqlInstance)
		{
			case "INS1":
				PublishType = PublishType.Debug;
				PublishDescription = LocaleCore.Sql.SqlServerTest;
				break;
			case "SQL2019":
				PublishType = PublishType.Develop;
				PublishDescription = LocaleCore.Sql.SqlServerDev;
				break;
			case "LUTON":
				PublishType = PublishType.Release;
				PublishDescription = LocaleCore.Sql.SqlServerProd;
				break;
		}
	}

	private string GetSqlInstanceString()
	{
		string result = string.Empty;
		SqlConnect.ExecuteReader(SqlQueries.DbSystem.Properties.GetInstance, (reader) =>
		{
			if (reader.Read())
			{
				result = SqlConnect.GetValueAsString(reader, "InstanceName");
			}
		});
		return result;
	}

	#endregion
}
