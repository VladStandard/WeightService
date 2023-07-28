// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusCharacteristicsFks;

[TestFixture]
public sealed class PluCharacteristicsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluCharacteristicsFkRepository PluCharacteristicsFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluCharacteristicsFkModel> items = PluCharacteristicsFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}