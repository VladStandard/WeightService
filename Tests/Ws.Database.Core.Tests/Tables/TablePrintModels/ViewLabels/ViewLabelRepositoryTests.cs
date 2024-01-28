using Ws.Database.Core.Entities.Print.ViewLabels;
using Ws.Domain.Models.Entities.Print;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.ViewLabels;

[TestFixture]
public sealed class ViewLabelRepositoryTests : TableRepositoryTests
{
    private ViewLabelRepository LabelRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(ViewLabel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        // AssertAction(() =>
        // {
        //     IEnumerable<ViewLabel> items = LabelRepository.GetList(SqlCrudConfig);
        //     ParseRecords(items);
        // });
    }
}