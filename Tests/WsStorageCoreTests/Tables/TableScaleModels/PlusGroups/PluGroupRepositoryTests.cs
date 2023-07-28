﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            WsSqlPluGroupFkModel pluGroupFkModel = GetFirstPluGroupFk();
            WsSqlPluGroupModel parentGroup = pluGroupFkModel.Parent;
            WsSqlPluGroupModel parentGroupByChild =
                PluGroupRepository.GetItemParentFromChildGroup(pluGroupFkModel.PluGroup);

            Assert.That(parentGroupByChild.IsNotNew, Is.True);
            Assert.That(parentGroupByChild, Is.EqualTo(parentGroup));

            TestContext.WriteLine(parentGroupByChild);
        }, false, DefaultPublishTypes);
    }
}