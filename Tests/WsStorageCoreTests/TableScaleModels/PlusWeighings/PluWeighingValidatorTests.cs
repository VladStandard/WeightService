// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PlusWeighings;

[TestFixture]
public sealed class PluWeighingValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluWeighingModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluWeighingModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluWeighingModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluWeighingModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}