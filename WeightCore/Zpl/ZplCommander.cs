// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using WeightCore.Print.Zebra;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace WeightCore.Zpl
{
    public class ZplCommander
    {
        #region Private fields and properties

        private bool TaskExit { get; set; }
        private Connection Connection { get; set; }
        private int CommandThreadTimeOut { get; set; } = 10_000;
        private object Locker { get; set; } = new();
        private Task Task { get; set; }

        #endregion

        #region Public methods

        public ZplCommander(string address, DeviceEntity mkDeviceEntity, string cmd)
        {
            StartTask(address, mkDeviceEntity, cmd);
        }

        private string GetDescription(PrinterStatus status)
        {
            if (status.isReadyToPrint)
                return @"Готов к печати";
            if (status.isHeadCold)
                return @"Голова холодна";
            if (status.isHeadOpen)
                return @"Голова открыта";
            if (status.isHeadTooHot)
                return @"Голова слишком горячая";
            if (status.isPaperOut)
                return @"Нет бумаги";
            if (status.isPartialFormatInProgress)
                return @"Частичное форматирование в процессе";
            if (status.isPaused)
                return @"Приостановлено";
            if (status.isReceiveBufferFull)
                return @"Буфер приема заполнен";
            if (status.isRibbonOut)
                return @"Лента закончилась";
            return "Ошибка";

        }

        public void StartTask(string address, DeviceEntity mkDeviceEntity, string cmd)
        {
            if (Task != null)
                return;

            TaskExit = false;
            Task = Task.Run(async () =>
            {
                bool isFirst = true;
                while (!TaskExit)
                {
                    lock (Locker)
                    {
                        try
                        {
                            ConnnectionOpen(ref address);
                            if (Connection != null)
                            {
                                if (Connection.GetType().Name.Contains("Status"))
                                {
                                    ZebraPrinterLinkOs printer = ZebraPrinterFactory.GetLinkOsPrinter(Connection);
                                    PrinterStatus status = printer?.GetCurrentStatus();
                                    if (status != null)
                                    {
                                        // Готов к печати
                                        if (status.isReadyToPrint)
                                        {
                                        }
                                        else
                                        {
                                            mkDeviceEntity.DataCollector.Setup(status);
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("GetLinkOsPrinter error!");
                                    }
                                }
                            }
                        }
                        catch (ConnectionException)
                        {
                            throw;
                        }
                        catch (ZebraPrinterLanguageUnknownException)
                        {
                            throw;
                        }

                    }
                    
                    if (isFirst)
                    {
                        isFirst = false;
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }
                    else
                        await Task.Delay(TimeSpan.FromMilliseconds(CommandThreadTimeOut));
                }
                ConnnectionClose();
            });
        }

        public void StopTask()
        {
            TaskExit = true;
            Task?.Dispose();
            Task = null;
            ConnnectionClose();
        }

        private void ConnnectionOpen(ref string address)
        {
            if (Connection == null)
            {
                //_connection = ZebraConnectionBuilder.Build($"TCP_STATUS:{address}");
                //_connection = Zebra.Sdk.Comm.ConnectionBuilder.Build($"TCP_STATUS:{address}");
                Connection = ConnectionBuilder.Build($"TCP_STATUS:{address}");
            }
            if (Connection != null)
                if (!Connection.Connected)
                    Connection.Open();
        }

        private void ConnnectionClose()
        {
            if (Connection != null)
            {
                try
                {
                    Connection.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Connection = null;
                }
            }
        }

        public void Close()
        {
            StopTask();
            if (Task != null)
            {
                Task.Wait(TimeSpan.FromMilliseconds(500));
                Task = null;
            }
        }

        ~ZplCommander()
        {
            Close();
        }

        #endregion
    }
}
