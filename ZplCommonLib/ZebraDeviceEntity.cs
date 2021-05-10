using log4net;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ZplCommonLib;

// ReSharper disable StringLiteralTypo
// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

namespace WeightServices.Common.Zpl
{
    public class ZebraDeviceEntity
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ZebraState ZebraCurrentState = new ZebraState();


        ConcurrentQueue<string> requestQueue = new ConcurrentQueue<string>();
        Thread SharingSessionThread = null;

        public delegate void OnResponseHandler(ZebraState state);
        public event OnResponseHandler NotifyStateForMainForm;
        public StatusDataCollector DataCollector { get; set; }


        private IDeviceSocket DeviceSocket { get; }
        public static readonly int CommandThreadTimeOut = 100;
        static object locker = new object();

        public Guid ID { get; private set; }
        public string Name { get; private set; }


        public ZebraDeviceEntity(IDeviceSocket deviceSocket, Guid id, string name = "")
        {
            DeviceSocket = deviceSocket;
            ID = id;
            Name = name;
            // Уведомитель состояния.
            DataCollector = new StatusDataCollector();
            ZebraState ZebraCurrentState = new ZebraState();
        }


        public void ClearZebraPrintBuffer()
        {
            // если очередь не пустая
            // очищаем
            // запускаем команду очистки очереди печати
            while (!requestQueue.IsEmpty) {
                var msg = string.Empty;
                requestQueue.TryDequeue(out msg);
            }
            var zplContent = ZplPipeClass.ZplClearPrintBuffer();
            requestQueue.Enqueue(zplContent);
        }

        public void SetOdometorUserLabel(int value)
        {
            var zplContent = ZplPipeClass.ZplSetOdometerUserLabel(value);
            requestQueue.Enqueue(zplContent);
        }

        public void GetOdometorUserLabel()
        {
            var zplContent = ZplPipeClass.ZplGetOdometerUserLabel();
            requestQueue.Enqueue(zplContent);
        }

        public async void SendAsync(string template, string content)
        {
            //await Task.Run(() => Send(template, content));
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                var zplContent = ZplPipeClass.XsltTransformationPipe(template, content);
                requestQueue.Enqueue(zplContent);
                log.Debug($"{Name} - send content:\n{content}");
            }
            catch (Exception ex)
            {
                log.Debug($"{this.Name}\n{ex.Message}");
            }

        }

        public async void SendAsync(string content)
        {
            //await Task.Run(() => Send(content));
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
               requestQueue.Enqueue(content);
            }
            catch (Exception ex)
            {
                log.Debug($"{this.Name}\n{ex.Message}");
            }
        }

        bool _workthread = true;

        public void CheckDeviceStatusOn()
        {
            SharingSessionThread = new Thread(t =>
            {
                while (_workthread)
                {
                    if (requestQueue.TryDequeue(out var request))
                    {
                        lock (locker)
                        {
                            var msg = string.Empty;
                            try
                            {
                                msg = this.DeviceSocket.SendStringToPrinter(request);
                            }
                            // Опросили принтер и получили такой ответ.
                            catch (System.Net.Sockets.SocketException ex)
                            {
                                log.Error(ex.Message);
                            }
                            // Отправили на печать и получили такой ответ.
                            catch (System.IO.IOException)
                            {
                                //
                            }
                            // Всё остальное.
                            catch (Exception ex)
                            {
                                log.Error(ex.Message);
                                throw ex;
                            }
                            this.ZebraCurrentState.LoadResponse(request, msg);
                            NotifyStateForMainForm?.Invoke(this.ZebraCurrentState);
                            //log.Debug(msg);
                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(CommandThreadTimeOut));
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }
            }
            )
            { IsBackground = true };
            SharingSessionThread.Start();
            Thread.Sleep(100);
        }

        public void CheckDeviceStatusOff()
        {
            if (SharingSessionThread != null && SharingSessionThread.IsAlive)
            {
                _workthread = false;
                Thread.Sleep(200);
                SharingSessionThread.Abort();
                SharingSessionThread.Join(1000);
                SharingSessionThread = null;
            }
        }
    }

    public abstract class IDeviceSocket
    {
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public abstract string SendStringToPrinter(string szString);
        //public abstract bool SendBytesToPrinter( IntPtr pBytes, Int32 dwCount);
        //public abstract bool SendFileToPrinter( string szFileName);
    }

    public class DeviceSocketRaw : IDeviceSocket
    {

        private string PrinterName { get; set; }

        public DeviceSocketRaw(string _PrinterName)
        {
            this.PrinterName = _PrinterName;
        }
        public override string SendStringToPrinter(string szString)
        {
            string zpl = ZplPipeClass.ToCodePoints(szString);
            RawPrinterHelper.SendStringToPrinter(PrinterName, zpl);
            return "";
        }

        //public override bool SendBytesToPrinter( IntPtr pBytes, Int32 dwCount)
        //{
        //    return true;
        //}
        //public override bool SendFileToPrinter( string szFileName)
        //{

        //    return true;
        //}

    }

    public class DeviceSocketTcp : IDeviceSocket
    {
        public string DeviceIP { get; private set; }
        public int DevicePort { get; private set; }

        public DeviceSocketTcp(string _DeviceIP, int _DevicePort)
        {
            this.DeviceIP = _DeviceIP;
            this.DevicePort = _DevicePort;
        }

        public override string SendStringToPrinter(string szString)
        {
            string _errorMessage = String.Empty;
            string info = ZplPipeClass.InterplayToPrinter(this.DeviceIP, this.DevicePort, szString.Split('\n'), out _errorMessage);
            if (_errorMessage.Length > 0)
            {
                log.Error(_errorMessage);
            }
            return info;
        }
        //public override bool SendBytesToPrinter(IntPtr pBytes, Int32 dwCount)
        //{

        //    return true;
        //}
        //public override bool SendFileToPrinter(string szFileName)
        //{

        //    return true;
        //}

    }
}
