using Ws.StorageCore.Entities.SchemaRef.PlusLines;

namespace Ws.StorageCoreTests.Tables.TableRefModels.PluLines;

[TestFixture]
public sealed class PluLinesRepositoryTests : TableRepositoryTests
{
    private SqlPluLineRepository PluLineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using(
            (IComparer<SqlPluLineEntity>)Comparer<SqlPluLineEntity>.Create((x, y) =>
                x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    private SqlPluLineEntity GetFirstPluScaleModel()
    {
        SqlCrudConfigModel sqlConfig = new() { SelectTopRowsCount = 1 };
        return PluLineRepository.GetList(sqlConfig).First();
    }

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlPluLineEntity> items = PluLineRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetListByLine()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlPluLineEntity pluLine = GetFirstPluScaleModel();
            List<SqlPluLineEntity> pluLines = PluLineRepository.GetListByLine(pluLine.Line, SqlCrudConfig);
            
            foreach (SqlPluLineEntity pluLineTest in pluLines)
            {
                Assert.That(pluLineTest.Line, Is.EqualTo(pluLine.Line));
                TestContext.WriteLine($"{pluLine}");
            }
            Assert.That(pluLines.Any(), Is.True);
        }, false);
    }
}