using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Boxes;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Boxes;

[TestFixture]
public sealed class BoxRepositoryTests : TableRepositoryTests
{
    private SqlBoxRepository BoxRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<BoxEntity> items = BoxRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}