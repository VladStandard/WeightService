using Ws.Domain.Models.Entities.Print;
using Ws.StorageCore.Entities.Print.Labels;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.Labels;

[TestFixture]
public sealed class LabelRepositoryTests : TableRepositoryTests
{
    private SqlLabelRepository LabelRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(LabelEntity.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<LabelEntity> items = LabelRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}