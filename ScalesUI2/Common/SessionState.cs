using EntitiesLib;
using ScalesUI.Forms;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using WeightServices.Common;
using WeightServices.Common.MK;
using WeightServices.Entities;
using ZabbixAgentLib;
using ZplCommonLib;
using ZplCommonLib.Zebra;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace ScalesUI.Common
{
    public enum Direction { forward, back }

    public class SessionState
    {
        #region Design pattern "Lazy Singleton"

        private static readonly Lazy<SessionState> _instance = new Lazy<SessionState>(() => new SessionState());
        public static SessionState Instance => _instance.Value;

        private SessionState()
        {

            var x = SqlConnectFactory.GetConnection(Properties.Settings.Default.ConnectionString);
            ProductDate = DateTime.Now;

            //тут загружается ID моноблока из файла токена,
            //а затем загружается сама линия
            //--->
            Host = new HostEntity();
            Host.TokenRead();
            CurrentScale = new ScaleEntity( Host.CurrentScaleId);
            CurrentScale.Load();

            //this.CurrentScaleId = Properties.Settings.Default.CurrentScaleId;
            //this.CurrentScale = new ScaleEntity(this.CurrentScaleId);
            //<---

            // Запустить http-прослушиватель.
            StartHttpListener();

            Kneading = KneadingMinValue;
            ProductDate = DateTime.Now;
            CurrentBox = 1;
            PalletSize = 60;

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
            // ZplCommander = new ZplCommander(zplDeviceSocket.DeviceIP, zebraDeviceEntity, ZplPipeClass.ZplHostQuery());

            try
            {
                ZebraDeviceEntity = new PrintSdk(CurrentScale.ZebraPrinter.Ip, CurrentScale.ZebraPrinter.Port, 120);
                ZebraDeviceEntity.Open(CurrentScale.ZebraPrinter.PrinterType);
            }
            catch (Exception ex)
            {
                if (CustomMessageBox.Show($"Печатающее устройство недоступно ({CurrentScale.ZebraPrinter}). {ex.Message}") == DialogResult.OK)
                {

                }
                throw new Exception(ex.Message);
            }


            // тут создается устройство работы с MassaK
            // запускаем поток, который разбирает очередь команд
            // т.к. команды пишутся не напрямую, а в очередь
            // а из нее потом доотправляются на устройство
            DeviceSocketRs232 deviceSocketRs232 = new DeviceSocketRs232(CurrentScale.DeviceComPort);
            MkDevice = new MkDeviceEntity(deviceSocketRs232);
            MkDevice.SetZero();

            // тут запускается процесс отправляющий комманды
            // для получения с устройства текущего веса
            MkCommander mkCommander = new MkCommander(MkDevice);

            // начинается новыя серия
            // упаковки продукции 
            // новая паллета, если хотите
            ProductSeries = new ProductSeriesEntity(CurrentScale);
            ProductSeries.New();

        }

        ~SessionState()
        {
            StopHttpListener();
            ZplCommander.Close();
        }

        #endregion

        #region Public and private fields and properties

        private readonly LogHelper _log = LogHelper.Instance;
        public string AppVersion => UtilsAppVersion.GetMainFormText(Assembly.GetExecutingAssembly());

        public ProductSeriesEntity ProductSeries { get; private set; }

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

        public HostEntity Host { get; private set; }

        public ZplCommander ZplCommander { get; private set; }

        public PrintSdk ZebraDeviceEntity { get; }

        public MkDeviceEntity MkDevice { get; }

        public ZabbixHttpListener HttpListener { get; private set; }
        private CancellationToken _token;
        private CancellationToken _tokenHttpListener;
        private ThreadChecker _threadChecker;
        public int CurrentScaleId { get; private set; }

        public OrderEntity CurrentOrder { get; set; }

        [XmlElement(IsNullable = true)]
        public ScaleEntity CurrentScale { get; set; }

        [XmlElement(IsNullable = true)]
        public WeighingFactEntity CurrentWeighingFact { get; set; }
        
        #endregion

        #region PalletSize
        public static readonly int PalletSizeMinValue = 1;
        public static readonly int PalletSizeMaxValue = 130;

        public delegate void OnResponseHandlerPalletSize(int palletSize);
        public event OnResponseHandlerPalletSize NotifyPalletSize;

        private int _palletSize;
        public int PalletSize { 
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
            if (direction == Direction.back)
            {
                PalletSize--;
                if (PalletSize < PalletSizeMinValue)
                    PalletSize = PalletSizeMinValue;

            }
            if (direction == Direction.forward)
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
        public int CurrentBox { 
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
            if (ZebraDeviceEntity != null)
            {
                ZebraDeviceEntity.ClearZebraPrintBuffer();
                ZebraDeviceEntity.SetOdometorUserLabel(1);
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
                //если замес изменился - чистим очередь печати
                if (ZebraDeviceEntity != null)
                {
                    ZebraDeviceEntity.ClearZebraPrintBuffer();
                    ZebraDeviceEntity.SetOdometorUserLabel(CurrentBox);
                }
                _kneading = value;
                NotifyKneading?.Invoke(value);
            }
        }

        public void RotateKneading(Direction direction)
        {
            if (direction == Direction.back)
            {
                Kneading--;
                if (Kneading < KneadingMinValue)
                    Kneading = KneadingMinValue;

            }
            if (direction == Direction.forward)
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
                if (ZebraDeviceEntity != null)
                    ZebraDeviceEntity.ClearZebraPrintBuffer();
                _productDate = value;
                NotifyProductDate?.Invoke(value);
            }
        }

        public void RotateProductDate(Direction direction)
        {
            if (direction == Direction.back)
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue)
                    ProductDate = ProductDateMinValue;

            }
            if (direction == Direction.forward)
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue)
                    ProductDate = ProductDateMaxValue;
            }
        }
        #endregion

        #region PluEntity
        public delegate void OnResponseHandlerPLU(PluEntity plu);
        public event OnResponseHandlerPLU NotifyPLU;
        public PluEntity _currentPLU;
        [XmlElement(IsNullable = true)]
        public PluEntity CurrentPLU
        {
            get => _currentPLU;
            set
            {
                //если ПЛУ изменился - чистим очередь печати
                if (ZebraDeviceEntity != null)
                    ZebraDeviceEntity.ClearZebraPrintBuffer();
                    ZebraDeviceEntity.SetOdometorUserLabel(1);
                _currentPLU = value;
                CurrentBox = 1;
                NotifyPLU?.Invoke(value);
            }
        }

        #endregion

        #region PrintMethods

        public void ProcessWeighingResult()
        {
            CurrentWeighingFact = null;
            TemplateEntity template = null;
            if (CurrentOrder != null && CurrentScale != null && CurrentScale.UseOrder)
            {
                template = CurrentOrder.Template;
                CurrentOrder.FactBoxCount++;
            }
            else if (CurrentPLU != null && CurrentScale != null && !CurrentScale.UseOrder)
            {
                template = CurrentPLU.Template;
            }

            if (template != null && CurrentPLU != null)
            {
                if (CurrentPLU.CheckWeight == false)
                {
                    // если печатать надо МНОГО!!! маленьких этикеток 
                    // и при этом правильный вес не нужен
                    PrintCheckWeightWithout(template);
                }
                else if (CurrentPLU.CheckWeight == true)
                {
                    // если необходимо опрашивать платформу 
                    // для КАЖДОЙ!!! коробки отдельно
                    // и при этом получать правильный вес
                    PrintWithCheckWeight(template);
                }
            }
        }

        private void PrintCheckWeightWithout(TemplateEntity template)
        {
            // Вывести серию этикеток по заданному размеру паллеты.
            for (var i = CurrentBox; i <= PalletSize; i++)
            {
                CurrentWeighingFact = WeighingFactEntity.New(
                    CurrentScale,
                    CurrentPLU,
                    ProductDate,
                    Kneading,
                    CurrentPLU.Scale.ScaleFactor,
                    CurrentPLU.NominalWeight,
                    CurrentPLU.GoodsTareWeight
                );

                CurrentWeighingFact.Save();
                var xmlInput = CurrentWeighingFact.SerializeObject();

                var zplContent = ZplPipeClass.XsltTransformationPipe(template.XslContent, xmlInput);
                ZebraDeviceEntity.SendAsync(zplContent);
                var zplLabel = new ZplLabel
                {
                    WeighingFactId = CurrentWeighingFact.Id,
                    Content = zplContent
                };
                zplLabel.Save();
            }
        }

        private void PrintWithCheckWeight(TemplateEntity template)
        {
            if (MkDevice != null)
            {
                // на платформе нет товара
                if (MkDevice.WeightNet - CurrentPLU.GoodsTareWeight <= 0)
                {
                    _log.Info($@"Вес товара: {MkDevice.WeightNet} кг. Печать этикетки невозможна.");
                    return;
                }

                CurrentWeighingFact = WeighingFactEntity.New(
                    CurrentScale,
                    CurrentPLU,
                    ProductDate,
                    Kneading,
                    CurrentPLU.Scale.ScaleFactor,
                    MkDevice.WeightNet - CurrentPLU.GoodsTareWeight,
                    CurrentPLU.GoodsTareWeight
                );

                CurrentWeighingFact.Save();
                var xmlInput = CurrentWeighingFact.SerializeObject();

                //_ws.zebraDeviceEntity.SendAsync(template.XslContent, xmlInput);
                // заменил один вызов на другой
                // хочу сохранять полученный  ZPL в таблицу Labels
                var zplContent = ZplPipeClass.XsltTransformationPipe(template.XslContent, xmlInput);
                ZebraDeviceEntity.SendAsync(zplContent);

                var zplLabel = new ZplLabel
                {
                    WeighingFactId = CurrentWeighingFact.Id,
                    Content = zplContent
                };
                zplLabel.Save();
            }
        }

        #endregion

        #region Public and private methods - Http listener

        private void StartHttpListener()
        {
            _log.Info("Запистить http-listener. начало.");
            _log.Info("http://localhost:18086/status");
            try
            {
                var cancelTokenSource = new CancellationTokenSource();
                _token = cancelTokenSource.Token;
                _threadChecker = new ThreadChecker(_token, 2_500);
                // Подписка на событие.
                //_threadChecker.EventReloadValues += EventHttpListenerReloadValues;
                _tokenHttpListener = cancelTokenSource.Token;
                HttpListener = new ZabbixHttpListener(_tokenHttpListener, 10);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            _log.Info("Запистить http-listener. Финиш.");
        }


        private void StopHttpListener()
        {
            _log.Info("Остановить http-listener. Начало.");
            try
            {
                HttpListener?.Stop();
                _token.ThrowIfCancellationRequested();
                _tokenHttpListener.ThrowIfCancellationRequested();
                _threadChecker.Stop();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            _log.Info("Остановить http-listener. Финиш.");
        }

        #endregion
    }
}
