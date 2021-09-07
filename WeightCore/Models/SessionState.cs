// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.DAL.Utils;
using DataProjectsCore.Utils;
using DataShareCore.Utils;
using MvvmHelpers;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using WeightCore.Gui;
using WeightCore.Managers;
using WeightCore.Zabbix;
using WeightCore.Zpl;

namespace WeightCore.Models
{
    public class SessionState : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

        private static SessionState _instance;
        public static SessionState Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        private TaskManagerEntity _taskManager = TaskManagerEntity.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; } = SqlViewModelEntity.Instance;
        public string AppVersion => AppVersionUtils.GetMainFormText(Assembly.GetExecutingAssembly());
        public ProductSeriesDirect ProductSeries { get; private set; }
        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        public HostDirect Host { get; private set; }
        public ZplCommander ZplCommander { get; private set; }
        public ZabbixHttpListener HttpListener { get; private set; }
        private CancellationToken _token;
        private CancellationToken _tokenHttpListener;
        private ThreadChecker _threadChecker;
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
                SqlViewModel.SetupTasks(Host?.CurrentScaleId);
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

        /// <summary>
        /// Текущая страница.
        /// </summary>
        public string CurrentPageAsString => $"Текущая страница: {_currentPage}";

        private LogUtils _logUtils = LogUtils.Instance;
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

        public SessionState()
        {
            ProductDate = DateTime.Now;

            // Load ID host from file.
            Host = HostsUtils.TokenRead();
            CurrentScale = ScalesUtils.GetScale(Host.CurrentScaleId);

            //this.CurrentScaleId = Properties.Settings.Default.CurrentScaleId;
            //this.CurrentScale = new ScaleEntity(this.CurrentScaleId);
            //<---

            Kneading = KneadingMinValue;
            ProductDate = DateTime.Now;
            CurrentBox = 1;
            PalletSize = 1;

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

        ~SessionState()
        {
            ZplCommander?.Close();
        }

        #endregion

        #region PalletSize

        public static readonly int PalletSizeMinValue = 1;
        public static readonly int PalletSizeMaxValue = 130;
        public delegate void OnResponseHandlerPalletSize(int palletSize);
        public event OnResponseHandlerPalletSize NotifyPalletSize;
        private int _palletSize;
        public int PalletSize
        {
            get => _palletSize;
            set
            {
                _palletSize = value;
                CurrentBox = 1;
                NotifyPalletSize?.Invoke(value);
            }
        }

        public void RotatePalletSize(Direction direction)
        {
            if (direction == Direction.Back)
            {
                PalletSize--;
                if (PalletSize < PalletSizeMinValue)
                    PalletSize = PalletSizeMinValue;

            }
            if (direction == Direction.Forward)
            {
                PalletSize++;
                if (PalletSize > PalletSizeMaxValue)
                    PalletSize = PalletSizeMaxValue;
            }
        }

        #endregion

        #region CurrentBox

        public delegate void OnResponseHandlerCurrentBox(int currentBox);
        public event OnResponseHandlerCurrentBox NotifyCurrentBox;

        private int _currentBox;
        public int CurrentBox
        {
            get => _currentBox;
            set
            {
                _currentBox = value;
                NotifyCurrentBox?.Invoke(value);
            }
        }

        public void NewPallet()
        {
            CurrentBox = 1;
            //если новая паллета - чистим очередь печати
            if (_taskManager.PrintManager != null)
            {
                _taskManager.PrintManager.ClearPrintBuffer(IsTscPrinter);
                if (!IsTscPrinter)
                    _taskManager.PrintManager.SetOdometorUserLabel(1);
                ProductSeries.New();
            }
        }

        #endregion

        #region Kneading
        public static readonly int KneadingMinValue = 1;
        public static readonly int KneadingMaxValue = 140;

        public delegate void OnResponseHandlerKneading(int kneading);
        public event OnResponseHandlerKneading NotifyKneading;
        private int _kneading;

        public int Kneading
        {
            get => _kneading;
            set
            {
                // если замес изменился - чистим очередь печати
                if (_taskManager.PrintManager != null)
                {
                    _taskManager.PrintManager.ClearPrintBuffer(IsTscPrinter);
                    if (!IsTscPrinter)
                        _taskManager.PrintManager.SetOdometorUserLabel(CurrentBox);
                }
                _kneading = value;
                NotifyKneading?.Invoke(value);
            }
        }

        public void RotateKneading(Direction direction)
        {
            if (direction == Direction.Back)
            {
                Kneading--;
                if (Kneading < KneadingMinValue)
                    Kneading = KneadingMinValue;

            }
            if (direction == Direction.Forward)
            {
                Kneading++;
                if (Kneading > KneadingMaxValue)
                    Kneading = KneadingMaxValue;

            }
        }
        #endregion

        #region ProductDate

        public static readonly DateTime ProductDateMaxValue = DateTime.Now.AddDays(+7);
        public static readonly DateTime ProductDateMinValue = DateTime.Now.AddDays(-1);

        public delegate void OnResponseHandlerProductDate(DateTime productDate);
        public event OnResponseHandlerProductDate NotifyProductDate;
        private DateTime _productDate;

        public DateTime ProductDate
        {
            get => _productDate;
            set
            {
                //если дата изменилась - чистим очередь печати
                if (_taskManager.PrintManager != null)
                    _taskManager.PrintManager.ClearPrintBuffer(IsTscPrinter);
                _productDate = value;
                NotifyProductDate?.Invoke(value);
            }
        }

        public void RotateProductDate(Direction direction)
        {
            if (direction == Direction.Back)
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue)
                    ProductDate = ProductDateMinValue;

            }
            if (direction == Direction.Forward)
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue)
                    ProductDate = ProductDateMaxValue;
            }
        }
        #endregion

        #region PluEntity

        public delegate void OnResponseHandlerPlu(PluDirect plu);
        public event OnResponseHandlerPlu NotifyPlu;
        private PluDirect _currentPlu;
        [XmlElement(IsNullable = true)]
        public PluDirect CurrentPlu
        {
            get => _currentPlu;
            set
            {
                // если ПЛУ изменился - чистим очередь печати
                _taskManager.PrintManager?.ClearPrintBuffer(IsTscPrinter);
                _taskManager.PrintManager?.SetOdometorUserLabel(1);
                _currentPlu = value;
                CurrentBox = 1;
                NotifyPlu?.Invoke(value);
            }
        }

        #endregion

        #region PrintMethods

        public void ProcessWeighingResult(IWin32Window owner)
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

            // Есть шаблон, есть PLU.
            if (template != null && CurrentPlu != null)
            {
                switch (CurrentPlu.CheckWeight)
                {
                    case true:
                        // Печать весовых этикеток.
                        PrintWeightLabel(owner, template);
                        break;
                    default:
                        // Печать штучных этикеток.
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
        /// <param name="labelId"></param>
        public void PrintSaveLabel(ref string printCmd, int labelId)
        {
            ZplLabelDirect zplLabel = new()
            {
                Content = printCmd,
                WeighingFactId = labelId,
            };
            zplLabel.Save();
        }

        [Obsolete(@"Use PrintCountLabel")]
        private void PrintCountLabelOld(TemplateDirect template)
        {
            // Вывести серию этикеток по заданному размеру паллеты.
            for (int i = CurrentBox; i <= PalletSize; i++)
            {
                CurrentWeighingFact = WeighingFactDirect.New(
                    CurrentScale,
                    CurrentPlu,
                    ProductDate,
                    Kneading,
                    CurrentPlu.Scale.ScaleFactor,
                    CurrentPlu.NominalWeight,
                    CurrentPlu.GoodsTareWeight
                );

                // Печать этикетки.
                PrintLabel(template);
            }
        }

        /// <summary>
        /// Печать штучных этикеток.
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
                if (_taskManager.MassaManager != null)
                    CurrentWeighingFact.NetWeight = _taskManager.MassaManager.WeightNet - CurrentPlu.GoodsTareWeight;
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
                CustomMessageBox messageBox = CustomMessageBox.Show(owner, Messages.WeightControl + Environment.NewLine +
                    $"Вес нетто: {CurrentWeighingFact.NetWeight} кг" + Environment.NewLine +
                    $"Номинальный вес: {CurrentPlu.NominalWeight} кг" + Environment.NewLine +
                    $"Верхнее значение веса: {CurrentPlu.UpperWeightThreshold} кг" + Environment.NewLine +
                    $"Нижнее значение веса: {CurrentPlu.LowerWeightThreshold} кг" + Environment.NewLine + Environment.NewLine +
                    "Для продолжения печати нажмите Ignore.",
                    Messages.OperationControl,
                    MessageBoxButtons.AbortRetryIgnore);
                messageBox.Wait();
                if (messageBox.Result != DialogResult.Ignore)
                    return;
            }

            // Шаблон с указанием кол-ва.
            if (template.XslContent.Contains("^PQ1"))
            {
                // Изменить кол-во этикеток.
                if (PalletSize > 1)
                    template.XslContent = template.XslContent.Replace("^PQ1", $"^PQ{PalletSize}");
                // Печать этикетки.
                PrintLabel(template);
            }
            // Шаблон без указания кол-ва.
            else
            {
                for (int i = CurrentBox; i <= PalletSize; i++)
                {
                    // Печать этикетки.
                    PrintLabel(template);
                }
            }
        }

        /// <summary>
        /// Печать весовых этикеток.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="template"></param>
        private void PrintWeightLabel(IWin32Window owner, TemplateDirect template)
        {
            // Проверка наличия устройства весов.
            if (_taskManager.MassaManager == null)
            {
                _logUtils.Information(@"Устройство весов не обнаружено!");
                return;
            }
            // Проверка товара на весах.
            if (_taskManager.MassaManager.WeightNet - CurrentPlu.GoodsTareWeight <= 0)
            {
                _logUtils.Information($@"Вес товара: {_taskManager.MassaManager.WeightNet} кг, печать этикетки невозможна!");
                return;
            }

            CurrentWeighingFact = WeighingFactDirect.New(
                CurrentScale,
                CurrentPlu,
                ProductDate,
                Kneading,
                CurrentPlu.Scale.ScaleFactor,
                _taskManager.MassaManager.WeightNet - CurrentPlu.GoodsTareWeight,
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
            // Сохранить запись в таблице [WeithingFact].
            CurrentWeighingFact.Save();

            string xmlInput = CurrentWeighingFact.SerializeObject();
            string printCmd = ZplPipeUtils.XsltTransformationPipe(template.XslContent, xmlInput, true);

            // Подменить картинки ZPL.
            PrintCmdReplacePics(ref printCmd);
            // Отправить задание в очередь печати.
            _taskManager.PrintManager.SendAsync(printCmd);
            // Сохранить ZPL-запрос в таблицу [Labels].
            PrintSaveLabel(ref printCmd, CurrentWeighingFact.Id);
        }

        #endregion
    }
}
