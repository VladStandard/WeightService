using CoreTests;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BlazorCoreTests.DAL
{
    [TestFixture]
    internal class AppSettingsEntityTests
    {
        [Test]
        public void AppSettingsEntity_CheckProperties_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            foreach (var server in TestsEnums.GetSqlServer())
            {
                foreach (var db in TestsEnums.GetSqlDb())
                {
                    Assert.DoesNotThrowAsync(async () => await Task.Run(() =>
                    {
                        var appSettings = new CoreSettingsEntity(server, db, true, "username", "password");
                        appSettings.CheckProperties();
                    }));
                    foreach (var username in TestsEnums.GetSqlUsername())
                    {
                        foreach (var password in TestsEnums.GetSqlPassword())
                        {
                            Assert.DoesNotThrowAsync(async () => await Task.Run(() =>
                            {
                                var appSettings = new CoreSettingsEntity(server, db, true, username, password);
                                appSettings.CheckProperties();
                            }));
                        }
                    }
                }
            }

            TestsUtils.MethodComplete();
        }
    }
}
