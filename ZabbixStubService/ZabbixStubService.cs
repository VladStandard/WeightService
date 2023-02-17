// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using ZabbixStubService.Zabbix;
// ReSharper disable IdentifierTypo

namespace ZabbixStubService;

public partial class ZabbixStubService : ServiceBase
{
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
    private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

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
        catch (Exception)
        {
            throw;
        }
    }

    protected override void OnStart(string[] args)
    {

        ServiceStatus serviceStatus = new();
        serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
        serviceStatus.dwWaitHint = 60000;
        SetServiceStatus(ServiceHandle, ref serviceStatus);

        base.OnStart(args);

        try
        {
            healthDataCollector = new HealthDataCollectorDummy();
            healthDataCollector.LoadValues();


            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;

            fakeCheckThreadByLog = new FakeCheckThreadByLog(healthDataCollector.LoadValues, token, 2500);
            fakeCheckThreadByLog.Start();

            cancelTokenSourceHttpListener = new CancellationTokenSource();
            tokenHttpListener = cancelTokenSource.Token;

            //zabbixHttpListener = new ZabbixHttpListener(healthDataCollector.ResponseBuilderFunc, tokenHttpListener, 10);
            zabbixHttpListener = new ZabbixHttpListener();
        }
        catch (Exception)
        {
            throw;
        }

        // Update the service state to Running.
        serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
        SetServiceStatus(ServiceHandle, ref serviceStatus);
    }

    protected override void OnStop()
    {

        ServiceStatus serviceStatus = new();
        serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
        serviceStatus.dwWaitHint = 60000;
        SetServiceStatus(ServiceHandle, ref serviceStatus);

        try
        {
            token.ThrowIfCancellationRequested();
            tokenHttpListener.ThrowIfCancellationRequested();

            //fakeCheckThreadByLog.Stop();
            //zabbixHttpListener.Stop();
        }
        catch (Exception)
        {
            throw;
        }


        base.OnStop();

        // Update the service state to Stopped.
        serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
        SetServiceStatus(ServiceHandle, ref serviceStatus);
    }
}