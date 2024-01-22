using Ws.Database.Core.Entities.Ref1c.Boxes;
using Ws.Domain.Models.Entities.Ref1c;

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