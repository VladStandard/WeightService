// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;

namespace WeightCore.MassaK;

public class MassaStableEntity
{
	public ushort WaitMilliseconds => 0_100;
	public Stopwatch StopwatchStable { get; } = new();
	private bool _isStable;
	public bool IsStable
	{
		get
		{
			if ((ushort)StopwatchStable.Elapsed.TotalMilliseconds < WaitMilliseconds)
				return false;
			return _isStable;
		}
		set
		{
			if (_isStable != value)
			{
				StopwatchStable.Restart();
				_isStable = value;
			}
		}
	}
}