using WsStorageCore.Entities.SchemaScale.PlusScales;
using WsStorageCore.Entities.SchemaScale.Scales;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluLines;

[TestFixture]
public sealed class PluLinesRepositoryTests : TableRepositoryTests
{
    private WsSqlPluLineRepository PluLineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using(
            (IComparer<WsSqlPluScaleEntity>)Comparer<WsSqlPluScaleEntity>.Create((x, y) =>
                x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    private WsSqlPluScaleEntity GetFirstPluScaleModel()
    {
        WsSqlCrudConfigModel sqlConfig = new() { SelectTopRowsCount = 1 };
        return PluLineRepository.GetList(sqlConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluScaleEntity> items = PluLineRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetListByLine()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluScaleEntity pluScale = GetFirstPluScaleModel();
            WsSqlScaleEntity line = pluScale.Line;
            List<WsSqlPluScaleEntity> pluLines = PluLineRepository.GetListByLine(line, SqlCrudConfig);
            foreach (WsSqlPluScaleEntity pluLine in pluLines)
            {
                Assert.That(pluLine.Line, Is.EqualTo(line));
                TestContext.WriteLine($"{pluLine}");
            }

            Assert.That(pluLines.Any(), Is.True);
        }, false, DefaultConfigurations);
    }
}