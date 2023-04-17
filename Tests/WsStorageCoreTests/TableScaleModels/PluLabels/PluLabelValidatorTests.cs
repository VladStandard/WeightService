// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleFkModels.PlusLabels;

namespace WsStorageCoreTests.TableScaleModels.PluLabels;

[TestFixture]
internal class PluLabelValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluLabelModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluLabelModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluLabelModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PluLabelModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}