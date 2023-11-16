using Application=System.Windows.Forms.Application;

namespace Ws.LabelCore.Models;

/// <summary>
/// Конфиг плагина.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class PluginConfigModel
{
    #region Public Properties
    
    public ushort WaitExecute { get; }

    #endregion

    #region Constants
    
    private const ushort DefaultWaitExecute = 100;
    private const ushort WaitSleep = 100;
    public const ushort WaitLowLimit = 100;
    private const ushort WaitHighLimit = 5000;

    #endregion

    #region Constructors

    public PluginConfigModel(ushort waitExecute = DefaultWaitExecute)
    {
        WaitExecute = ClampToLimits(waitExecute);
    }

    public PluginConfigModel() : this(DefaultWaitExecute) { }

    #endregion

    #region Public Methods

    public static void WaitSync(ushort milliseconds)
    {
        milliseconds = ClampToLimits(milliseconds);
        Stopwatch stopwatchSleep = Stopwatch.StartNew();

        while (stopwatchSleep.Elapsed.TotalMilliseconds < milliseconds)
        {
            Thread.Sleep(WaitSleep);
            Application.DoEvents();
        }

        stopwatchSleep.Stop();
    }

    public static async Task WaitAsync(ushort milliseconds)
    {
        milliseconds = ClampToLimits(milliseconds);
        Stopwatch stopwatchSleep = Stopwatch.StartNew();

        while (stopwatchSleep.Elapsed.TotalMilliseconds < milliseconds)
        {
            await Task.Delay(WaitSleep).ConfigureAwait(true);
            Application.DoEvents();
        }

        stopwatchSleep.Stop();
    }

    #endregion

    #region Private Methods

    private static ushort ClampToLimits(ushort value)
    {
        return value switch
        {
            < WaitLowLimit => WaitLowLimit,
            > WaitHighLimit => WaitHighLimit,
            _ => value
        };
    }

    #endregion
}