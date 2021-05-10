// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using log4net;
using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using ZabbixAgentLib;
// ReSharper disable IdentifierTypo

namespace ZabbixStubService
{
    public partial class ZabbixStubService : ServiceBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
        [DllImport("advapi32.dll", SetLastError = true)]

        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        CancellationTokenSource cancelTokenSource;
        CancellationToken token;
        FakeCheckThreadByLog fakeCheckThreadByLog = null;

        CancellationTokenSource cancelTokenSourceHttpListener;
        CancellationToken tokenHttpListener;
        ZabbixHttpListener zabbixHttpListener = null;
        HealthDataCollectorDummy healthDataCollector = null;

        public ZabbixStubService()
        {
            InitializeComponent();

            try
            {
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }


        }

        protected override void OnStart(string[] args)
        {

            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 60000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            log.Info(String.Format("ServiceState.SERVICE_START_PENDING"));

            log.Info(String.Format("OnStart."));
            base.OnStart(args);

            try
            {
                log.Info(String.Format("new HealthDataCollectorDummy()"));
                healthDataCollector = new HealthDataCollectorDummy();
                healthDataCollector.LoadValues();


                log.Info(String.Format("new FakeCheckThreadByLog"));
                cancelTokenSource = new CancellationTokenSource();
                token = cancelTokenSource.Token;

                fakeCheckThreadByLog = new FakeCheckThreadByLog(healthDataCollector.LoadValues, token, 2500);
                fakeCheckThreadByLog.Start();



                log.Info(String.Format("new ZabbixHttpListener"));
                cancelTokenSourceHttpListener = new CancellationTokenSource();
                tokenHttpListener = cancelTokenSource.Token;

                zabbixHttpListener = new ZabbixHttpListener(healthDataCollector.ResponseBuilderFunc, tokenHttpListener, 10);

                log.Info("fakeCheckThreadByLog.StartED.");
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }


            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            log.Info(String.Format("ServiceState.SERVICE_RUNNING"));

        }

        protected override void OnStop()
        {

            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
            serviceStatus.dwWaitHint = 60000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            log.Info("ServiceState.SERVICE_STOP_PENDING");


            log.Info("fakeCheckThreadByLog.Stop()");
            try
            {
                token.ThrowIfCancellationRequested();
                tokenHttpListener.ThrowIfCancellationRequested();

                //fakeCheckThreadByLog.Stop();
                //zabbixHttpListener.Stop();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }


            base.OnStop();

            log.Info("In OnStop.");

            // Update the service state to Stopped.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            log.Info("ServiceState.SERVICE_STOPPED");
        }
    }
}
