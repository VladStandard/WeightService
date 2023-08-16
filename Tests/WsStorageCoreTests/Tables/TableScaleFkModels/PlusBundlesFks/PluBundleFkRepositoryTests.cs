namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusBundlesFks;

[TestFixture]
public sealed class PluBundleFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluBundleFkRepository PluBundleFkRepository { get; } = new();

    private WsSqlPluBundleFkModel GetFirstPluBundleFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluBundleFkRepository.GetEnumerable(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluBundleFkModel> items = PluBundleFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetListByPlu()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluBundleFkModel pluBundleFk = GetFirstPluBundleFkModel();
            WsSqlPluModel plu = pluBundleFk.Plu;
            IEnumerable<WsSqlPluBundleFkModel> pluBundlesFks = PluBundleFkRepository.GetEnumerableByPlu(plu).ToList();
            foreach (WsSqlPluBundleFkModel pluBundlesFk in pluBundlesFks)
            {
                Assert.That(pluBundlesFk.Plu, Is.EqualTo(plu));
                TestContext.WriteLine(pluBundlesFk);
            }

            Assert.That(pluBundlesFks.Any(), Is.True);
        }, false, DefaultConfigurations);
    }
}