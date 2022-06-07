// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using DataCore.Protocols;
using System.IO;

namespace DataCoreTests
{
    public static class TestsUtils
    {
        #region Public and private fields and properties

        public static DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        public static SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Constructor and destructor

        static TestsUtils()
        {
            DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
                NetUtils.GetLocalHostName(true), nameof(DataCoreTests));
        }

        #endregion
    }
}
