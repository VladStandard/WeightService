// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL;
using DataCore.DAL.DataModels;
using DataCore.DAL.TableDirectModels;
using DataCore.DAL.Utils;
using MvvmHelpers;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using WeightCore.Gui;
using WeightCore.Managers;
using WeightCore.Print;
using WeightCore.Zpl;

namespace WeightCore.Helpers
{
    public class SessionStateHelper : BaseViewModel, IDisposable
    {
        #region Design pattern "Lazy Singleton"

        private static SessionStateHelper _instance;
        public static SessionStateHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public ManagerHelper Manager { get; private set; }
        public ExceptionHelper Exception { get; private set; } = ExceptionHelper.Instance;
        public LogHelper Log { get; private set; } = LogHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        public ProductSeriesDirect ProductSeries { get; private set; }
        public HostDirect Host { get; private set; }
        public int CurrentScaleId { get; }
        public OrderDirect CurrentOrder { get; set; }
        [XmlElement(IsNullable = true)]
        private ScaleDirect _currentScale;
        public ScaleDirect CurrentScale
        {
            get => _currentScale;
            set
            {
                _currentScale = value;
                SqlViewModel.SetupTasks(Host?.ScaleId);
                OnPropertyChanged();
            }
        }
        public PrintBrand PrintBrand => CurrentScale != null && CurrentScale.ZebraPrinter.PrinterType.Contains("TSC ")
            ? PrintBrand.TSC : PrintBrand.Zebra;
        [XmlElement(IsNullable = true)]
        public WeighingFactDirect CurrentWeighingFact { get; private set; }
        private int _currentPage;
        /// <summary>
        /// Текущая страница.
        /// </summary>
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        private bool _isWpfPageLoaderClose;
        /// <summary>
        /// Close WpfPageLoader form.
        /// </summary>
        public bool IsWpfPageLoaderClose
        {
            get => _isWpfPageLoaderClose;
            set
            {
                _isWpfPageLoaderClose = value;
                WpfPageLoader_OnClose?.Invoke(null, null);
                OnPropertyChanged();
            }
        }
        public RoutedEventHandler WpfPageLoader_OnClose { get; set; }
        public bool IsCheckWeight => CurrentPlu != null ? CurrentPlu.IsCheckWeight : false;

        #endregion

        #region Constructor and destructor

        public SessionStateHelper()
        {
            // Load ID host from file.
            Host = HostsUtils.TokenRead();
            CurrentScale = ScalesUtils.GetScale(Host.ScaleId);

            Kneading = KneadingMinValue;
            ProductDate = DateTime.Now;
            LabelsCurrent = 1;
            LabelsCount = 1;

            // начинается новыя серия, упаковки продукции, новая паллета
            ProductSeries = new ProductSeriesDirect(CurrentScale);
            //ProductSeries.Load();

            Manager = new ManagerHelper();
        }

        ~SessionStateHelper()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(false);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
                Manager?.Dispose(disposing);
        }

        #endregion

        #region PalletSize

        public static readonly int PalletSizeMinValue = 1;
        public static readonly int PalletSizeMaxValue = 130;
        private int _labelsCount;
        public int LabelsCount
        {
            get => _labelsCount;
            set
            {
                _labelsCount = value;
            }
        }

        public void RotatePalletSize(ProjectsEnums.Direction direction)
        {
            if (direction == ProjectsEnums.Direction.Back)
            {
                LabelsCount--;
                if (LabelsCount < PalletSizeMinValue)
                    LabelsCount = PalletSizeMinValue;

            }
            if (direction == ProjectsEnums.Direction.Forward)
            {
                LabelsCount++;
                if (LabelsCount > PalletSizeMaxValue)
                    LabelsCount = PalletSizeMaxValue;
            }
        }

        #endregion

        #region CurrentBox

        private int _labelsCurrent;
        public int LabelsCurrent
        {
            get => _labelsCurrent;
            set
            {
                _labelsCurrent = value;
            }
        }

        public void NewPallet()
        {
            LabelsCurrent = 1;
            ProductSeries.Load();
            //if (Manager == null || Manager.Print == null)
            //    return;
            //Manager.Print.ClearPrintBuffer(true, LabelsCurrent);
        }

        #endregion

        #region Kneading
        public static readonly int KneadingMinValue = 1;
        public static readonly int KneadingMaxValue = 140;

        private int _kneading;
        public int Kneading
        {
            get => _kneading;
            set
            {
                _kneading = value;
                if (Manager == null || Manager.Print == null)
                    return;
                //Manager.Print.ClearPrintBuffer(true, LabelsCurrent);
            }
        }

        public void RotateKneading(ProjectsEnums.Direction direction)
        {
            if (direction == ProjectsEnums.Direction.Back)
            {
                Kneading--;
                if (Kneading < KneadingMinValue)
                    Kneading = KneadingMinValue;
            }
            if (direction == ProjectsEnums.Direction.Forward)
            {
                Kneading++;
                if (Kneading > KneadingMaxValue)
                    Kneading = KneadingMaxValue;
            }
        }
        #endregion

        #region ProductDate

        public static readonly DateTime ProductDateMaxValue = DateTime.Now.AddDays(+7);
        public static readonly DateTime ProductDateMinValue = DateTime.Now.AddDays(-31);

        private DateTime _productDate;

        public DateTime ProductDate
        {
            get => _productDate;
            set
            {
                _productDate = value;
                if (Manager == null || Manager.Print == null)
                    return;
            }
        }

        public void RotateProductDate(ProjectsEnums.Direction direction)
        {
            if (direction == ProjectsEnums.Direction.Back)
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue)
                    ProductDate = ProductDateMinValue;
            }
            if (direction == ProjectsEnums.Direction.Forward)
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue)
                    ProductDate = ProductDateMaxValue;
            }
        }

        #endregion

        #region PluEntity

        private PluDirect _currentPlu;
        [XmlElement(IsNullable = true)]
        public PluDirect CurrentPlu
        {
            get => _currentPlu;
            private set
            {
                _currentPlu = value;
                LabelsCurrent = 1;
                if (Manager == null || Manager.Print == null)
                    return;
                //Manager.Print.ClearPrintBuffer(true, LabelsCurrent);
            }
        }

        public void SetCurrentPlu(PluDirect plu)
        {
            if (CurrentPlu != plu)
                CurrentPlu = plu;
        }

        public void ClearCurrentPlu()
        {
            if (CurrentPlu != null)
                CurrentPlu = null;
        }

        #endregion

        #region PrintMethods

        /// <summary>
        /// Check PLU is empty.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool CheckPluIsEmpty(IWin32Window owner)
        {
            if (CurrentPlu == null)
            {
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.PluIsEmpty;
                Log.Information(wpfPageLoader.MessageBox.Message);
                wpfPageLoader.MessageBox.ButtonCancelVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog(owner);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
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
            if (Manager == null || Manager.Massa == null)
            {
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.MassaNotFound;
                Log.Information(wpfPageLoader.MessageBox.Message);
                wpfPageLoader.MessageBox.ButtonCancelVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog(owner);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
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
            if (!IsCheckWeight)
                return true;
            if (Manager.Massa.WeightNet - CurrentPlu.GoodsTareWeight < LocalizationData.ScalesUI.MassaThreshold)
            {
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.CheckWeightThreshold(Manager.Massa.WeightNet);
                Log.Information(wpfPageLoader.MessageBox.Message);
                wpfPageLoader.MessageBox.ButtonCancelVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog(owner);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                return result == DialogResult.Cancel;
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
            if (!IsCheckWeight)
                return true;
            if (Manager.Massa.WeightNet - CurrentPlu.GoodsTareWeight > LocalizationData.ScalesUI.MassaThreshold)
            {
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.CheckWeightThreshold(Manager.Massa.WeightNet);
                Log.Information(wpfPageLoader.MessageBox.Message);
                wpfPageLoader.MessageBox.ButtonCancelVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog(owner);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
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
            if (!IsCheckWeight)
                return true;
            bool isCheck = false;
            if (CurrentPlu.NominalWeight > 0)
            {
                if (CurrentWeighingFact.NetWeight >= CurrentPlu.LowerWeightThreshold && CurrentWeighingFact.NetWeight <= CurrentPlu.UpperWeightThreshold)
                    isCheck = true;
            }
            else
                isCheck = true;
            if (!isCheck)
            {
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.CheckWeightThresholds(
                    CurrentWeighingFact.NetWeight, CurrentPlu.UpperWeightThreshold, CurrentPlu.NominalWeight, CurrentPlu.LowerWeightThreshold);
                Log.Information(wpfPageLoader.MessageBox.Message);
                wpfPageLoader.MessageBox.ButtonCancelVisibility = Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog(owner);
                DialogResult result = wpfPageLoader.MessageBox.Result;
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                return result == DialogResult.Cancel;
            }
            return true;
        }

        public void PrintLabel()
        {
            TemplateDirect template = null;
            if (CurrentOrder != null && CurrentScale != null && CurrentScale.UseOrder == true)
            {
                template = CurrentOrder.Template;
                CurrentOrder.FactBoxCount++;
            }
            else if (CurrentPlu != null && CurrentScale != null && CurrentScale.UseOrder != true)
            {
                template = CurrentPlu.Template;
            }

            // Template exist.
            if (template != null)
            {
                switch (IsCheckWeight)
                {
                    case true:
                        PrintLabel(template);
                        break;
                    default:
                        PrintCountLabel(template);
                        break;
                }
            }

            CurrentWeighingFact = null;
        }

        /// <summary>
        /// Replace ZPL's pics
        /// </summary>
        /// <param name="value"></param>
        public void PrintCmdReplacePics(ref string value)
        {
            // Подменить картинки ZPL.
            switch (PrintBrand)
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
                Label = printCmd,
                Zpl = printCmd,
            };
            zplLabel.SaveZpl();
        }

        /// <summary>
        /// Item label printing.
        /// </summary>
        /// <param name="template"></param>
        private void PrintCountLabel(TemplateDirect template)
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
            //    wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
            //    wpfPageLoader.MessageBox.Message =
            //        LocalizationData.ScalesUI.WeightingControl + Environment.NewLine +
            //        $"Вес нетто: {CurrentWeighingFact.NetWeight} кг" + Environment.NewLine +
            //        $"Номинальный вес: {CurrentPlu.NominalWeight} кг" + Environment.NewLine +
            //        $"Верхнее значение веса: {CurrentPlu.UpperWeightThreshold} кг" + Environment.NewLine +
            //        $"Нижнее значение веса: {CurrentPlu.LowerWeightThreshold} кг" + Environment.NewLine + Environment.NewLine +
            //        "Для продолжения печати нажмите Ignore.";
            //    wpfPageLoader.MessageBox.ButtonAbortVisibility = Visibility.Visible;
            //    wpfPageLoader.MessageBox.ButtonRetryVisibility = Visibility.Visible;
            //    wpfPageLoader.MessageBox.ButtonIgnoreVisibility = Visibility.Visible;
            //    wpfPageLoader.MessageBox.Localization();
            //    wpfPageLoader.ShowDialog(owner);
            //    DialogResult result = wpfPageLoader.MessageBox.Result;
            //    wpfPageLoader.Close();
            //    wpfPageLoader.Dispose();
            //    if (result != DialogResult.Ignore)
            //        return;
            //}

            // Шаблон с указанием кол-ва.
            if (template.XslContent.Contains("^PQ1"))
            {
                // Изменить кол-во этикеток.
                if (LabelsCount > 1)
                    template.XslContent = template.XslContent.Replace("^PQ1", $"^PQ{LabelsCount}");
                // Печать этикетки.
                PrintLabel(template);
            }
            // Шаблон без указания кол-ва.
            else
            {
                for (int i = LabelsCurrent; i <= LabelsCount; i++)
                {
                    // Печать этикетки.
                    PrintLabel(template);
                }
            }
        }

        /// <summary>
        /// Вывести серию этикеток по заданному размеру паллеты.
        /// </summary>
        public void SetCurrentWeighingFact()
        {
            if (IsCheckWeight)
                CurrentWeighingFact = new WeighingFactDirect(CurrentScale, CurrentPlu, ProductDate, Kneading,
                    CurrentPlu.Scale.ScaleFactor, Manager.Massa.WeightNet - CurrentPlu.GoodsTareWeight, CurrentPlu.GoodsTareWeight);
            else
                CurrentWeighingFact = new WeighingFactDirect(CurrentScale, CurrentPlu, ProductDate, Kneading,
                    CurrentPlu.Scale.ScaleFactor, CurrentPlu.NominalWeight, CurrentPlu.GoodsTareWeight);
        }

        private void PrintLabel(TemplateDirect template,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                CurrentWeighingFact.Save();

                //string xmlInput = CurrentWeighingFact.SerializeObject();
                string xmlInput = CurrentWeighingFact.SerializeAsXmlWithEmptyNamespaces();
                string printCmd = ZplPipeUtils.XsltTransformationPipe(template.XslContent, xmlInput, true);

                // Replace ZPL's pics.
                PrintCmdReplacePics(ref printCmd);
                // DB save ZPL-query to Labels.
                PrintSaveLabel(printCmd, CurrentWeighingFact.Id);
                if (Manager == null || Manager.Print == null)
                    return;

                // Print.
                Manager.Print.ClearPrintBuffer(true, LabelsCurrent);
                Manager.Print.SendCmd(printCmd);
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, true, filePath, lineNumber, memberName);
            }
        }

        #endregion
    }
}
