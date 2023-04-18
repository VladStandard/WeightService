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
        PluClipFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluClipFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluClipFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluClipFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}