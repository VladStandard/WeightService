// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Boxes;

namespace WsStorageCoreTests.TableScaleModels.Boxes;

[TestFixture]
public sealed class BoxValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        BoxModel item = WsTestsUtils.DataCore.CreateNewSubstitute<BoxModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        BoxModel item = WsTestsUtils.DataCore.CreateNewSubstitute<BoxModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}