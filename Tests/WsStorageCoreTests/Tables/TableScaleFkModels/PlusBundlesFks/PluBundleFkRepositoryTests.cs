// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusBundlesFks;

[TestFixture]
public sealed class PluBundleFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluBundleFkRepository PluBundleFkRepository { get; } = new();

    private WsSqlPluBundleFkModel GetFirstPluBundleFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluBundleFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluBundleFkModel> items = PluBundleFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void GetListByPlu()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluBundleFkModel pluBundleFk = GetFirstPluBundleFkModel();
            WsSqlPluModel plu = pluBundleFk.Plu;
            List<WsSqlPluBundleFkModel> pluBundlesFks = PluBundleFkRepository.GetListByPlu(plu);
            foreach (WsSqlPluBundleFkModel pluBundlesFk in pluBundlesFks)
            {
                Assert.That(pluBundlesFk.Plu, Is.EqualTo(plu));
                TestContext.WriteLine(pluBundlesFk);
            }

            Assert.That(pluBundlesFks.Any(), Is.True);
        }, false, DefaultPublishTypes);
    }
}