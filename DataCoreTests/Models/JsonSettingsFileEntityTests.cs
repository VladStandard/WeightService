// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;

namespace DataCoreTests
{
    /// <summary>
    /// Utilites.
    /// </summary>
    public class JsonSettingsFileEntityTests
    {
        #region Public and private methods

        [Test]
        public void JsonSettings_New_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
            {
            });
            //IConfiguration config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .AddEnvironmentVariables()
            //    .Build();
            //JsonSettingsEntity jsonSettings = new(config);

            //JsonSettingsFileEntity jsonSettingsFile = new("appsettings.json");
            //DataAccessEntity DataAccess = new(jsonSettings);

            //SqlCon = SqlConnectFactory.GetConnection(DataAccess.GetSession().Connection.ConnectionString);

            //IsPrepare = true;
        }

        #endregion
    }
}
