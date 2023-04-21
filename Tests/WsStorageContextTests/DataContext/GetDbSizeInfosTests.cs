// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.DataContext;

[TestFixture]
public sealed class GetDbSizeInfosTests
{
    private static List<WsConfiguration> Configurations => new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS };

    [Test]
    public void DataContext_GetDbFileSizeInfos_Assert()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlDbFileSizeInfoModel> infos = WsTestsUtils.DataTests.ContextManager.GetDbFileSizeInfos();
            Assert.That(Equals(true, infos.Any()));
            foreach (SqlDbFileSizeInfoModel info in infos)
            {
                Assert.IsNotEmpty(info.ToString());
                TestContext.WriteLine(info);
                Assert.That(info.SizeMb < 10240);
            }
        }, false, Configurations);
    }

    [Test]
    public void DataContext_GetDbFileSizeAll_Assert()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            ushort dbFileSizeAll = WsTestsUtils.DataTests.ContextManager.GetDbFileSizeAll();
            Assert.That(dbFileSizeAll > 0);
            TestContext.WriteLine($"{nameof(dbFileSizeAll)}: {dbFileSizeAll} MB");
        }, false, Configurations);
    }
}