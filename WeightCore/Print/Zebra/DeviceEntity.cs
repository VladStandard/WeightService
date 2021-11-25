// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using log4net;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using WeightCore.Print.Native;
using WeightCore.Zpl;

namespace WeightCore.Print.Zebra
{
    public class DeviceEntity
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public StateEntity ZebraCurrentState = new();
        public BlockingCollection<string> Requests { get; private set; } = new();

        public StatusDataCollector DataCollector { get; set; }
        public static readonly int CommandThreadTimeOut = 100;

        public Guid ID { get; private set; }
        public string Name { get; private set; }


        public DeviceEntity(Guid id, string name = "")
        {
            ID = id;
            Name = name;
            // Уведомитель состояния.
            DataCollector = new StatusDataCollector();
        }


        public void ClearZebraPrintBuffer()
        {
            // если очередь не пустая - очищаем, запускаем команду очистки очереди печати
            if (Requests.Count > 0)
                Requests = new BlockingCollection<string>();

            Requests.Add(ZplPipeUtils.ZplClearPrintBuffer());
        }

        public void SetOdometorUserLabel(int value)
        {
            Requests.Add(ZplPipeUtils.ZplSetOdometerUserLabel(value));
        }

        public void GetOdometorUserLabel()
        {
            Requests.Add(ZplPipeUtils.ZplGetOdometerUserLabel());
        }

        //public async void SendAsync(string template, string content)
        //{
        //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        //    try
        //    {
        //        Requests.Add(ZplPipeUtils.XsltTransformationPipe(template, content, true));
        //        log.Debug($"{Name} - send content:\n{content}");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Debug($"{Name}\n{ex.Message}");
        //    }

        //}

        public async void SendAsync(string content)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                Requests.Add(content);
            }
            catch (Exception ex)
            {
                log.Debug($"{Name}\n{ex.Message}");
            }
        }

        //public void CheckDeviceStatusOn()
        //{
        //    SharingSessionThread = new Thread(t =>
        //    {
        //        while (_workthread)
        //        {
        //            if (requestQueue.TryDequeue(out string request))
        //            {
        //                lock (locker)
        //                {
        //                    string msg = string.Empty;
        //                    try
        //                    {
        //                        msg = DeviceSocket.SendStringToPrinter(request);
        //                    }
        //                    // Опросили принтер и получили такой ответ.
        //                    catch (System.Net.Sockets.SocketException ex)
        //                    {
        //                        log.Error(ex.Message);
        //                    }
        //                    // Отправили на печать и получили такой ответ.
        //                    catch (System.IO.IOException)
        //                    {
        //                        //
        //                    }
        //                    // Всё остальное.
        //                    catch (Exception ex)
        //                    {
        //                        log.Error(ex.Message);
        //                        throw ex;
        //                    }
        //                    ZebraCurrentState.LoadResponse(request, msg);
        //                    NotifyStateForMainForm?.Invoke(ZebraCurrentState);
        //                    //log.Debug(msg);
        //                }
        //                Thread.Sleep(TimeSpan.FromMilliseconds(CommandThreadTimeOut));
        //            }
        //            else
        //            {
        //                Thread.Sleep(TimeSpan.FromSeconds(1));
        //            }
        //        }
        //    }
        //    )
        //    { IsBackground = true };
        //    SharingSessionThread.Start();
        //    Thread.Sleep(100);
        //}

        //public void CheckDeviceStatusOff()
        //{
        //    if (SharingSessionThread != null && SharingSessionThread.IsAlive)
        //    {
        //        //_workthread = false;
        //        Thread.Sleep(200);
        //        SharingSessionThread.Abort();
        //        SharingSessionThread.Join(1000);
        //        SharingSessionThread = null;
        //    }
        //}
    }

    public abstract class IDeviceSocket
    {
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public abstract string SendStringToPrinter(string szString);
    }

    public class DeviceSocketRaw : IDeviceSocket
    {
        private string PrinterName { get; set; }

        public DeviceSocketRaw(string _PrinterName)
        {
            PrinterName = _PrinterName;
        }

        public override string SendStringToPrinter(string szString)
        {
            string zpl = ZplPipeUtils.ToCodePoints(szString);
            RawPrinterHelper.SendStringToPrinter(PrinterName, zpl);
            return "";
        }
    }

    public class DeviceSocketTcp : IDeviceSocket
    {
        public string DeviceIP { get; private set; }
        public int DevicePort { get; private set; }

        public DeviceSocketTcp(string _DeviceIP, int _DevicePort)
        {
            DeviceIP = _DeviceIP;
            DevicePort = _DevicePort;
        }

        public override string SendStringToPrinter(string szString)
        {
            string info = ZplPipeUtils.InterplayToPrinter(DeviceIP, DevicePort, szString.Split('\n'), out string _errorMessage);
            if (_errorMessage.Length > 0)
            {
                log.Error(_errorMessage);
            }
            return info;
        }
    }
}
