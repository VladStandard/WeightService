namespace Ws.StorageCore.Helpers;

[Obsolete("Will be deleted soon")]
public sealed class SqlContextManagerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlContextManagerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlContextManagerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    
    #endregion

    #region Public and private methods
    

    #endregion
}