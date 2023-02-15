// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Boxes;

[TestFixture]
internal class BoxValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BoxModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BoxModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BoxModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<BoxModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}