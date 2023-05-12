// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.Utils;

[TestFixture]
public sealed class SqlQueriesTests
{
    [Test]
    public void SqlQueries_GetWeightingsAggr_MoreThanZero()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluAggrModel> pluWeighingAggrs = new();
            object[] objects = WsTestsUtils.DataTests.ContextManager.AccessList.GetArrayObjectsNotNullable(
                WsSqlQueriesScales.Tables.PluWeighings.GetWeighingsAggr(200));
            Assert.That(objects.Any());
            foreach (object obj in objects)
            {
                if (obj is object[] { Length: 4 } item)
                {
                    DateTime dt = Convert.ToDateTime(item[0]);
                    int count = Convert.ToInt32(item[1]);
                    string line = Convert.ToString(item[2]) ?? string.Empty;
                    string plu = Convert.ToString(item[3]) ?? string.Empty;
                    pluWeighingAggrs.Add(new(dt, count, line, plu));
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (i < pluWeighingAggrs.Count)
                    TestContext.WriteLine(pluWeighingAggrs[i]);
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test]
    public void SqlQueries_GetLabelsAggr_MoreThanZero()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluAggrModel> pluWeighingAggrs = new();
            object[] objects = WsTestsUtils.DataTests.ContextManager.AccessList.GetArrayObjectsNotNullable(
                WsSqlQueriesScales.Tables.PluLabels.GetLabelsAggrWithoutPlu(200));
            Assert.That(objects.Any());
            foreach (object obj in objects)
            {
                if (obj is object[] { Length: 4 } item)
                {
                    pluWeighingAggrs.Add(new(Convert.ToDateTime(item[0]),
                        Convert.ToInt32(item[1]), Convert.ToString(item[2]) ?? string.Empty,
                        Convert.ToString(item[3]) ?? string.Empty)
                    );
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (i < pluWeighingAggrs.Count)
                    TestContext.WriteLine(pluWeighingAggrs[i]);
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}