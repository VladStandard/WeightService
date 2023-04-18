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
        PluNestingFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluNestingFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluNestingFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluNestingFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}