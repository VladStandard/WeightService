// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Contragents;

[TestFixture]
public sealed class ContragentValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ContragentModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ContragentModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ContragentModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ContragentModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}