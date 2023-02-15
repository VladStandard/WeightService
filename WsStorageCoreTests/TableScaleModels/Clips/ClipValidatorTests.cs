// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Clips;

internal class ClipValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ClipModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ClipModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ClipModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<ClipModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}