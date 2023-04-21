// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PluScales;

[TestFixture]
public sealed class PluScaleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluScaleModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluScaleModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}