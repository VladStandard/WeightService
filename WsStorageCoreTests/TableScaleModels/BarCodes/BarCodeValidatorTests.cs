// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.BarCodes;

[TestFixture]
internal class BarCodeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BarCodeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BarCodeModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BarCodeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BarCodeModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}