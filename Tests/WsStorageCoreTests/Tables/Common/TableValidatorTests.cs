namespace WsStorageCoreTests.Tables.Common;

public class TableValidatorTests<TItem> where TItem : WsSqlTableBase, new()
{
    [Test]
    public virtual void Model_Validate_IsFalse()
    {
        TItem item = WsTestsUtils.DataTests.CreateNewSubstitute<TItem>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public virtual void Model_Validate_IsTrue()
    {
        TItem item = WsTestsUtils.DataTests.CreateNewSubstitute<TItem>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}