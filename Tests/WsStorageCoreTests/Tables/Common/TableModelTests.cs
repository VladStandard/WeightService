namespace WsStorageCoreTests.Tables.Common;

public class TableModelTests<TItem> where TItem : SqlEntityBase, new()
{
    [Test]
    public virtual void Model_ToString()
    {
        TestsUtils.DataTests.TableBaseModelAssertToString<TItem>();
    }

    [Test]
    public virtual void Model_EqualsNew()
    {
        TestsUtils.DataTests.TableBaseModelAssertEqualsNew<TItem>();
    }

    [Test]
    public virtual void Model_EqualsDefault()
    {
        TestsUtils.DataTests.TableBaseModelAssertEqualsDefault<TItem>();
    }
}