// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusNestingFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusNestingFks;

[TestFixture]
public sealed class PluNestingFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluNestingFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluNestingFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluNestingFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluNestingFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}