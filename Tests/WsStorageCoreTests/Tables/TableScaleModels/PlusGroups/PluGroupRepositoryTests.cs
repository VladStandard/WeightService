using WsStorageCoreTests.Tables.Common;

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
        }, false, DefaultPublishTypes);
    }
    
    [Test]
    public void GetItemByChildGroup()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluGroupFkModel PluGroupFkModel = GetFirstPluGroupFk();
            WsSqlPluGroupModel parentGroup = PluGroupFkModel.Parent;
            WsSqlPluGroupModel parentGroupByChild = PluGroupRepository.GetItemParentFromChildGroup(PluGroupFkModel.PluGroup);
            
            Assert.That(parentGroupByChild.IsNotNew, Is.True);
            Assert.That(parentGroupByChild, Is.EqualTo(parentGroup));
            
            TestContext.WriteLine(parentGroupByChild);
        }, false, DefaultPublishTypes);
    }
}