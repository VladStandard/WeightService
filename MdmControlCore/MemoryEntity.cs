// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MdmControlCore;

/// <summary>
/// Task memory.
/// </summary>
public class MemoryEntity
{
	#region Public and private fields and properties

	public int SleepMiliSeconds { get; private set; }
	public int WaitCloseMiliSeconds { get; private set; }
	public MemorySizeEntity MemorySize { get; private set; }
	public string ExceptionMsg { get; private set; }
	public bool IsExecute { get; set; }

	#endregion

	#region Constructor and destructor

	public MemoryEntity(int sleepMiliSeconds, int waitCloseMiliSeconds, ulong limitBytes)
	{
		SleepMiliSeconds = sleepMiliSeconds;
		WaitCloseMiliSeconds = waitCloseMiliSeconds;
		MemorySize = new MemorySizeEntity(limitBytes);
		IsExecute = false;
	}

	#endregion

	#region Public and private methods

	public void Open(Action actionRefresh, 
		[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		IsExecute = true;
		try
		{
			while (IsExecute)
			{
				MemorySize.Physical.Bytes = (ulong)Process.GetCurrentProcess().WorkingSet64;
				MemorySize.Virtual.Bytes = (ulong)Process.GetCurrentProcess().PrivateMemorySize64;

				actionRefresh();
				Thread.Sleep(TimeSpan.FromMilliseconds(SleepMiliSeconds));
			}
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
			Console.WriteLine(ExceptionMsg);
			Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
		}
	}

	public void Close([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		try
		{
			IsExecute = false;
		}
		catch (Exception ex)
		{
			ExceptionMsg = ex.Message;
			if (!string.IsNullOrEmpty(ex.InnerException?.Message))
				ExceptionMsg += Environment.NewLine + ex.InnerException.Message;
			Console.WriteLine(ExceptionMsg);
			Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
		}
	}

	#endregion
}