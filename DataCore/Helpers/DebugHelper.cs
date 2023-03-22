// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Helpers;

public class DebugHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static DebugHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static DebugHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private methods

	public bool IsDebug =>
#if DEBUG
		true;
#else
        false;
#endif

    public Configuration Config =>
#if DEVELOPALEKSANDROV
        Configuration.DevelopAleksandrov;
#elif DEVELOPMOROZOV
        Configuration.DevelopMorozov;
#elif DEVELOPVS
        Configuration.DevelopVS;
#elif RELEASEALEKSANDROV
        Configuration.ReleaseAleksandrov;
#elif RELEASEMOROZOV
        Configuration.ReleaseMorozov;
#elif RELEASEVS
        Configuration.ReleaseVS;
#else
        Configuration.DevelopVS;
#endif

    #endregion
}