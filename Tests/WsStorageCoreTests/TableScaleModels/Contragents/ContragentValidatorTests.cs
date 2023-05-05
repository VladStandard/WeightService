// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Contragents;

[TestFixture]
public sealed class ContragentValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlContragentModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlContragentModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlContragentModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlContragentModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}