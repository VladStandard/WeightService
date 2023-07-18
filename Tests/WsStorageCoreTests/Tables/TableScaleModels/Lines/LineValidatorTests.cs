// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlScaleModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlScaleModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}