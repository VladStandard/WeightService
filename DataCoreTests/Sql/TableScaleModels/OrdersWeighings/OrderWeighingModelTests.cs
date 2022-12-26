// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.OrdersWeighings;

namespace DataCoreTests.Sql.TableScaleModels.OrdersWeighings;

[TestFixture]
internal class OrdersWeighingModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(OrderWeighingModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(OrderWeighingModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<OrderWeighingModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<OrderWeighingModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<OrderWeighingModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<OrderWeighingModel>();
    }
}