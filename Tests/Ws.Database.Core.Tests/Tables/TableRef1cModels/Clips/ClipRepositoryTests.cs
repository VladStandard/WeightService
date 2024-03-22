using Ws.Database.Nhibernate.Entities.Ref1c.Clips;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Clips;

[TestFixture]
public sealed class ClipRepositoryTests : TableRepositoryTests
{
    private SqlClipRepository ClipRepository { get; } = new();

    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<ClipEntity> items = ClipRepository.GetAll();
            ParseRecords(items);
        });
    }
}