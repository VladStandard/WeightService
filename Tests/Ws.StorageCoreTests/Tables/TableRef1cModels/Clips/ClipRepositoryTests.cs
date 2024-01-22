using Ws.Database.Core.Entities.Ref1c.Clips;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Clips;

[TestFixture]
public sealed class ClipRepositoryTests : TableRepositoryTests
{
    private SqlClipRepository ClipRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<ClipEntity> items = ClipRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}