using NUnit.Framework;
using System.Threading.Tasks;
using BlazorCore.Models;

namespace BlazorCoreTests.DAL
{
    [TestFixture]
    internal class AppSettingsEntityTests
    {
        [Test]
        public void AppSettingsEntity_CheckProperties_DoesNotThrow()
        {
            Utils.MethodStart();

            foreach (var server in EnumValues.GetSqlServer())
            {
                foreach (var db in EnumValues.GetSqlDb())
                {
                    Assert.DoesNotThrowAsync(async () => await Task.Run(() =>
                    {
                        var appSettings = new CoreSettingsEntity(server, db, true, "username", "password");
                        appSettings.CheckProperties();
                    }));
                    foreach (var username in EnumValues.GetSqlUsername())
                    {
                        foreach (var password in EnumValues.GetSqlPassword())
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

            Utils.MethodComplete();
        }
    }
}
