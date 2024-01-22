using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Printers;

[TestFixture]
public sealed class PrinterRepositoryTests : TableRepositoryTests
{
    private SqlPrinterRepository PrinterRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PrinterEntity> items = PrinterRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}