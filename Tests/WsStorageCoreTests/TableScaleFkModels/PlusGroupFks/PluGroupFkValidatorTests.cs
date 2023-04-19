// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusGroupsFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusGroupFks;

[TestFixture]
public sealed class PluGroupFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluGroupFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluGroupFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluGroupFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluGroupFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}