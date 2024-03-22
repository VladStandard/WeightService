using Ws.Domain.Models.Common;

namespace Ws.StorageCoreTests.Tables.Common;

public class TableModelTests<TItem> where TItem : EntityBase, new()
{
    [Test]
    public virtual void Model_EqualsNew()
    {
        Assert.DoesNotThrow(() =>
        {
            TItem item = new();
            Assert.That(item, Is.EqualTo(new TItem()));
        });
    }
}