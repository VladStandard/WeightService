// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsAssertCoreTests;

public static class WsTestsUtils
{
    public static WsDataTestsHelper DataTests => WsDataTestsHelper.Instance;
    public static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
}