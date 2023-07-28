// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluLines;

[TestFixture]
public sealed class PluLinesRepositoryTests : TableRepositoryTests
{
    private WsSqlPluLineRepository PluLineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using(
            (IComparer<WsSqlPluScaleModel>)Comparer<WsSqlPluScaleModel>.Create((x, y) =>
                x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    private WsSqlPluScaleModel GetFirstPluScaleModel()
    {
        WsSqlCrudConfigModel sqlConfig = new() { SelectTopRowsCount = 1 };
        return PluLineRepository.GetList(sqlConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluScaleModel> items = PluLineRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
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