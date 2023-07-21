using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluLines;

[TestFixture]
public sealed class PluLinesRepositoryTests : TableRepositoryTests
{
    private WsSqlPluLineRepository PluLineRepository  { get; set; } = new();

    private WsSqlPluScaleModel GetFirstPluScaleModel()
    {
        WsSqlCrudConfigModel sqlConfig = GetNewSqlConfig();
        sqlConfig.SelectTopRowsCount = 1;
        return PluLineRepository.GetList(sqlConfig).First();
    }
        
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluScaleModel> items = PluLineRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
    
    [Test]
    public void GetListByLine()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluScaleModel pluScale = GetFirstPluScaleModel();
            WsSqlScaleModel line = pluScale.Line;
            List<WsSqlPluScaleModel> pluLines = PluLineRepository.GetListByLine(line, SqlCrudConfig);
            foreach (WsSqlPluScaleModel pluLine in pluLines)
            {
                Assert.That(pluLine.Line, Is.EqualTo(line));
                TestContext.WriteLine($"{pluLine}");
            }
            Assert.That(pluLines.Any(), Is.True);
        }, false, DefaultPublishTypes);
    }
}