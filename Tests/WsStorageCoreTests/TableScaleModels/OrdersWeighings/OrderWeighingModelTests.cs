// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Tables;
using WsStorage.TableScaleModels.OrdersWeighings;

namespace WsStorageCoreTests.TableScaleModels.OrdersWeighings;

[TestFixture]
internal class OrdersWeighingModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(WsSqlTableBase.CreateDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(WsSqlTableBase.ChangeDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckBool<OrderWeighingModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertToString<OrderWeighingModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertEqualsNew<OrderWeighingModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertSerialize<OrderWeighingModel>();
    }
}