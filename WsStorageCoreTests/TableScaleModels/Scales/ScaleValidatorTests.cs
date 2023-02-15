// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Scales;

[TestFixture]
internal class ScaleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ScaleModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ScaleModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ScaleModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ScaleModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}