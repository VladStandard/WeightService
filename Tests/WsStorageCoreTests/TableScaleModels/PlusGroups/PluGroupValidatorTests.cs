// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PlusGroups;

namespace WsStorageCoreTests.TableScaleModels.PlusGroups;

[TestFixture]
public sealed class PluGroupValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluGroupModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluGroupModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluGroupModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluGroupModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}