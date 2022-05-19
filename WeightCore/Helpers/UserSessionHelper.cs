// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Settings;
using DataCore.Sql;
using DataCore.Sql.Controllers;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using MvvmHelpers;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using WeightCore.Gui;
using WeightCore.Managers;
using WeightCore.Print;
using WeightCore.Zpl;
using static DataCore.ShareEnums;

namespace WeightCore.Helpers
{
    public class UserSessionHelper : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

        private static UserSessionHelper _instance;
        public static UserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public AppVersionHelper AppVersion { get; private set; } = AppVersionHelper.Instance;
        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        public ManagerControllerHelper ManagerControl { get; private set; } = ManagerControllerHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        public ProductSeriesDirect ProductSeries { get; private set; }
        //public HostDirect Host { get; private set; }
        public OrderDirect Order { get; set; }
        private ScaleEntity _scale;
        public ScaleEntity Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                SqlViewModel.SetupTasks(_scale.IdentityId);
                OnPropertyChanged();
            }
        }
        public PrintBrand PrintBrandMain => Scale?.PrinterMain != null &&
            Scale.PrinterMain.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
        public PrintBrand PrintBrandShipping => Scale?.PrinterShipping != null &&
            Scale.PrinterShipping.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
        [XmlElement(IsNullable = true)]
        public WeighingFactDirect WeighingFact { get; private set; }
        public bool IsPluCheckWeight => Plu?.IsCheckWeight == true;
        public WeighingSettingsEntity WeighingSettings { get; set; }
        public Stopwatch StopwatchMain { get; set; }

        public static readonly DateTime ProductDateMaxValue = DateTime.Now.AddDays(+31);
        public static readonly DateTime ProductDateMinValue = DateTime.Now.AddDays(-31);
        private DateTime _productDate;
        public DateTime ProductDate
        {
            get => _productDate;
            set
            {
                _productDate = value;
                if (ManagerControl == null || ManagerControl.PrintMain == null)
                    return;
            }
        }

        private PluDirect _plu;
        [XmlElement]
        public PluDirect Plu
        {
            get => _plu;
            private set
            {
                _plu = value;
                ManagerControl.PrintMain.LabelsCount = 1;
                ManagerControl.PrintShipping.LabelsCount = 1;
            }
        }
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public UserSessionHelper()
        {
            lock (_locker)
            {
                HostEntity host = HostsUtils.GetHostEntity(NetUtils.GetLocalHostName(false));
                Scale = HostsUtils.GetScaleEntity(host.IdentityId);

                AppVersion.AppDescription = $"{AppVersion.AppTitle}.  {Scale.Description}.";
                ProductDate = DateTime.Now;
                // начинается новыя серия, упаковки продукции, новая паллета
                ProductSeries = new(Scale);
                //ProductSeries.Load();
                WeighingSettings = new();
            }
        }

        #endregion

        #region Public and private methods

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
            if (direction == ProjectsEnums.Direction.Left)
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue)
                    ProductDate = ProductDateMinValue;
            }
            if (direction == ProjectsEnums.Direction.Right)
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue)
                    ProductDate = ProductDateMaxValue;
            }
        }

        public void SetCurrentPlu(PluDirect plu)
        {
            if ((Plu = plu) != null)
                DataAccess.Log.LogInformation($"{LocaleCore.Scales.PluSet(Plu.Id, Plu.PLU, Plu.GoodsName)}", Scale.Host.HostName);
        }

        /// <summary>
        /// Check PLU is empty.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool CheckPluIsEmpty(IWin32Window owner)
        {
            if (Plu == null)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.PluNotSelect,
                    true, LogType.Warning,
                    new() { ButtonCancelVisibility = Visibility.Visible },
                    Scale.Host.HostName, nameof(WeightCore));
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
            if (Plu.IsCheckWeight && !ManagerControl.Massa.MassaStable.IsStable)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.MassaIsNotCalc + Environment.NewLine + LocaleCore.Scales.MassaWaitStable,
                    true, LogType.Warning,
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
                    true, LogType.Warning,
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
        /// <exception cref="NotImplementedException"></exception>
        public bool CheckPrintStatusReady(IWin32Window owner, ManagerPrint managerPrint, bool isMain)
        {
            if (!managerPrint.CheckDeviceStatus())
            {
                GuiUtils.WpfForm.ShowNewOperationControl(owner, isMain
                    ? LocaleCore.Print.DeviceMainCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus()
                    : LocaleCore.Print.DeviceShippingCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus(),
                    true, LogType.Warning,
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
            if (!IsPluCheckWeight)
                return true;
            decimal weight = ManagerControl.Massa.WeightNet - Plu.GoodsTareWeight;
            if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight), 
                    true, LogType.Warning,
                    new() { ButtonCancelVisibility = Visibility.Visible },
                    Scale.Host.HostName, nameof(WeightCore));
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
            decimal weight = ManagerControl.Massa.WeightNet - Plu.GoodsTareWeight;
            if (weight > LocaleCore.Scales.MassaThresholdValue)
            {
                DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight), 
                    true, LogType.Warning,
                    new() { ButtonCancelVisibility = Visibility.Visible },
                    Scale.Host.HostName, nameof(WeightCore));
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
            if (Plu.NominalWeight > 0)
            {
                if (WeighingFact.NetWeight >= Plu.LowerWeightThreshold && WeighingFact.NetWeight <= Plu.UpperWeightThreshold)
                    isCheck = true;
            }
            else
                isCheck = true;
            if (!isCheck)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThresholds(
                    WeighingFact.NetWeight, Plu.UpperWeightThreshold, Plu.NominalWeight, Plu.LowerWeightThreshold),
                    true, LogType.Warning,
                    new() { ButtonCancelVisibility = Visibility.Visible },
                    Scale.Host.HostName, nameof(WeightCore));
                return false;
            }
            return true;
        }

        public void PrintLabel(bool isClearBuffer)
        {
            TemplateDirect template = null;
            if (Order != null && Scale != null && Scale.IsOrder == true)
            {
                template = Order.Template;
                Order.FactBoxCount++;
            }
            else if (Plu != null && Scale != null && Scale.IsOrder != true)
            {
                template = Plu.Template;
            }

            // Template exist.
            if (template != null)
            {
                switch (IsPluCheckWeight)
                {
                    case true:
                        PrintLabel(template, isClearBuffer);
                        break;
                    default:
                        PrintLabelCount(template, isClearBuffer);
                        break;
                }
            }

            WeighingFact = null;
        }

        /// <summary>
        /// Replace ZPL's pics
        /// </summary>
        /// <param name="value"></param>
        public void PrintCmdReplacePics(ref string value)
        {
            // Подменить картинки ZPL.
            switch (PrintBrandMain)
            {
                case PrintBrand.Default:
                    break;
                case PrintBrand.Zebra:
                    break;
                case PrintBrand.TSC:
                    TemplateDirect templateEac = new("EAC_107x109_090");
                    TemplateDirect templateFish = new("FISH_94x115_000");
                    TemplateDirect templateTemp6 = new("TEMP6_116x113_090");
                    value = value.Replace("[EAC_107x109_090]", templateEac.XslContent);
                    value = value.Replace("[FISH_94x115_000]", templateFish.XslContent);
                    value = value.Replace("[TEMP6_116x113_090]", templateTemp6.XslContent);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Сохранить ZPL-запрос в таблицу [Labels].
        /// </summary>
        /// <param name="printCmd"></param>
        /// <param name="weithingFactId"></param>
        public void PrintSaveLabel(string printCmd, long weithingFactId)
        {
            ZplLabelDirect zplLabel = new()
            {
                WeighingFactId = weithingFactId,
                Zpl = printCmd,
            };
            zplLabel.SaveZpl();
        }

        /// <summary>
        /// Count label printing.
        /// </summary>
        /// <param name="template"></param>
        private void PrintLabelCount(TemplateDirect template, bool isClearBuffer)
        {
            //// Указан номинальный вес.
            //bool isCheck = false;
            //if (CurrentPlu.NominalWeight > 0)
            //{
            //    if (Manager?.Massa != null)
            //        CurrentWeighingFact.NetWeight = Manager.Massa.WeightNet - CurrentPlu.GoodsTareWeight;
            //    else
            //        CurrentWeighingFact.NetWeight -= CurrentPlu.GoodsTareWeight;
            //    if (CurrentWeighingFact.NetWeight >= CurrentPlu.LowerWeightThreshold &&
            //        CurrentWeighingFact.NetWeight <= CurrentPlu.UpperWeightThreshold)
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
            //        $"Вес нетто: {CurrentWeighingFact.NetWeight} кг" + Environment.NewLine +
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
            if (template.XslContent.Contains("^PQ1") && !IsPluCheckWeight)
            {
                // Изменить кол-во этикеток.
                if (WeighingSettings.LabelsCountMain > 1)
                    template.XslContent = template.XslContent.Replace("^PQ1", $"^PQ{WeighingSettings.LabelsCountMain}");
                // Печать этикетки.
                PrintLabel(template, isClearBuffer);
            }
            // Шаблон без указания кол-ва.
            else
            {
                for (int i = ManagerControl.PrintMain.LabelsCount; i <= WeighingSettings.LabelsCountMain; i++)
                {
                    // Печать этикетки.
                    PrintLabel(template, isClearBuffer);
                }
            }
        }

        /// <summary>
        /// Вывести серию этикеток по заданному размеру паллеты.
        /// </summary>
        public void SetWeighingFact()
        {
            if (IsPluCheckWeight)
                WeighingFact = new(Scale, Plu, ProductDate, WeighingSettings.Kneading,
                    Plu.Scale.ScaleFactor, ManagerControl.Massa.WeightNet - Plu.GoodsTareWeight, Plu.GoodsTareWeight);
            else
                WeighingFact = new(Scale, Plu, ProductDate, WeighingSettings.Kneading,
                    Plu.Scale.ScaleFactor, Plu.NominalWeight, Plu.GoodsTareWeight);
        }

        /// <summary>
        /// Weight label printing.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="isClearBuffer"></param>
        private void PrintLabel(TemplateDirect template, bool isClearBuffer)
        {
            try
            {
                WeighingFact.Save();

                //string xmlInput = CurrentWeighingFact.SerializeObject();
                string xmlInput = WeighingFact.SerializeAsXmlWithEmptyNamespaces<WeighingFactDirect>();
                string printCmd = ZplPipeUtils.XsltTransformationPipe(template.XslContent, xmlInput);

                // Replace ZPL's pics.
                PrintCmdReplacePics(ref printCmd);
                // DB save ZPL-query to Labels.
                PrintSaveLabel(printCmd, WeighingFact.Id);
                if (ManagerControl == null || ManagerControl.PrintMain == null)
                    return;

                // Print.
                if (isClearBuffer)
                {
                    ManagerControl.PrintMain.ClearPrintBuffer();
                    if (Scale.IsOrder)
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
}
