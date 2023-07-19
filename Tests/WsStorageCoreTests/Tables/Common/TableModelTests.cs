namespace WsStorageCoreTests.Tables.Common;

public class TableModelTests<TItem> where TItem: WsSqlTableBase, new()
{
    [Test]
    public virtual void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<TItem>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<TItem>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<TItem>(nameof(WsSqlTableBase.IsMarked));
    }
    
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

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<TItem>();
    }
}