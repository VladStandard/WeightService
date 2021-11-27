// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableModels;
using DataShareCore.Wmi;
using System;
using System.Collections.Concurrent;
using WeightCore.Print.Tsc;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace WeightCore.Managers
{
    public class ManagerFactoryPrint : ManagerBase
    {
        #region Public and private fields and properties

        public string Peeler { get; private set; }
        public int UserLabelCount { get; private set; }
        public PrinterStatus CurrentStatus { get; private set; }
        public Connection ZebraConnection { get; private set; }
        public BlockingCollection<string> Documents { get; private set; } = new();
        private ZebraPrinter _zebraPrinter;
        public ZebraPrinter ZebraPrinter => _zebraPrinter ??= ZebraPrinterFactory.GetInstance(ZebraConnection);
        public TscPrintControlHelper TscPrintControl = TscPrintControlHelper.Instance;
        private readonly WmiHelper _wmi = WmiHelper.Instance;
        public bool IsTscPrinter { get; private set; }
        public Win32PrinterEntity Win32Printer() => _wmi.GetWin32Printer(TscPrintControl.Name);

        #endregion

        #region Public and private methods

        public void Init(bool isTscPrinter, string name, string ip, int port)
        {
            Init(
            () =>
            {
                IsTscPrinter = isTscPrinter;
                if (IsTscPrinter)
                    ZebraConnection = new TcpConnection(ip, port);
                TscPrintControl.Init(name, ip, port);
            },
            1_000, 500, 250, 2_000, 1_000);
        }

        public void Open(SqlViewModelEntity sqlViewModel)
        {
            Open(ProjectsEnums.TaskType.MemoryManager, sqlViewModel,
            () => {
                if (IsTscPrinter)
                {
                    OpenTsc();
                }
                else
                {
                    ZebraConnection?.Open();
                    OpenZebra();
                }
            },
            () => {
                //
            },
            () => {
                //
            });
        }

        public void Close()
        {
            Close(() =>
            {
                if (!IsTscPrinter)
                {
                    ZebraConnection?.Close();
                }
            });
        }

        public void Send(string printCmd)
        {
            Documents.Add(printCmd);
        }

        public void OpenZebra()
        {
            try
            {
                if (Documents.Count > 0)
                {
                    CurrentStatus = ZebraPrinter.GetCurrentStatus();
                    UserLabelCount = int.Parse(SGD.GET("odometer.user_label_count", ZebraPrinter.Connection));
                    if (CurrentStatus.isReadyToPrint)
                    {
                        Peeler = SGD.GET("sensor.peeler", ZebraPrinter.Connection);
                        if (Peeler == "clear")
                        {
                            foreach (string doc in Documents.GetConsumingEnumerable())
                            {
                                //if (Documents.TryDequeue(out string doc))
                                string docReplace = doc.Replace("|", "\\&");
                                ZebraPrinter.SendCommand(docReplace);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        public void OpenTsc()
        {
            UserLabelCount = 1;
            try
            {
                if (Documents.Count > 0)
                {
                    foreach (string doc in Documents.GetConsumingEnumerable())
                    {
                        //if (Documents.TryDequeue(out string request))
                        string docReplace = doc.Replace("|", "\\&");
                        if (!docReplace.Equals("^XA~JA^XZ") && !docReplace.Contains("odometer.user_label_count"))
                        {
                            //TscPrintControl.CmdSendCustom(docReplace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(null, ref ex, false);
            }
        }

        public void ClearPrintBuffer(bool isTscPrinter)
        {
            Documents = new BlockingCollection<string>();
            if (isTscPrinter)
            {
                //TscPrintControl.CmdClearBuffer();
            }
            else
            {
                Documents.Add("^XA~JA^XZ");
            }
        }

        public void SetOdometorUserLabel(int value) => 
            Documents.Add($"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n");

        #endregion
    }
}
