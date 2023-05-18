// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;

namespace WsDataCore.Helpers;

public class DebugHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static DebugHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static DebugHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private methods

    public bool IsDevelop => Config switch
    {
        WsEnumConfiguration.DevelopAleksandrov => true,
        WsEnumConfiguration.DevelopMorozov => true,
        WsEnumConfiguration.DevelopVS => true,
        WsEnumConfiguration.ReleaseAleksandrov => false,
        WsEnumConfiguration.ReleaseMorozov => false,
        WsEnumConfiguration.ReleaseVS => false,
        _ => throw new ArgumentOutOfRangeException(nameof(IsDevelop), IsDevelop.ToString())
    };

    public bool IsRelease => !IsDevelop;

    public WsEnumConfiguration Config =>
#if DEVELOPALEKSANDROV
        WsEnumConfiguration.DevelopAleksandrov;
#elif DEVELOPMOROZOV
        WsEnumConfiguration.DevelopMorozov;
#elif DEVELOPVS
        WsEnumConfiguration.DevelopVS;
#elif RELEASEALEKSANDROV
        WsEnumConfiguration.ReleaseAleksandrov;
#elif RELEASEMOROZOV
        WsEnumConfiguration.ReleaseMorozov;
#elif RELEASEVS
        WsEnumConfiguration.ReleaseVS;
#else
        WsEnumConfiguration.DevelopVS;
#endif

    #endregion
}