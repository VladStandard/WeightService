// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusCharacteristicsFks;

namespace WsStorageCoreTests.TableScaleFkModels.PluCharacteristicFks;

[TestFixture]
public sealed class PluCharacteristicsFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluCharacteristicsFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluCharacteristicsFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluCharacteristicsFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluCharacteristicsFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}