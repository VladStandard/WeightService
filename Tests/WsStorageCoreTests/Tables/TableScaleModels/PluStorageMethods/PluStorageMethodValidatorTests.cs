// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluStorageMethods;

[TestFixture]
public sealed class PluStorageMethodValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPluStorageMethodModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluStorageMethodModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPluStorageMethodModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPluStorageMethodModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}