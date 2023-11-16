namespace Ws.StorageCoreTests.Tables.Common;

public class TableValidatorTests<TItem> where TItem : SqlEntityBase, new()
{
    [Test]
    public virtual void Model_Validate_IsFalse()
    {
        // TestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public virtual void Model_Validate_IsTrue()
    {
        // TestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}