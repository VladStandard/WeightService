using Ws.StorageCore.Entities.SchemaRef.PlusLines;
using Ws.StorageCore.Entities.SchemaScale.Scales;

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
            SqlLineEntity line = pluLine.Line;
            List<SqlPluLineEntity> pluLines = PluLineRepository.GetListByLine(line, SqlCrudConfig);
            foreach (SqlPluLineEntity pluLine1 in pluLines)
            {
                Assert.That(pluLine1.Line, Is.EqualTo(line));
                TestContext.WriteLine($"{pluLine}");
            }

            Assert.That(pluLines.Any(), Is.True);
        }, false);
    }
}