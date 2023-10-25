namespace WsStorageCoreTests.Tables.Common;

public class TableModelTests<TItem> where TItem : WsSqlEntityBase, new()
{
    [Test]
    public virtual void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<TItem>();
    }

    [Test]
    public virtual void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<TItem>();
    }

    [Test]
    public virtual void Model_EqualsDefault()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsDefault<TItem>();
    }
}