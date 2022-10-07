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
using DataCore.Sql.Fields;
using DataCore.Sql.Models;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels;
using DataCore.Utils;
using MDSoft.BarcodePrintUtils;
using MvvmHelpers;
using WeightCore.Gui;
using WeightCore.Managers;
using System.Linq;
using DataCore.Helpers;

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
		set
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
			if (value.Identity.IsNotNew())
				DataAccess.LogInformation(
					$"{LocaleCore.Scales.PluSet(value.Plu.Identity.Id, value.Plu.Number, value.Plu.Name)}", _scale.Host?.HostName);
			ManagerControl.PrintMain.LabelsCount = 1;
			ManagerControl.PrintShipping.LabelsCount = 1;
			PluPackages = SqlUtils.DataAccess.GetListPluPackages(value.Plu, false, false, true);
			PluPackage = PluPackages.First();
			OnPropertyChanged();
		}
	}
	private PluPackageModel _pluPackage;
	[XmlElement]
	public PluPackageModel PluPackage
	{
		get => _pluPackage;
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
	private HostModel _host;
	[XmlElement]
	public HostModel Host
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
			if (_area.Identity.IsNotNew())
				return _area;
			if (Scale.WorkShop is not null)
				return Scale.WorkShop.ProductionFacility;
			return _area;
		}
		set
		{
			if (value.Identity.IsNotNew())
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
		Scales = SqlUtils.DataAccess.GetListScales(false, false, false);
		Areas = SqlUtils.DataAccess.GetListAreas(false, false, false);
	}

	private void SetScale(long scaleId, string areaName)
	{
		lock (_locker)
		{
			// Host.
			string hostName = NetUtils.GetLocalHostName(false);
			if (string.IsNullOrEmpty(Host.HostName))
			{
				Host = SqlUtils.GetHostNotNull(hostName);
			}

			// Scale.
			Scale = scaleId <= 0 ? SqlUtils.GetScaleFromHostNotNull(Host.Identity.Id) : SqlUtils.GetScaleNotNull(scaleId);

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
		//if (PluScale.Plu.IsCheckWeight && PluPackages.Count > 0 && PluPackage.Identity.IsNew())
		if (PluPackages.Count > 1 && PluPackage.Identity.IsNew())
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.PluPackageNotSelect,
				true, LogTypeEnum.Warning, new() { ButtonCancelVisibility = Visibility.Visible },
				Scale.Host is null ? NetUtils.GetLocalHostName(false) : Scale.Host.HostName, nameof(WeightCore));
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
		if (PluScale.Identity.IsNew())
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.PluNotSelect,
				true, LogTypeEnum.Warning, new() { ButtonCancelVisibility = Visibility.Visible },
				Scale.Host is null ? NetUtils.GetLocalHostName(false) : Scale.Host.HostName, nameof(WeightCore));
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
		if (ManagerControl.Massa is null)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.MassaIsNotFound,
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Scale.Host.HostName, nameof(WeightCore));
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
			GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.MassaIsNotCalc + Environment.NewLine + LocaleCore.Scales.MassaWaitStable,
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Scale.Host.HostName, nameof(WeightCore));
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
				Scale.Host.HostName, nameof(WeightCore));
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
				Scale.Host.HostName, nameof(WeightCore));
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

		decimal weight = ManagerControl.Massa.WeightNet - (PluScale.Identity.IsNew() ? 0 : PluScale.Plu.TareWeight);
		if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
		{
			GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Scale.Host is null ? string.Empty : Scale.Host.HostName, nameof(WeightCore));
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

		decimal weight = ManagerControl.Massa.WeightNet - (PluScale.Identity.IsNew() ? 0 : PluScale.Plu.TareWeight);
		if (weight > LocaleCore.Scales.MassaThresholdValue)
		{
			DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
				true, LogTypeEnum.Warning,
				new() { ButtonCancelVisibility = Visibility.Visible },
				Scale.Host is null ? string.Empty : Scale.Host.HostName, nameof(WeightCore));
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
			if (PluWeighing.Identity.IsNotNew())
				GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThresholds(
					PluWeighing.NettoWeight, PluScale.Identity.IsNew() ? 0 : PluScale.Plu.UpperThreshold,
					PluScale.Identity.IsNew() ? 0 : PluScale.Plu.NominalWeight,
					PluScale.Identity.IsNew() ? 0 : PluScale.Plu.LowerThreshold),
					true, LogTypeEnum.Warning,
					new() { ButtonCancelVisibility = Visibility.Visible },
					Scale.Host is null ? string.Empty : Scale.Host.HostName, nameof(WeightCore));
			return false;
		}
		return true;
	}

	public void PrintLabel(bool isClearBuffer)
	{
		TemplateModel template = new();
		if (Scale is { IsOrder: true })
		{
			throw new Exception("Order under construct!");
			//template = Order.Template;
			//Order.FactBoxCount = Order.FactBoxCount >= 100 ? 1 : Order.FactBoxCount + 1;
		}
		else if (Scale.IsOrder != true)
		{
			//template = PluScale.LoadTemplate();
			if (PluScale.Identity.IsNotNew())
			{
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
					new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, PluScale.Plu.Template.Identity.Id), 0, false, false);
				template = DataAccess.GetItemNotNull<TemplateModel>(sqlCrudConfig);
			}
		}

		// Template exist.
		if (template.Identity.IsNotNew())
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
		SetNewScaleCounter();
	}

	private void SetNewScaleCounter()
	{
		Scale.Counter++;
		DataAccess.Update(Scale);
	}

	/// <summary>
	/// Save item.
	/// </summary>
	/// <param name="pluLabel"></param>
	/// <param name="printCmd"></param>
	private void PrintSaveLabel(PluLabelModel pluLabel, string printCmd)
	{
		pluLabel.Zpl = printCmd;
		DataAccess.Save(pluLabel);
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

	public void SetPluWeighing(IWin32Window owner)
	{
		if (PluScale.Identity.IsNew())
			return;

		// Debug check.
		if (PluScale.Plu.IsCheckWeight && ManagerControl.Massa.WeightNet <= 0 && Debug.IsDebug)
		{
			DialogResult dialogResult = GuiUtils.WpfForm.ShowNewOperationControl(owner,
				LocaleCore.Print.QuestionUseFakeData,
				true, LogTypeEnum.Question,
				new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
				Scale.Host is null ? string.Empty : Scale.Host.HostName,
				nameof(WeightCore));
			if (dialogResult is DialogResult.Yes)
			{
				// Fake data.
				Random random = new();
				ManagerControl.Massa.WeightNet = StringUtils.NextDecimal(
					random, PluScale.Plu.LowerThreshold, PluScale.Plu.UpperThreshold);
				ManagerControl.Massa.IsWeightNetFake = true;
			}
		}

		PluWeighing = new()
		{
			PluScale = PluScale,
			Kneading = WeighingSettings.Kneading,
			NettoWeight = PluScale.Plu.IsCheckWeight ? ManagerControl.Massa.WeightNet - PluScale.Plu.TareWeight : PluScale.Plu.NominalWeight,
			TareWeight = PluScale.Plu.TareWeight
		};
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
			if (PluWeighing.Identity.IsNew())
				DataAccess.Save(PluWeighing);

			//string xmlStringPluWeighing = PluWeighing.SerializeAsXmlString<PluWeighingModel>(true);
			//string xmlStringProductionFacility = Area.Identity.IsNew() ? string.Empty : Area.SerializeAsXmlString<ProductionFacilityModel>(true);
			//string xmlString = XmlUtils.MergeXml(xmlStringPluWeighing, xmlStringProductionFacility);
			//PluLabelModel pluLabel = new() { PluWeighing = PluWeighing, ProductDt = ProductDate };
			//string xmlStringPluLabel = pluLabel.SerializeAsXmlString<PluLabelModel>(true);
			//xmlString = XmlUtils.XmlCompatibleReplace(xmlString);
			//xmlString = XmlUtils.MergeXml(xmlString, xmlStringPluLabel);
			//// XSLT transform.
			//string printCmd = XmlUtils.XsltTransformation(template.ImageData.ValueUnicode, xmlString);

			PluLabelModel pluLabel = new() { PluWeighing = PluWeighing, ProductDt = ProductDate };
			pluLabel.Xml = pluLabel.SerializeAsXmlDocument<PluLabelModel>(true);
			// XSLT transform.
			string printCmd = XmlUtils.XsltTransformation(template.ImageData.ValueUnicode, pluLabel.Xml?.OuterXml);
			printCmd = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(printCmd);
			// Replace ZPL resources.
			printCmd = XmlUtils.PrintCmdReplaceZplResources(printCmd);
			// PLU label.
			PrintSaveLabel(pluLabel, printCmd);
			//if (ManagerControl is null || ManagerControl.PrintMain is null)
			//    return;

			// Print.
			if (isClearBuffer)
			{
				ManagerControl.PrintMain.ClearPrintBuffer();
				if (Scale.IsShipping)
					ManagerControl.PrintShipping.ClearPrintBuffer();
			}

			// Debug check.
			if (Debug.IsDebug)
			{
				DialogResult dialogResult = GuiUtils.WpfForm.ShowNewOperationControl(null, LocaleCore.Print.QuestionPrintSendCmd,
					true, LogTypeEnum.Question,
					new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
					Scale.Host is null ? string.Empty : Scale.Host.HostName,
					nameof(WeightCore));
				if (dialogResult != DialogResult.Yes)
					return;
			}

			// Send cmd to the print.
			ManagerControl.PrintMain.SendCmd(printCmd);
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex);
		}
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

	#endregion
}
