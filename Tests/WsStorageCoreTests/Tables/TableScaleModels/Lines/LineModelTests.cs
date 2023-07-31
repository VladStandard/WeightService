namespace WsStorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineModelTests : TableModelTests<WsSqlScaleModel>
{
    [Test]
    public override void Model_AssertSqlFields_Check()
    {
        base.Model_AssertSqlFields_Check();
        //WsTestsUtils.DataCore.AssertSqlFieldStringCheck<ScaleModel>(nameof(ScaleModel.Host.Name));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<WsSqlScaleModel>(nameof(WsSqlTableBase.Description));
    }
}