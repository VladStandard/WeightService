using Ws.Database.Nhibernate.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Printers;

[TestFixture]
public sealed class PrinterRepositoryTests : TableRepositoryTests
{
    private SqlPrinterRepository PrinterRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(PrinterEntity.Type)).Ascending;

    [Test]
    public void GetList()
    {
        AssertAction(() => {
            IEnumerable<PrinterEntity> items = PrinterRepository.GetAll();
            ParseRecords(items);
        });
    }
}