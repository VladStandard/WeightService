// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusStorageMethodsFks;

namespace WsStorageCoreTests.TableScaleFkModels.PluStorageMethodFks;

[TestFixture]
public sealed class PluStorageMethodFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluStorageMethodFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluStorageMethodFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluStorageMethodFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluStorageMethodFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}