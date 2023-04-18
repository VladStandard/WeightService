// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PlusScales;

namespace WsStorageCoreTests.TableScaleModels.PluScales;

[TestFixture]
public sealed class PluScaleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluScaleModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluScaleModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluScaleModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluScaleModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}