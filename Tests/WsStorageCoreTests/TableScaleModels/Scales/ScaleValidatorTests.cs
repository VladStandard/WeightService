// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Scales;

namespace WsStorageCoreTests.TableScaleModels.Scales;

[TestFixture]
public sealed class ScaleValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        ScaleModel item = WsTestsUtils.DataCore.CreateNewSubstitute<ScaleModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        ScaleModel item = WsTestsUtils.DataCore.CreateNewSubstitute<ScaleModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}