using Ws.Domain.Models.Entities.Scale;
using Ws.StorageCore.Entities.Scales.PlusFks;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.PlusFks;

[TestFixture]
public sealed class PluFkRepositoryTests : TableRepositoryTests
{
    private SqlPluFkRepository PluFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PluFkEntity> items = PluFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}