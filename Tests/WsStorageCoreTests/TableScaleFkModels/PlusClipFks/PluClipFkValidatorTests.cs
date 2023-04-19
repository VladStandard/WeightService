// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusClipsFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusClipFks;

[TestFixture]
public sealed class PluClipFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluClipFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluClipFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluClipFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluClipFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}