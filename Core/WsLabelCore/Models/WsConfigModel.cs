// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class WsConfigModel
{
    #region Public and private fields and properties

    public ushort WaitClose { get; }
    public ushort WaitExecute { get; }
    public const ushort WaitSleep = 0_100;
    public const ushort WaitLowLimit = 0_100;
    public const ushort WaitHighLimit = 4_000;
    public Stopwatch StopwatchExecute { get; }

    #endregion

    #region Constructor and destructor

    public WsConfigModel(ushort waitExecute = 0, ushort waitClose = 0)
    {
        WaitExecute = waitExecute == 0 ? (ushort)0_100 : waitExecute;
        WaitClose = waitClose == 0 ? (ushort)0_500 : waitClose;
        StopwatchExecute = Stopwatch.StartNew();
    }

    public WsConfigModel() : this(0) { }

    public void WaitSync(ushort miliSeconds)
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

    public void WaitSync(Stopwatch stopwatch, ushort wait)
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