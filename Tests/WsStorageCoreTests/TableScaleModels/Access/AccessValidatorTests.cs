// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Access;

[TestFixture]
public sealed class AccessValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlAccessModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlAccessModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlAccessModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlAccessModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}