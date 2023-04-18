// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusFks;

[TestFixture]
public sealed class PluFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PluFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}