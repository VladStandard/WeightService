// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.DAL.Utils;
using DataProjectsCore.Helpers;
using MvvmHelpers;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using WeightCore.Gui;
using WeightCore.Managers;
using WeightCore.Zpl;

namespace WeightCore.Helpers
{
    public class SessionStateHelper : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

        private static SessionStateHelper _instance;
        public static SessionStateHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public readonly TaskManagerHelper TaskManager = TaskManagerHelper.Instance;
        private readonly LogHelper _log = LogHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        public ProductSeriesDirect ProductSeries { get; private set; }
        public HostDirect Host { get; private set; }
        public ZplCommander ZplCommander { get; private set; }
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

        public bool IsTscPrinter => CurrentScale != null && CurrentScale.ZebraPrinter.PrinterType.Contains("TSC ");
        [XmlElement(IsNullable = true)]
        public WeighingFactDirect CurrentWeighingFact { get; set; }

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

        #endregion

        #region Constructor and destructor

        public SessionStateHelper()
        {
            // Load ID host from file.
            Host = HostsUtils.TokenRead();
            CurrentScale = ScalesUtils.GetScale(Host.ScaleId);
            //this.CurrentScaleId = Properties.Settings.Default.CurrentScaleId;
            //this.CurrentScale = new ScaleEntity(this.CurrentScaleId);

            Kneading = KneadingMinValue;
            ProductDate = DateTime.Now;
            LabelsCurrent = 1;
            LabelsCount = 1;

            // контейнер пока не используем
            // оставим для бурного роста
            // ZebraDeviceСontainer = ZebraDeviceСontainer.Instance;
            // ZebraDeviceСontainer.AddDevice(this.CurrentScale.ZebraIP, this.CurrentScale.ZebraPort);
            // ZebraDeviceСontainer.CheckDeviceStatusOn();
            // создаем устройство ZEBRA
            // с необходимым крннектором (т.е. TCP, а можно и через USB)
            // WeightServices.Common.Zpl.DeviceSocketTcp zplDeviceSocket =
            //    new WeightServices.Common.Zpl.DeviceSocketTcp(this.CurrentScale.ZebraPrinter.Ip, this.CurrentScale.ZebraPrinter.Port);
            // ZebraDeviceEntity = new ZebraDeviceEntity(zplDeviceSocket, Guid.NewGuid());
            // ZebraDeviceEntity.DataCollector.SetIpPort(zplDeviceSocket.DeviceIP, zplDeviceSocket.DevicePort);
            // тут запускается поток 
            // который разбирает очередь 
            // т.к. команды пишутся не напрямую, а в очередь
            // а из нее потом доотправляются на устройство
            // zebraDeviceEntity.CheckDeviceStatusOn();
            // тут запускается процесс отправляющий комманды проверки состояния устройства
            // ZplCommander = new ZplCommander(zplDeviceSocket.DeviceIP, zebraDeviceEntity, ZplPipeUtils.ZplHostQuery());

            //try
            //{
            //    PrintManager = new PrintManagerEntity(CurrentScale.ZebraPrinter.Ip, CurrentScale.ZebraPrinter.Port, 120);
            //    PrintManager.Open(IsTscPrinter);
            //}
            //catch (Exception ex)
            //{
            //    if (CustomMessageBox.Show($"Печатающее устройство недоступно ({CurrentScale.ZebraPrinter}). {ex.Message}") == DialogResult.OK)
            //    {

            //    }
            //    //throw new Exception(ex.Message);
            //}

            // тут создается устройство работы с MassaK
            // запускаем поток, который разбирает очередь команд
            // т.к. команды пишутся не напрямую, а в очередь
            // а из нее потом доотправляются на устройство
            //var deviceSocketRs232 = new DeviceSocketRs232(CurrentScale.DeviceComPort);
            //MkDevice = new MkDeviceEntity(deviceSocketRs232);
            //MkDevice.SetZero();

            // тут запускается процесс отправляющий комманды
            // для получения с устройства текущего веса
            //MkCommander mkCommander = new MkCommander(MkDevice);

            // начинается новыя серия
            // упаковки продукции 
            // новая паллета, если хотите
            ProductSeries = new ProductSeriesDirect(CurrentScale);
            ProductSeries.New();
        }

        ~SessionStateHelper()
        {
            ZplCommander?.Close();
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
            if (TaskManager.PrintManager == null)
                return;

            TaskManager.PrintManager.ClearPrintBuffer(IsTscPrinter);
            if (!IsTscPrinter)
                TaskManager.PrintManager.SetOdometorUserLabel(1);
            ProductSeries.New();
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
                // если замес изменился - чистим очередь печати
                if (TaskManager.PrintManager == null)
                    return ;

                TaskManager.PrintManager.ClearPrintBuffer(IsTscPrinter);
                if (!IsTscPrinter)
                    TaskManager.PrintManager.SetOdometorUserLabel(LabelsCurrent);
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
                if (TaskManager.PrintManager == null)
                    return;

                TaskManager.PrintManager.ClearPrintBuffer(IsTscPrinter);
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
            set
            {
                _currentPlu = value;
                LabelsCurrent = 1;
                if (TaskManager.PrintManager == null)
                    return;

                // если ПЛУ изменился - чистим очередь печати
                TaskManager.PrintManager.ClearPrintBuffer(IsTscPrinter);
                TaskManager.PrintManager.SetOdometorUserLabel(1);
            }
        }

        #endregion

        #region PrintMethods

        public void PrintLabel(IWin32Window owner)
        {
            CurrentWeighingFact = null;
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
                switch (CurrentPlu?.CheckWeight)
                {
                    case true:
                        PrintWeightLabel(template);
                        break;
                    default:
                        PrintCountLabel(owner, template);
                        break;
                }
            }
        }

        /// <summary>
        /// Подменить картинки ZPL.
        /// </summary>
        /// <param name="value"></param>
        public void PrintCmdReplacePics(ref string value)
        {
            // Подменить картинки ZPL.
            if (IsTscPrinter)
            {
                TemplateDirect templateEac = new("EAC_107x109_090");
                TemplateDirect templateFish = new("FISH_94x115_000");
                TemplateDirect templateTemp6 = new("TEMP6_116x113_090");
                value = value.Replace("[EAC_107x109_090]", templateEac.XslContent);
                value = value.Replace("[FISH_94x115_000]", templateFish.XslContent);
                value = value.Replace("[TEMP6_116x113_090]", templateTemp6.XslContent);
            }
        }

        /// <summary>
        /// Сохранить ZPL-запрос в таблицу [Labels].
        /// </summary>
        /// <param name="printCmd"></param>
        /// <param name="weithingFactId"></param>
        public void PrintSaveLabel(string printCmd, int weithingFactId)
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
        /// <param name="owner"></param>
        /// <param name="template"></param>
        private void PrintCountLabel(IWin32Window owner, TemplateDirect template)
        {
            // Вывести серию этикеток по заданному размеру паллеты.
            CurrentWeighingFact = WeighingFactDirect.New(CurrentScale, CurrentPlu, ProductDate, Kneading,
                CurrentPlu.Scale.ScaleFactor, CurrentPlu.NominalWeight, CurrentPlu.GoodsTareWeight);

            // Указан номинальный вес.
            bool isCheck = false;
            if (CurrentPlu.NominalWeight > 0)
            {
                if (TaskManager.MassaManager != null)
                    CurrentWeighingFact.NetWeight = TaskManager.MassaManager.WeightNet - CurrentPlu.GoodsTareWeight;
                else
                    CurrentWeighingFact.NetWeight -= CurrentPlu.GoodsTareWeight;
                if (CurrentWeighingFact.NetWeight >= CurrentPlu.LowerWeightThreshold &&
                    CurrentWeighingFact.NetWeight <= CurrentPlu.UpperWeightThreshold)
                {
                    isCheck = true;
                }
            }
            else
                isCheck = true;

            if (!isCheck)
            {
                CustomMessageBox messageBox = new();
                messageBox.Show(owner, LocalizationData.ScalesUI.WeightingControl + Environment.NewLine +
                    $"Вес нетто: {CurrentWeighingFact.NetWeight} кг" + Environment.NewLine +
                    $"Номинальный вес: {CurrentPlu.NominalWeight} кг" + Environment.NewLine +
                    $"Верхнее значение веса: {CurrentPlu.UpperWeightThreshold} кг" + Environment.NewLine +
                    $"Нижнее значение веса: {CurrentPlu.LowerWeightThreshold} кг" + Environment.NewLine + Environment.NewLine +
                    "Для продолжения печати нажмите Ignore.",
                    LocalizationData.ScalesUI.OperationControl,
                    MessageBoxButtons.AbortRetryIgnore);
                //messageBox.Wait();
                if (messageBox.Result != DialogResult.Ignore)
                    return;
            }

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
        /// Weight label printing.
        /// </summary>
        /// <param name="template"></param>
        private void PrintWeightLabel(TemplateDirect template)
        {
            // Проверка наличия устройства весов.
            if (TaskManager.MassaManager == null)
            {
                _log.Information(@"Устройство весов не обнаружено!");
                return;
            }
            // Проверка товара на весах.
            if (TaskManager.MassaManager.WeightNet - CurrentPlu.GoodsTareWeight <= 0)
            {
                _log.Information($@"Вес товара: {TaskManager.MassaManager.WeightNet} кг, печать этикетки невозможна!");
                return;
            }

            CurrentWeighingFact = WeighingFactDirect.New(
                CurrentScale,
                CurrentPlu,
                ProductDate,
                Kneading,
                CurrentPlu.Scale.ScaleFactor,
                TaskManager.MassaManager.WeightNet - CurrentPlu.GoodsTareWeight,
                CurrentPlu.GoodsTareWeight
            );

            // Указан номинальный вес.
            bool isCheck = false;
            if (CurrentPlu.NominalWeight > 0)
            {
                if (CurrentWeighingFact.NetWeight >= CurrentPlu.LowerWeightThreshold &&
                    CurrentWeighingFact.NetWeight <= CurrentPlu.UpperWeightThreshold)
                {
                    isCheck = true;
                }
            }
            else
                isCheck = true;
            if (!isCheck)
                return;

            // Печать этикетки.
            PrintLabel(template);
        }

        /// <summary>
        /// Печать этикетки.
        /// </summary>
        /// <param name="template"></param>
        private void PrintLabel(TemplateDirect template)
        {
            CurrentWeighingFact.Save();

            string xmlInput = CurrentWeighingFact.SerializeObject();
            string printCmd = ZplPipeUtils.XsltTransformationPipe(template.XslContent, xmlInput, true);

            // Подменить картинки ZPL.
            PrintCmdReplacePics(ref printCmd);
            // Сохранить ZPL-запрос в таблицу [Labels].
            PrintSaveLabel(printCmd, CurrentWeighingFact.Id);
            if (TaskManager.PrintManager == null)
                return;

            // Отправить задание в очередь печати.
            TaskManager.PrintManager.Send(printCmd);
        }

        #endregion
    }
}
