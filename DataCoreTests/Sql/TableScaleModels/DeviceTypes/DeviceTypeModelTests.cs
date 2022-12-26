// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.DeviceTypes;

namespace DataCoreTests.Sql.TableScaleModels.DeviceTypes;

[TestFixture]
internal class DeviceTypeModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(DeviceTypeModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(DeviceTypeModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<DeviceTypeModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<DeviceTypeModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceTypeModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<DeviceTypeModel>();
    }
}