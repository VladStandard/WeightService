//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Collections.Concurrent;
//using System.Threading.Tasks;
//using WeightCore.Zpl;

namespace MDSoft.BarcodePrintUtils.Zebra;

//    public class DeviceEntity
//    {
//        public StateEntity ZebraCurrentState = new();
//        public BlockingCollection<string> Requests { get; private set; } = new();

//        public StatusDataCollector DataCollector { get; }
//        public static readonly int CommandThreadTimeOut = 100;

//        public Guid ID { get; }
//        public string Name { get; }


//        public DeviceEntity(Guid id, string name = "")
//        {
//            ID = id;
//            Name = name;
//            // Уведомитель состояния.
//            DataCollector = new StatusDataCollector();
//        }


//        public void ClearZebraPrintBuffer()
//        {
//            // если очередь не пустая - очищаем, запускаем команду очистки очереди печати
//            if (Requests.Count > 0)
//                Requests = new BlockingCollection<string>();

//            Requests.Add(ZplUtils.ZplClearPrintBuffer);
//        }

//        public void SetOdometorUserLabel(int value)
//        {
//            Requests.Add(ZplUtils.ZplSetOdometerUserLabel(value));
//        }

//        public void GetOdometorUserLabel()
//        {
//            Requests.Add(ZplUtils.ZplGetOdometerUserLabel());
//        }

//        public async void SendAsync(string content)
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            Requests.Add(content);
//        }

//        //public void CheckDeviceStatusOn()
//        //{
//        //    SharingSessionThread = new Thread(t =>
//        //    {
//        //        while (_workthread)
//        //        {
//        //            if (requestQueue.TryDequeue(out string request))
//        //            {
//        //                lock (_locker)
//        //                {
//        //                    string msg = string.Empty;
//        //                    try
//        //                    {
//        //                        msg = DeviceSocket.SendStringToPrinter(request);
//        //                    }
//        //                    // Опросили принтер и получили такой ответ.
//        //                    catch (System.Net.Sockets.SocketException ex)
//        //                    {
//        //                        log.Error(ex.Message);
//        //                    }
//        //                    // Отправили на печать и получили такой ответ.
//        //                    catch (System.IO.IOException)
//        //                    {
//        //                        //
//        //                    }
//        //                    // Всё остальное.
//        //                    catch (Exception ex)
//        //                    {
//        //                        log.Error(ex.Message);
//        //                        throw ex;
//        //                    }
//        //                    ZebraCurrentState.LoadResponse(request, msg);
//        //                    NotifyStateForMainForm?.Invoke(ZebraCurrentState);
//        //                    //log.Debug(msg);
//        //                }
//        //                Thread.Sleep(TimeSpan.FromMilliseconds(CommandThreadTimeOut));
//        //            }
//        //            else
//        //            {
//        //                Thread.Sleep(TimeSpan.FromSeconds(1));
//        //            }
//        //        }
//        //    }
//        //    )
//        //    { IsBackground = true };
//        //    SharingSessionThread.Start();
//        //    Thread.Sleep(100);
//        //}

//        //public void CheckDeviceStatusOff()
//        //{
//        //    if (SharingSessionThread != null && SharingSessionThread.IsAlive)
//        //    {
//        //        //_workthread = false;
//        //        Thread.Sleep(200);
//        //        SharingSessionThread.Abort();
//        //        SharingSessionThread.Join(1000);
//        //        SharingSessionThread = null;
//        //    }
//        //}
//    }