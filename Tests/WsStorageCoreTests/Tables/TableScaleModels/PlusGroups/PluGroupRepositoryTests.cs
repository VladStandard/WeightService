namespace WsStorageCoreTests.Tables.TableScaleModels.PlusGroups;

[TestFixture]
public sealed class PluGroupRepositoryTests : TableRepositoryTests
{
    private WsSqlPluGroupRepository PluGroupRepository { get; } = new();

    private WsSqlPluGroupFkModel GetFirstPluGroupFk()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return new WsSqlPluGroupFkRepository().GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluGroupModel> items = PluGroupRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByChildGroup()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluGroupFkModel pluGroupFkModel = GetFirstPluGroupFk();
            WsSqlPluGroupModel parentGroup = pluGroupFkModel.Parent;
            WsSqlPluGroupModel parentGroupByChild =
                PluGroupRepository.GetItemParentFromChildGroup(pluGroupFkModel.PluGroup);

            Assert.That(parentGroupByChild.IsNotNew, Is.True);
            Assert.That(parentGroupByChild, Is.EqualTo(parentGroup));

            TestContext.WriteLine(parentGroupByChild);
        }, false, DefaultConfigurations);
    }
}