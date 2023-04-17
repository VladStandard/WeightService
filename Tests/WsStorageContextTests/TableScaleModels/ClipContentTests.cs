// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.Clips;

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
internal class ClipContentTests
{
    [Test]
    public void Model_Validate_Content()
    {
        DataCoreTestsUtils.DataCore.AssertSqlDbContentValidate<ClipModel>();
    }
}