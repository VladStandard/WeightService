// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Boxes;

[TestFixture]
public sealed class BoxValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlBoxModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBoxModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlBoxModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBoxModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}