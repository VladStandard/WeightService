using Ws.StorageCore.Entities.SchemaScale.PlusScales;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.PluLines;

[TestFixture]
public sealed class PluLinesRepositoryTests : TableRepositoryTests
{
    private SqlPluLineRepository PluLineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using(
            (IComparer<SqlPluScaleEntity>)Comparer<SqlPluScaleEntity>.Create((x, y) =>
                x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    private SqlPluScaleEntity GetFirstPluScaleModel()
    {
        SqlCrudConfigModel sqlConfig = new() { SelectTopRowsCount = 1 };
        return PluLineRepository.GetList(sqlConfig).First();
    }

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlPluScaleEntity> items = PluLineRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetListByLine()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlPluScaleEntity pluScale = GetFirstPluScaleModel();
            SqlScaleEntity line = pluScale.Line;
            List<SqlPluScaleEntity> pluLines = PluLineRepository.GetListByLine(line, SqlCrudConfig);
            foreach (SqlPluScaleEntity pluLine in pluLines)
            {
                Assert.That(pluLine.Line, Is.EqualTo(line));
                TestContext.WriteLine($"{pluLine}");
            }

            Assert.That(pluLines.Any(), Is.True);
        }, false);
    }
}