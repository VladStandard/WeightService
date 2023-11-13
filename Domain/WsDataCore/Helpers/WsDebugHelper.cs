namespace WsDataCore.Helpers;

public sealed class WsDebugHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static WsDebugHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static WsDebugHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private methods

    /// <summary>
    /// Режим работы разработка.
    /// </summary>
    public bool IsDevelop => Config switch
    {
        WsEnumConfiguration.DevelopVs => true,
        WsEnumConfiguration.ReleaseVs => false,
        _ => throw new ArgumentOutOfRangeException(nameof(IsDevelop), IsDevelop.ToString())
    };
    
    /// <summary>
    /// Режим работы релиз.
    /// </summary>
    public WsEnumConfiguration Config =>
#if  DEVELOPVS
        WsEnumConfiguration.DevelopVs;
#elif RELEASEVS
        WsEnumConfiguration.ReleaseVs;
#else
        WsEnumConfiguration.DevelopVS;
#endif

    #endregion
}