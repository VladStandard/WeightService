// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL;
using DataCore.DAL.Models;
using DataCore.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace DataCoreTests
{
    /// <summary>
    /// Utilites.
    /// </summary>
    public static class TestsUtils
    {
        #region Public and private fields and properties

        public static bool IsPrepare { get; private set; } = false;
        public static DataAccessEntity DataAccess { get; private set; } = null;
        public static SqlConnection SqlCon { get; private set; }

        #endregion

        #region Public and private methods

        public static void SqlPrepare()
        {
            if (IsPrepare) return;

            //IConfiguration config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .AddEnvironmentVariables()
            //    .Build();
            //JsonSettingsEntity jsonSettings = new(config);
            
            //JsonSettingsConfigEntity jsonSettings = new("");
            //DataAccess = new(jsonSettings);
            
            //SqlCon = SqlConnectFactory.GetConnection(DataAccess.GetSession().Connection.ConnectionString);

            IsPrepare = true;
        }

        public static void MethodStart([CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{memberName} start.");
            TestContext.WriteLine();
            
            SqlPrepare();
        }

        public static void MethodComplete([CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine();
            TestContext.WriteLine($@"{memberName} complete.");
        }

        #endregion
    }
}
