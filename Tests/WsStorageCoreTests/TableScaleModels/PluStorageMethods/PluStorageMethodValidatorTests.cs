// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PlusStorageMethods;

namespace WsStorageCoreTests.TableScaleModels.PluStorageMethods;

[TestFixture]
public sealed class PluStorageMethodValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluStorageMethodModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluStorageMethodModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluStorageMethodModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluStorageMethodModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}