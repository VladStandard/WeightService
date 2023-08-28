namespace WsWebApiCore.Utils;

public class WsServiceUtils
{
    #region Public and private fields, properties, constructor

    public static WsAppVersionHelper AppVersion => WsAppVersionHelper.Instance;
    public static WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public static WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    public static string RootDirectory => @"\\ds4tb\Dev\WebServicesLogs\";

    #endregion
}