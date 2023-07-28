// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusGroupsFks;

[TestFixture]
public sealed class PluGroupFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluGroupFkRepository PluGroupFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluGroupFkModel> items = PluGroupFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}