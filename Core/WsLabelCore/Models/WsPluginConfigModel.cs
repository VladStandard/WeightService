namespace WsLabelCore.Models;

/// <summary>
/// Конфиг плагина.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsPluginConfigModel
{
    #region Public and private fields and properties

    public ushort WaitClose { get; }
    public ushort WaitExecute { get; }
    private const ushort WaitSleep = 0_100;
    public const ushort WaitLowLimit = 0_100;
    private const ushort WaitHighLimit = 3_000;
    public Stopwatch StopwatchExecute { get; }

    #endregion

    #region Constructor and destructor

    public WsPluginConfigModel(ushort waitExecute = 0, ushort waitClose = 0)
    {
        WaitExecute = waitExecute == 0 ? (ushort)0_100 : waitExecute;
        WaitClose = waitClose == 0 ? (ushort)0_500 : waitClose;
        StopwatchExecute = Stopwatch.StartNew();
    }

    public WsPluginConfigModel() : this(0) { }

    public static void WaitSync(ushort miliSeconds)
    {
        Stopwatch stopwatchSleep = Stopwatch.StartNew();
        if (miliSeconds < WaitLowLimit)
            miliSeconds = WaitLowLimit;
        if (miliSeconds > WaitHighLimit)
            miliSeconds = WaitHighLimit;
        stopwatchSleep.Restart();
        while ((ushort)stopwatchSleep.Elapsed.TotalMilliseconds < miliSeconds)
        {
            Thread.Sleep(WaitSleep);
            System.Windows.Forms.Application.DoEvents();
        }
        stopwatchSleep.Stop();
    }

    public async Task WaitAsync(ushort miliSeconds)
    {
        Stopwatch stopwatchSleep = Stopwatch.StartNew();
        if (miliSeconds < WaitLowLimit)
            miliSeconds = WaitLowLimit;
        if (miliSeconds > WaitHighLimit)
            miliSeconds = WaitHighLimit;
        stopwatchSleep.Restart();
        while ((ushort)stopwatchSleep.Elapsed.TotalMilliseconds < miliSeconds)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(WaitSleep)).ConfigureAwait(true);
            System.Windows.Forms.Application.DoEvents();
        }
        stopwatchSleep.Stop();
    }

    public static void WaitSync(Stopwatch stopwatch, ushort wait)
    {
        stopwatch.Restart();
        while ((ushort)stopwatch.Elapsed.TotalMilliseconds < wait)
        {
            Thread.Sleep(WaitSleep);
            System.Windows.Forms.Application.DoEvents();
        }
        stopwatch.Stop();
    }

    public async Task WaitAsync(Stopwatch stopwatch, ushort wait)
    {
        stopwatch.Restart();
        while ((ushort)stopwatch.Elapsed.TotalMilliseconds < wait)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(WaitSleep)).ConfigureAwait(true);
            System.Windows.Forms.Application.DoEvents();
        }
        stopwatch.Stop();
    }

    #endregion
}