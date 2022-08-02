// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using DataCore.Protocols;
using System.IO;

namespace DataCoreTests;

public static class TestsUtils
{
    #region Public and private fields, properties, constructor

    public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;

    static TestsUtils()
    {
        DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
            NetUtils.GetLocalHostName(true), nameof(DataCoreTests));
    }

    #endregion
}
