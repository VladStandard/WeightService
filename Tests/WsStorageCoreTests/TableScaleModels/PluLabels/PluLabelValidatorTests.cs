// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PluLabels;

[TestFixture]
public sealed class PluLabelValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluLabelModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluLabelModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluLabelModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluLabelModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}