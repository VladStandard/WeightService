namespace WsDataCore.Helpers;

public sealed class WsProcHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static WsProcHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static WsProcHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public methods

	public void RunSilent(string procName, string args, bool runAs)
	{
		Run(procName, args, runAs, ProcessWindowStyle.Hidden, true);
	}

	public void Run(string procName, string args, bool runAs, ProcessWindowStyle windowStyle, bool useShellExecute)
	{
		Process process = new()
		{
			StartInfo = new(procName, args)
			{
				Verb = runAs ? "runas" : "",
				WindowStyle = windowStyle,
				UseShellExecute = useShellExecute
            }
        };
		process.Start();
		process.WaitForExit();
	}

	#endregion
}