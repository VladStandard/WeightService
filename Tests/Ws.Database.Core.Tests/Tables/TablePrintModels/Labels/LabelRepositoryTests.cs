using Ws.Database.Core.Entities.Print.Labels;
using Ws.Domain.Models.Entities.Print;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.Labels;

[TestFixture]
public sealed class LabelRepositoryTests : TableRepositoryTests
{
    private SqlLabelRepository LabelRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(LabelEntity.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<LabelEntity> items = LabelRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}