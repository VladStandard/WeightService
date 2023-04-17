// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleModels.TasksTypes;

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
internal class TaskTypeContentTests
{
	[Test]
    public void Model_Content_Validate()
    {
		DataCoreTestsUtils.DataCore.AssertSqlDbContentValidate<TaskTypeModel>();
	}
}