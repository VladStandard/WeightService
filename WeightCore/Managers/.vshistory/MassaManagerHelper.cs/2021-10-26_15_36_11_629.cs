// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Helpers;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WeightCore.MassaK;

namespace WeightCore.Managers
{
    public class MassaManagerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static MassaManagerHelper _instance;
        public static MassaManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties - Manager

        public int WaitWhileMiliSeconds { get; private set; }
        public int WaitExceptionMiliSeconds { get; private set; }
        public int WaitCloseMiliSeconds { get; private set; }
        public string ExceptionMsg { get; private set; }
        public delegate void Callback();
        public bool IsExecute { get; set; }

        #endregion

        #region Public fields and properties

        public DeviceSocketEntity DeviceSocket { get; private set; }
        public static readonly int CommandThreadTimeOut = 100;
        public decimal WeightNet { get; private set; }
        public decimal WeightGross { get; private set; }
        public byte IsStable { get; private set; }
        public bool IsReady { get; private set; }
        public int ScaleFactor { get; set; } = 1000;
        public int CurrentError { get; private set; }
        public AskScalePar DeviceParameters { get; private set; }
        public AskError DeviceError { get; private set; }
        private static readonly object Locker = new();
        private readonly ConcurrentQueue<CmdEntity> _requestQueue = new();
        private readonly LogHelper _log = LogHelper.Instance;

        #endregion

        #region Constructor and destructor

        public void Init(DeviceSocketEntity deviceSocket, int waitWhileMiliSeconds, int waitExceptionMiliSeconds, int waitCloseMiliSeconds)
        {
            WaitWhileMiliSeconds = waitWhileMiliSeconds;
            WaitExceptionMiliSeconds = waitExceptionMiliSeconds;
            WaitCloseMiliSeconds = waitCloseMiliSeconds;
            IsExecute = false;
            DeviceSocket = deviceSocket;
        }

        #endregion

        #region Public and private methods - Manager

        public void Open(Callback callback, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IsExecute = true;
            while (IsExecute)
            {
                try
                {
                    if (WaitWhileMiliSeconds == 0)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(1_000));
                        continue;
                    }
                    OpenJob();
                    callback();
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                }
                catch (TaskCanceledException)
                {
                    // Console.WriteLine(tcex.Message);
                    // Not the problem.
                }
                catch (Exception ex)
                {
                    ExceptionMsg = ex.Message;
                    if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                        ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                    _log.Error(ExceptionMsg, filePath, memberName, lineNumber);
                    Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
                    throw;
                }
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void Close([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                IsExecute = false;
                //Thread.Sleep(TimeSpan.FromMilliseconds(WaitWhileMiliSeconds));
                CloseJob();
            }
            catch (Exception ex)
            {
                ExceptionMsg = ex.Message;
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
                Console.WriteLine(ExceptionMsg);
                Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
                //Thread.Sleep(TimeSpan.FromMilliseconds(WaitExceptionMiliSeconds));
            }
        }

        #endregion

        #region Public and private methods

        private void AddRequest(CmdEntity request)
        {
            _requestQueue.Enqueue(request);
        }

        public void ParseRequest([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (DeviceSocket == null)
                return;
            if (_requestQueue.TryDequeue(out CmdEntity request))
            {
                lock (Locker)
                {
                    IsReady = false;
                    //var state = EnumControlState.Up;
                    byte[] response = DeviceSocket.Bytes(request.BuildCmd());
                    AskEntity ask = AskFactory.GetAsk(response);
                    if (ask == null)
                    {
                        //state = EnumControlState.Down;
                        _log.Error("Нет ответа");
                    }
                    else
                    {
                        //var scaleFactor = 0;
                        decimal weightTare = 0M;
                        if (request.GetType() == typeof(CmdGetMassa))
                        {
                            switch (ask)
                            {
                                case AskGetMassa askGetMassa:
                                    //byte Division
                                    //1 байт
                                    //Цена деления в значении массы нетто и массы тары:
                                    // 0 – 100 мг, 1 – 1 г, 2 – 10 г, 3 – 100 г, 4 – 1 кг
                                    ScaleFactor = askGetMassa.ScaleFactor;

                                    //Int32 Weight
                                    //4 байта
                                    //Текущая масса нетто со знаком
                                    WeightNet = askGetMassa.Weight / (decimal)ScaleFactor;

                                    //Int32 Tare
                                    //4 байта
                                    //* Текущая масса тары со знаком
                                    weightTare = askGetMassa.Tare / (decimal)ScaleFactor;

                                    //Int32 Tare
                                    //4 байта
                                    //* Текущая масса тары со знаком
                                    WeightGross = WeightNet + weightTare;

                                    //byte Stable
                                    //1 байт
                                    //Признак стабилизации массы: 0 – нестабильна,
                                    //1 – стабильна
                                    IsStable = askGetMassa.Stable;

                                    //byte Net
                                    //1 байт
                                    //Признак индикации<NET>: 0 – нет индикации,
                                    //1 – есть индикация
                                    //... = x.Net;

                                    //byte Zero
                                    //1 байт
                                    //Признак индикации > 0 < : 0 – нет индикации,
                                    //1 – есть индикация
                                    //... = x.Zero;
                                    IsReady = true;
                                    break;
                                case AskError askError:
                                    DeviceError = askError;
                                    _log.Error(askError.GetMessage());
                                    // state = EnumControlState.Down;
                                    break;
                            }
                        }
                        else if (request.GetType() == typeof(CmdSetZero))
                        {
                            switch (ask)
                            {
                                case AskSetZero askSetZero:
                                    _log.Information(askSetZero.GetMessage());
                                    IsReady = true;
                                    break;
                                case AskError askError:
                                    DeviceError = askError;
                                    _log.Error(askError.GetMessage(), filePath, memberName, lineNumber);
                                    _log.Error(askError.ErrorCode.ToString(), filePath, memberName, lineNumber);
                                    _log.Error(askError.Command.ToString(), filePath, memberName, lineNumber);
                                    _log.Error(askError.Data.ToString(), filePath, memberName, lineNumber);
                                    _log.Error(askError.IsValid.ToString(), filePath, memberName, lineNumber);
                                    //state = EnumControlState.Down;
                                    break;
                            }
                        }
                        else if (request.GetType() == typeof(CmdSetTare))
                        {
                            switch (ask)
                            {
                                case AskSetTare askSetTare:
                                    //weightTare = ((CmdSetTare) request).WeightTare;
                                    //scaleFactor = ((CmdSetTare) request).ScaleFactor;
                                    _log.Information(askSetTare.GetMessage());
                                    IsReady = true;
                                    break;
                                case AskNackTare askNackTare:
                                    _log.Information(askNackTare.GetMessage());
                                    IsReady = true;
                                    break;
                                case AskError askError:
                                    DeviceError = askError;
                                    _log.Information(askError.GetMessage());
                                    // state = EnumControlState.Down;
                                    break;
                            }
                        }
                        else if (request.GetType() == typeof(CmdGetScalePar))
                        {
                            switch (ask)
                            {
                                case AskScalePar askScalePar:
                                    IsReady = true;
                                    DeviceParameters = askScalePar;
                                    //_logUtils.Information(ask.GetMessage());
                                    break;
                                case AskError askError:
                                    //state = EnumControlState.Down;
                                    DeviceError = askError;
                                    _log.Error(askError.GetMessage());
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void OpenJob()
        {
            AddRequest(new CmdGetMassa());
            ParseRequest();
        }

        public void CloseJob()
        {
            //
        }

        public void SetZero() => AddRequest(new CmdSetZero());

        public void SetTareWeight(int weightTare) => AddRequest(new CmdSetTare() { ScaleFactor = 1000, WeightTare = weightTare });

        public void GetScalePar() => AddRequest(new CmdGetScalePar());

        #endregion
    }
}
