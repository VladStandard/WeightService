// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Settings;
using DataCore.Sql.Core;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using DataCore.Utils;
using MDSoft.BarcodePrintUtils;
using MvvmHelpers;
using WeightCore.Gui;
using WeightCore.Managers;
using System.Linq;
using DataCore.Helpers;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Xml;

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

	private AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
	private SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;
	public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public DataContextModel DataContext { get; } = new();
	public DebugHelper Debug { get; } = DebugHelper.Instance;
	public ManagerControllerHelper ManagerControl { get; } = ManagerControllerHelper.Instance;

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
			if (value.IdentityIsNotNew)
				DataAccess.LogInformation(
					$"{LocaleCore.Scales.PluSet(value.Plu.IdentityValueId, value.Plu.Number, value.Plu.Name)}",
					//_scale.DeviceTypeFk.Device.Name);
					Host.Device.Name);
			ManagerControl.PrintMain.LabelsCount = 1;
			ManagerControl.PrintShipping.LabelsCount = 1;
			PluPackages = DataContext.GetListNotNull<PluPackageModel>(value.Plu, false, false, true);
			PluPackage = PluPackages.Count > 1 ? PluPackages[1] : PluPackages.FirstOrDefault();
			OnPropertyChanged();
		}
	}
#nullable enable
	private PluPackageModel? _pluPackage;
#nullable disable
	[XmlElement]
	public PluPackageModel PluPackage
	{
		get
		{
			if (_pluPackage is null)
				return _pluPackage = DataAccess.GetItemNew<PluPackageModel>();
			return _pluPackage;
		}
		set
		{
			_pluPackage = value;
			OnPropertyChanged();
		}
	}

	private List<PluPackageModel> _pluPackages;
	[XmlElement]
	public List<PluPackageModel> PluPackages
	{
		get => _pluPackages;
		set
		{
			_pluPackages = value;
			OnPropertyChanged();
		}
	}
	private DeviceScaleFkModel _host;
	[XmlElement]
	public DeviceScaleFkModel Host
	{
		get => _host;
		set
		{
			_host = value;
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
			_ = Area;
			PluScale = new();
			OnPropertyChanged();
		}
	}
	//public string HostName => Scale.DeviceTypeFk.Device.Name;

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
	private ProductionFacilityModel _area;
	[XmlElement]
	public ProductionFacilityModel Area
	{
		get
		{
			if (_area.IdentityIsNotNew)
				return _area;
			if (Scale.WorkShop is not null)
				return Scale.WorkShop.ProductionFacility;
			return _area;
		}
		set
		{
			if (value.IdentityIsNotNew)
				_area = value;
			OnPropertyChanged();
		}
	}
	private List<ProductionFacilityModel> _areas;
	[XmlElement]
	public List<ProductionFacilityModel> Areas
	{
		get => _areas;
		set
		{
			_areas = value;
			OnPropertyChanged();
		}
	}
	private PublishTypeEnum _publishType = PublishTypeEnum.Default;
	[XmlElement]
	public PublishTypeEnum PublishType
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
	public readonly DateTime ProductDateMaxValue = DateTime.Now.AddDays(+31);
	public readonly DateTime ProductDateMinValue = DateTime.Now.AddDays(-31);
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

	/// <summary>
	/// Constructor.
	/// </summary>
	public UserSessionHelper()
	{
		_pluScale = new();
		_pluPackage = new();
		_pluPackages = new();
		_pluWeighing = new();
		_host = new();
		_scale = new();
		_scales = new();
		_area = new();
		_areas = new();
		_productSeries = new();
		_weighingSettings = new();
		_publishDescription = string.Empty;
		_sqlInstance = string.Empty;

		SetupPublish();
		Setup(-1, "");
	}

	#endregion

	#region Public and private methods

	public void Setup(long scaleId, string areaName)
	{
		SetScale(scaleId, areaName);
		Scales = DataContext.GetListNotNull<ScaleModel>();
		Areas = DataContext.GetListNotNull<ProductionFacilityModel>();
	}

	private void SetScale(long scaleId, string areaName)
	{
		lock (_locker)
		{
			// Host.
			string hostName = NetUtils.GetLocalHostName(false);
			Host.Device = DataAccess.GetItemDeviceNotNull(hostName);
			//Host = SqlUtils.GetHostNotNull(hostName);
			Host = DataAccess.GetItemDeviceScaleFkNotNull(Host.Device);

			// Scale.
			//Scale = scaleId <= 0 ? SqlUtils.GetScaleFromDeviceTypeFkNotNull(Host) : SqlUtils.GetScaleNotNull(scaleId);
			Scale = scaleId <= 0 ? Host.Scale : SqlUtils.GetScaleNotNull(scaleId);

			// Area.
			Area = SqlUtils.GetAreaNotNull(areaName);

			// Other.
			AppVersion.AppDescription = $"{AppVersion.AppTitle}.  {Scale.Description}.";
			ProductDate = DateTime.Now;
			// начинается новыя серия, упаковки продукции, новая паллета
			ProductSeries = new(Scale);
			//ProductSeries.Load();
			WeighingSettings = new();
		}
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
	/// Check PLU package is empty.
	/// </summary>
	/// <param name="owner"></param>
	/// <returns></returns>
	public bool CheckPluPackageIsEmpty(IWin32Window owner)
	{
		//if (PluScale.Plu.IsCheckWeight && PluPackages.Count > 0 && PluPackage.IdentityIsNew)
		if (PluPackage.IdentityIsNew && PluPackages.Count > 1)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, 
				LocaleCore.Scales.PluPackageNotSelect, true, LogTypeEnum.Warning, 
				new() { ButtonCancelVisibility = Visibility.Visible },
				Host.Device.Name, nameof(WeightCore));
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
		if (PluScale.IdentityIsNew)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, 
				LocaleCore.Scales.PluNotSelect, true, LogTypeEnum.Warning, 
				new() { ButtonCancelVisibility = Visibility.Visible },
				Host.Device.Name, nameof(WeightCore));
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
		if (!PluScale.IdentityIsNew && !PluScale.Plu.IsCheckWeight) return true;
		if (ManagerControl.Massa is null)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, 
				LocaleCore.Scales.MassaIsNotFound, true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Host.Device.Name, nameof(WeightCore));
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
		if (PluScale.Plu.IsCheckWeight && !ManagerControl.Massa.MassaStable.IsStable)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, 
				LocaleCore.Scales.MassaIsNotCalc + Environment.NewLine + LocaleCore.Scales.MassaWaitStable,
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Host.Device.Name, nameof(WeightCore));
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
				Host.Device.Name, nameof(WeightCore));
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
				Host.Device.Name, nameof(WeightCore));
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

		decimal weight = ManagerControl.Massa.WeightNet - (PluScale.IdentityIsNew ? 0 : PluPackage.Package.Weight);
		if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, 
				LocaleCore.Scales.CheckWeightThreshold(weight), true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Host.Device.Name, nameof(WeightCore));
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

		decimal weight = ManagerControl.Massa.WeightNet - (PluScale.IdentityIsNew ? 0 : PluPackage.Package.Weight);
		if (weight > LocaleCore.Scales.MassaThresholdValue)
		{
			DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Host.Device.Name, nameof(WeightCore));
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

		bool isCheck = false;
		if (PluScale.Plu.NominalWeight > 0)
		{
			if (PluWeighing.NettoWeight >= PluScale.Plu.LowerThreshold && PluWeighing.NettoWeight <= PluScale.Plu.UpperThreshold)
				isCheck = true;
		}
		else
			isCheck = true;
		if (!isCheck)
		{
			if (PluWeighing.IdentityIsNotNew)
				GuiUtils.WpfForm.ShowNewOperationControl(owner, 
					LocaleCore.Scales.CheckWeightThresholds(PluWeighing.NettoWeight, PluScale.IdentityIsNew ? 0 : PluScale.Plu.UpperThreshold,
					PluScale.IdentityIsNew ? 0 : PluScale.Plu.NominalWeight,
					PluScale.IdentityIsNew ? 0 : PluScale.Plu.LowerThreshold),
					true, LogTypeEnum.Warning,
					new() { ButtonCancelVisibility = Visibility.Visible },
					Host.Device.Name, nameof(WeightCore));
			return false;
		}
		return true;
	}

#nullable enable
	public void PrintLabel(bool isClearBuffer)
	{
		if (Scale is { IsOrder: true })
		{
			throw new("Order under construct!");
			//Order.FactBoxCount = Order.FactBoxCount >= 100 ? 1 : Order.FactBoxCount + 1;
		}
		
		TemplateModel? template = DataAccess.GetItemTemplate(PluScale);
		// Template exist.
		if (template is not null && template.IdentityIsNotNew)
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
#nullable disable

	public void SetNewScaleCounter()
	{
		Scale.Counter++;
		DataAccess.Update(Scale);
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
		//        CurrentWeighingFact.NettoWeight = Manager.Massa.WeightNet - CurrentPlu.GoodsTareWeight;
		//    else
		//        CurrentWeighingFact.NettoWeight -= CurrentPlu.GoodsTareWeight;
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

#nullable enable
	public void NewPluWeighing()
	{
        ProductSeriesModel? productSeries = DataAccess.GetItemProductSeries(PluScale.Scale);

        PluWeighing = new()
		{
			PluScale = PluScale,
			Kneading = WeighingSettings.Kneading,
			NettoWeight = PluScale.Plu.IsCheckWeight ? ManagerControl.Massa.WeightNet - PluPackage.Package.Weight : PluScale.Plu.NominalWeight,
			TareWeight = PluPackage.Package.Weight,
			Series = productSeries,
		};
        
        // Save or update weighing products.
        SaveOrUpdatePluWeighing();
	}
#nullable disable

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
			Host.Device.Name, nameof(WeightCore));
		if (dialogResult is DialogResult.Yes)
		{
			ManagerControl.Massa.WeightNet = StringUtils.NextDecimal(PluScale.Plu.LowerThreshold, PluScale.Plu.UpperThreshold);
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
					Host.Device.Name, nameof(WeightCore));
				if (dialogResult != DialogResult.Yes)
					return;
			}

			// Send cmd to the print.
			ManagerControl.PrintMain.SendCmd(pluLabel);
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex);
		}
	}

	/// <summary>
	/// Save or update weighing products.
	/// </summary>
	private void SaveOrUpdatePluWeighing()
	{
		if (!PluWeighing.PluScale.Plu.IsCheckWeight) return;

		if (PluWeighing.IdentityIsNew)
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

		XmlDocument xmlArea = Area.SerializeAsXmlDocument<ProductionFacilityModel>(true);
		pluLabel.Xml = pluLabel.SerializeAsXmlDocument<PluLabelModel>(true);
		pluLabel.Xml = XmlUtils.XmlMerge(pluLabel.Xml, xmlArea);
		pluLabel.Zpl = XmlUtils.XsltTransformation(template.ImageData.ValueUnicode, pluLabel.Xml.OuterXml);
        pluLabel.Zpl = XmlUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        pluLabel.Zpl = XmlUtils.PrintCmdReplaceZplResources(pluLabel.Zpl);

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

    private void SetupPublish()
	{
		PublishType = PublishTypeEnum.Default;
		PublishDescription = "Неизвестный сервер";
		SqlInstance = GetSqlInstanceString();
		SetPublishFromInstance();
	}

	private void SetPublishFromInstance()
	{
		switch (SqlInstance)
		{
			case "INS1":
				PublishType = PublishTypeEnum.Debug;
				PublishDescription = LocaleCore.Sql.SqlServerTest;
				break;
			case "SQL2019":
				PublishType = PublishTypeEnum.Dev;
				PublishDescription = LocaleCore.Sql.SqlServerDev;
				break;
			case "LUTON":
				PublishType = PublishTypeEnum.Release;
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

#nullable enable
	public void MakeScreenShot(Form? owner)
#nullable disable
	{
		using MemoryStream stream = new();
		
		if (owner is null)
		{
			System.Drawing.Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);
			using Bitmap bitmap = new(bounds.Width, bounds.Height);
			using Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, bounds.Size);
			Image img = (Image)bitmap;
			img.Save(stream, ImageFormat.Png);
		}
		else
		{
			using Bitmap bitmap = new(owner.Width, owner.Height);
			using Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(owner.Location.X, owner.Location.Y, 0, 0, owner.Size);
			using Image img = (Image)bitmap;
			img.Save(stream, ImageFormat.Png);
		}

		ScaleScreenShotModel scaleScreenShot = new() { Scale = Scale, ScreenShot = stream.ToArray() };
		DataAccess.Save(scaleScreenShot);
	}

	#endregion
}
