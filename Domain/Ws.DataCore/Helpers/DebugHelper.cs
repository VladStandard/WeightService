namespace Ws.DataCore.Helpers;

public sealed class DebugHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static DebugHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static DebugHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private methods

    /// <summary>
    /// Режим работы разработка.
    /// </summary>
    public bool IsDevelop => Config switch
    {
        EnumConfiguration.DevelopVs => true,
        EnumConfiguration.ReleaseVs => false,
        _ => throw new ArgumentOutOfRangeException(nameof(IsDevelop), IsDevelop.ToString())
    };
    
    /// <summary>
    /// Режим работы релиз.
    /// </summary>
    public EnumConfiguration Config =>
#if  DEVELOPVS
        EnumConfiguration.DevelopVs;
#elif RELEASEVS
        EnumConfiguration.ReleaseVs;
#else
        EnumConfiguration.DevelopVS;
#endif

    #endregion
}