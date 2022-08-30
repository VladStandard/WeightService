// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Settings;
using DataCore.Sql;
using DataCore.Sql.Fields;
using DataCore.Sql.Models;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using MDSoft.BarcodePrintUtils;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using WeightCore.Gui;
using WeightCore.Managers;
using static DataCore.ShareEnums;

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
    public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public DebugHelper Debug { get; } = DebugHelper.Instance;
    public ManagerControllerHelper ManagerControl { get; } = ManagerControllerHelper.Instance;
    public SqlViewModelHelper SqlViewModel { get; } = SqlViewModelHelper.Instance;
    public ProductSeriesDirect ProductSeries { get; private set; } = new();
    public PrintBrand PrintBrandMain => SqlViewModel.Scale.PrinterMain != null &&
        SqlViewModel.Scale.PrinterMain.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
    public PrintBrand PrintBrandShipping => SqlViewModel.Scale.PrinterShipping != null &&
        SqlViewModel.Scale.PrinterShipping.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
    [XmlElement(IsNullable = true)] public PluWeighingEntity? PluWeighing { get; private set; }
    public WeighingSettingsEntity WeighingSettings { get; private set; } = new();
    public Stopwatch StopwatchMain { get; set; } = new();
    public bool IsPluCheckWeight => PluScale is { Plu.IsCheckWeight: true };

    private PluScaleEntity? _pluScale;
    [XmlElement] public PluScaleEntity? PluScale
    {
        get => _pluScale;
        private set
        {
            _pluScale = value;
            ManagerControl.PrintMain.LabelsCount = 1;
            ManagerControl.PrintShipping.LabelsCount = 1;
        }
    }
    private readonly object _locker = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public UserSessionHelper()
    {
        Setup(-1, "");
    }

    #endregion

    #region Public and private methods

    public void Setup(long scaleId, string hostName)
    {
        lock (_locker)
        {
            if (string.IsNullOrEmpty(hostName))
                hostName = NetUtils.GetLocalHostName(false);
            HostEntity host = SqlUtils.GetHost(hostName);
            SqlViewModel.Scale = scaleId <= 0 ? SqlUtils.GetScaleFromHost(host.IdentityId) : SqlUtils.GetScale(scaleId);

            AppVersion.AppDescription = $"{AppVersion.AppTitle}.  {SqlViewModel.Scale.Description}.";
            //AppVersion.AppDescription = $"{AppVersion.AppTitle}. ";
            SqlViewModel.ProductDate = DateTime.Now;
            // начинается новыя серия, упаковки продукции, новая паллета
            ProductSeries = new(SqlViewModel.Scale);
            //ProductSeries.Load();
            WeighingSettings = new();
        }
    }

    public void NewPallet()
    {
        ManagerControl.PrintMain.LabelsCount = 1;
        ProductSeries.Load();
        //if (Manager == null || Manager.Print == null)
        //    return;
        //Manager.Print.ClearPrintBuffer(true, LabelsCurrent);
    }

    public void RotateProductDate(ProjectsEnums.Direction direction)
    {
        switch (direction)
        {
            case ProjectsEnums.Direction.Left:
                {
                    SqlViewModel.ProductDate = SqlViewModel.ProductDate.AddDays(-1);
                    if (SqlViewModel.ProductDate < SqlViewModel.ProductDateMinValue)
                        SqlViewModel.ProductDate = SqlViewModel.ProductDateMinValue;
                    break;
                }
            case ProjectsEnums.Direction.Right:
                {
                    SqlViewModel.ProductDate = SqlViewModel.ProductDate.AddDays(1);
                    if (SqlViewModel.ProductDate > SqlViewModel.ProductDateMaxValue)
                        SqlViewModel.ProductDate = SqlViewModel.ProductDateMaxValue;
                    break;
                }
        }
    }

    public void SetCurrentPlu(PluScaleEntity pluScale)
    {
        if ((PluScale = pluScale) != null)
        {
            DataAccess.Log.LogInformation($"{LocaleCore.Scales.PluSet(PluScale.Plu.IdentityId, PluScale.Plu.Number, PluScale.Plu.Name)}",
                SqlViewModel.Scale.Host?.HostName);
        }
    }

    /// <summary>
    /// Check PLU is empty.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckPluIsEmpty(IWin32Window owner)
    {
        if (PluScale == null)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.PluNotSelect,
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
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
        if (ManagerControl == null || ManagerControl.Massa == null)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.MassaIsNotFound,
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
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
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
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
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
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
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
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
        if (!IsPluCheckWeight)
            return true;
        decimal weight = ManagerControl.Massa.WeightNet - (PluScale == null ? 0 : PluScale.Plu.TareWeight);
        if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host == null ? string.Empty : SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
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
        if (!IsPluCheckWeight)
            return true;
        decimal weight = ManagerControl.Massa.WeightNet - (PluScale == null ? 0 : PluScale.Plu.TareWeight);
        if (weight > LocaleCore.Scales.MassaThresholdValue)
        {
            DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
                true, LogType.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host == null ? string.Empty : SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
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
        if (!IsPluCheckWeight)
            return true;
        bool isCheck = false;
        if (PluScale?.Plu.NominalWeight > 0)
        {
            if (PluWeighing?.NettoWeight >= PluScale.Plu.LowerThreshold && PluWeighing?.NettoWeight <= PluScale.Plu.UpperThreshold)
                isCheck = true;
        }
        else
            isCheck = true;
        if (!isCheck)
        {
            if (PluWeighing != null)
                GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThresholds(
                    PluWeighing.NettoWeight, PluScale == null ? 0 : PluScale.Plu.UpperThreshold,
                    PluScale == null ? 0 : PluScale.Plu.NominalWeight,
                    PluScale == null ? 0 : PluScale.Plu.LowerThreshold),
                    true, LogType.Warning,
                    new() { ButtonCancelVisibility = Visibility.Visible },
                    SqlViewModel.Scale.Host == null ? string.Empty : SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    public void PrintLabel(bool isClearBuffer)
    {
        TemplateEntity? template = null;
        if (SqlViewModel.Scale is { IsOrder: true })
        {
            throw new Exception("Order under construct!");
            //template = SqlViewModel.Order.Template;
            //SqlViewModel.Order.FactBoxCount = SqlViewModel.Order.FactBoxCount >= 100 ? 1 : SqlViewModel.Order.FactBoxCount + 1;
        }
        else if (SqlViewModel.Scale.IsOrder != true)
        {
            //template = PluScale?.LoadTemplate();
            if (PluScale != null)
            {
	            SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(new() 
		            { new(DbField.IdentityId, DbComparer.Equal, PluScale.Plu.Template.IdentityId) }, null, 0, false,false);
                template = DataAccess.Crud.GetItem<TemplateEntity>(sqlCrudConfig);
            }
        }

        // Template exist.
        if (template != null)
        {
            switch (IsPluCheckWeight)
            {
                case true:
                    PrintLabelCore(template, isClearBuffer);
                    break;
                default:
                    PrintLabelCount(template, isClearBuffer);
                    break;
            }
        }
        PluWeighing = null;
        SetNewScaleCounter();
    }

    private void SetNewScaleCounter()
    {
        SqlViewModel.Scale.Counter++;
        DataAccess.Crud.Update(SqlViewModel.Scale);
    }

    /// <summary>
    /// Save item.
    /// </summary>
    /// <param name="printCmd"></param>
    /// <param name="pluWeighing"></param>
    private void PrintSaveLabel(string printCmd, PluWeighingEntity pluWeighing)
    {
        PluLabelEntity pluLabel = new()
        {
            PluWeighing = pluWeighing,
            Zpl = printCmd,
        };
        DataAccess.Crud.Save(pluLabel);
    }

    /// <summary>
    /// Count label printing.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCount(TemplateEntity template, bool isClearBuffer)
    {
        //// Указан номинальный вес.
        //bool isCheck = false;
        //if (CurrentPlu.NominalWeight > 0)
        //{
        //    if (Manager?.Massa != null)
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
        //    using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
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
        if (template.ImageData.ValueUnicode.Contains("^PQ1") && !IsPluCheckWeight)
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

    /// <summary>
    /// Вывести серию этикеток по заданному размеру паллеты.
    /// </summary>
    public void SetWeighingFact()
    {
        if (PluScale == null)
            return;

        PluWeighing = new();
        PluWeighing.PluScale = PluScale;
        PluWeighing.PluScale.Scale = SqlViewModel.Scale;
        PluWeighing.PluScale.Scale.ScaleFactor = PluScale.Scale.ScaleFactor;
        PluWeighing.ProductDt = SqlViewModel.ProductDate;
        PluWeighing.Kneading = WeighingSettings.Kneading;
        PluWeighing.NettoWeight = IsPluCheckWeight ? ManagerControl.Massa.WeightNet - PluScale.Plu.TareWeight : PluScale.Plu.NominalWeight;
        PluWeighing.TareWeight = PluScale.Plu.TareWeight;
    }

    /// <summary>
    /// Weight label printing.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCore(TemplateEntity template, bool isClearBuffer)
    {
        try
        {
            if (PluWeighing == null)
                return;

            DataAccess.Crud.Save(PluWeighing);

            string xmlWeighingFact = PluWeighing.SerializeAsXml<PluWeighingEntity>(true);
            string xmlArea = string.Empty;
            if (SqlViewModel.Area != null)
                xmlArea = SqlViewModel.Area.SerializeAsXml<ProductionFacilityEntity>(true);
            xmlWeighingFact = Zpl.ZplUtils.XmlCompatibleReplace(xmlWeighingFact);
            string xml = Zpl.ZplUtils.MergeXml(xmlWeighingFact, xmlArea);
            // XSLT transform.
            string printCmd = Zpl.ZplUtils.XsltTransformation(template.ImageData.ValueUnicode, xml);
            printCmd = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(printCmd);
            // Replace ZPL resources.
            printCmd = Zpl.ZplUtils.PrintCmdReplaceZplResources(printCmd);
            // DB save ZPL query to Labels.
            PrintSaveLabel(printCmd, PluWeighing);
            //if (ManagerControl == null || ManagerControl.PrintMain == null)
            //    return;

            // Print.
            if (isClearBuffer)
            {
                ManagerControl.PrintMain.ClearPrintBuffer();
                if (SqlViewModel.Scale.IsShipping)
                    ManagerControl.PrintShipping.ClearPrintBuffer();
            }
            ManagerControl.PrintMain.SendCmd(printCmd);
        }
        catch (Exception ex)
        {
            GuiUtils.WpfForm.CatchException(ex);
        }
    }

    #endregion
}
