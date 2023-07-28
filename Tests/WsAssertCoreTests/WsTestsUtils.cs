namespace WsAssertCoreTests;

public static class WsTestsUtils
{
    public static WsDataTestsHelper DataTests => WsDataTestsHelper.Instance;
    public static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
}