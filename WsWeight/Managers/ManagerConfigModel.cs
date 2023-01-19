// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.Managers;

public class ManagerConfigModel
{
	#region Public and private fields and properties

	public ushort WaitClose { get; }
	public ushort WaitException { get; }
	public ushort WaitReopen { get; }
	public ushort WaitRequest { get; }
	public ushort WaitResponse { get; }
    public const ushort WaitSleep = 0_050;
    public const ushort WaitLowLimit = 0_050;
    public const ushort WaitHighLimit = 5_000;
	public Stopwatch StopwatchReopen { get; }
	public Stopwatch StopwatchRequest { get; }
	public Stopwatch StopwatchResponse { get; }

	#endregion

	#region Constructor and destructor

	public ManagerConfigModel(ushort waitReopen, ushort waitRequest, ushort waitResponse, ushort waitClose, ushort waitException)
	{
		WaitReopen = waitReopen == 0 ? (ushort)1_000 : waitReopen;
		WaitRequest = waitRequest == 0 ? (ushort)250 : waitRequest;
		WaitResponse = waitResponse == 0 ? (ushort)500 : waitResponse;
		WaitClose = waitClose == 0 ? (ushort)0_200 : waitClose;
		WaitException = waitException == 0 ? (ushort)1_000 : waitException;
		StopwatchReopen = Stopwatch.StartNew();
		StopwatchRequest = Stopwatch.StartNew();
		StopwatchResponse = Stopwatch.StartNew();
	}

	public ManagerConfigModel() : this(1_000, 0_250, 0_250, 1_000, 1_000) { }

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

	public void WaitSync(Stopwatch? stopwatch, ushort wait)
	{
		if (stopwatch is null)
			return;

		stopwatch.Restart();
		while ((ushort)stopwatch.Elapsed.TotalMilliseconds < wait)
		{
			Thread.Sleep(WaitSleep);
            System.Windows.Forms.Application.DoEvents();
        }
		stopwatch.Stop();
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

	#endregion
}