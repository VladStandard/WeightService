namespace WsStorageCoreTests.Tables.TableScaleModels.Bundles;

[TestFixture]
public sealed class BundleRepositoryTests : TableRepositoryTests
{
    private WsSqlBundleRepository BundleRepository { get; } = new();

    private WsSqlBundleModel GetFirstBundleModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return BundleRepository.GetList(SqlCrudConfig).First();
    }

    private WsSqlPluBundleFkModel GetFirstBundleFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return new WsSqlPluBundleFkRepository().GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlBundleModel> items = BundleRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByPlu()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluBundleFkModel bundleFkModel = GetFirstBundleFkModel();
            WsSqlBundleModel oldBundle = bundleFkModel.Bundle;
            WsSqlBundleModel bundleByPlu = BundleRepository.GetItemByPlu(bundleFkModel.Plu);

            Assert.That(bundleByPlu.IsNotNew, Is.True);
            Assert.That(bundleByPlu, Is.EqualTo(oldBundle));

            TestContext.WriteLine(bundleByPlu);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByUid1C()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlBundleModel oldBundle = GetFirstBundleModel();
            WsSqlBundleModel bundleBy1C = BundleRepository.GetItemByUid1C(oldBundle.Uid1C);

            Assert.That(bundleBy1C.IsNotNew, Is.True);
            Assert.That(bundleBy1C, Is.EqualTo(oldBundle));

            TestContext.WriteLine(bundleBy1C);
        }, false, DefaultConfigurations);
    }
}