// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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