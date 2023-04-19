// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Clips;

namespace WsStorageCoreTests.TableScaleModels.Clips;

public sealed class ClipValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ClipModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ClipModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ClipModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ClipModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}