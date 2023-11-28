namespace Ws.StorageCoreTests.Tables.Common;

public class TableModelTests<TItem> where TItem : SqlEntityBase, new()
{
    [Test]
    public virtual void Model_ToString()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            TItem item = new();
            SqlEntityBase baseItem = new();
            // Act.
            string itemString = item.ToString();
            string baseString = baseItem.ToString();
            TestContext.WriteLine($"{nameof(itemString)}: {itemString}");
            TestContext.WriteLine($"{nameof(baseString)}: {baseString}");
            // Assert.
            Assert.That(itemString, Is.Not.EqualTo(baseString));
        });
    }

    [Test]
    public virtual void Model_EqualsNew()
    {
        Assert.DoesNotThrow(() =>
        {
            TItem item = new();
            Assert.That(item, Is.EqualTo(new()));
        });
    }
}