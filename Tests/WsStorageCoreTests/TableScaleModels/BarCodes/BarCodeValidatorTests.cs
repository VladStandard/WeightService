// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.BarCodes;

namespace WsStorageCoreTests.TableScaleModels.BarCodes;

[TestFixture]
public sealed class BarCodeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BarCodeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<BarCodeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BarCodeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<BarCodeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}