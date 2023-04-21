// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Scales;

[TestFixture]
public sealed class ScaleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ScaleModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ScaleModel item = WsTestsUtils.DataTests.CreateNewSubstitute<ScaleModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}