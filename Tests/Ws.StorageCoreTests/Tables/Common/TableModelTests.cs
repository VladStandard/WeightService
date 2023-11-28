namespace Ws.StorageCoreTests.Tables.Common;

public class TableModelTests<TItem> where TItem : SqlEntityBase, new()
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