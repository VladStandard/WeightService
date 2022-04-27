// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL;
using DataCore.Protocols;
using System.IO;

namespace DataCoreTests
{
    public static class TestsUtils
    {
        public static DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;


        static TestsUtils()
        {
            DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
                NetUtils.GetLocalHostName(true), nameof(DataCoreTests));
        }
    }
}
