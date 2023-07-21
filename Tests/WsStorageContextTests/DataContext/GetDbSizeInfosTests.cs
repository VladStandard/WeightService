// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.DataContext;

[TestFixture]
public sealed class GetDbSizeInfosTests
{
    private static List<WsEnumConfiguration> Configurations => new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS };

    [Test]
    public void DataContext_GetDbFileSizeInfos_Assert()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDbFileSizeInfoModel> infos = WsTestsUtils.DataTests.ContextManager.GetDbFileSizeInfos();
            Assert.That(infos.Any(), Is.EqualTo(true));
            foreach (WsSqlDbFileSizeInfoModel info in infos)
            {
                Assert.IsNotEmpty(info.ToString());
                TestContext.WriteLine(info);
                Assert.That(info.SizeMb, Is.LessThan(10240));
            }
        }, false, Configurations);
    }

    [Test]
    public void DataContext_GetDbFileSizeAll_Assert()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            ushort dbFileSizeAll = WsTestsUtils.DataTests.ContextManager.GetDbFileSizeAll();
            Assert.That(dbFileSizeAll, Is.GreaterThan(0));
            TestContext.WriteLine($"{nameof(dbFileSizeAll)}: {dbFileSizeAll} MB");
        }, false, Configurations);
    }
}