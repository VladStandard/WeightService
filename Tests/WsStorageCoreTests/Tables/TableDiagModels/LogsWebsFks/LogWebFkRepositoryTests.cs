// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableDiagModels.LogsWebsFks;

[TestFixture]
public sealed class LogWebFkRepositoryTests : TableRepositoryTests
{
    private WsSqlLogWebFkRepository LogWebFkRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using((IComparer<WsSqlLogWebFkModel>)Comparer<WsSqlLogWebFkModel>.Create((x, y) =>
            x.LogWebRequest.CreateDt.CompareTo(y.LogWebRequest.CreateDt))).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlLogWebFkModel> items = LogWebFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}