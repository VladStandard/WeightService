// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.BarCodes;

[TestFixture]
public sealed class BarCodeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlBarCodeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBarCodeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlBarCodeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlBarCodeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}