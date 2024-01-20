using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Printers;

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