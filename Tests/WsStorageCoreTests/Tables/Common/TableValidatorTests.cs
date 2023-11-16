namespace WsStorageCoreTests.Tables.Common;

public class TableValidatorTests<TItem> where TItem : SqlEntityBase, new()
{
    [Test]
    public virtual void Model_Validate_IsFalse()
    {
        // WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public virtual void Model_Validate_IsTrue()
    {
        // WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}