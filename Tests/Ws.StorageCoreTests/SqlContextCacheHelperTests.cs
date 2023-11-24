using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.StorageCoreTests;

[TestFixture]
public sealed class SqlContextCacheHelperTests
{
    private SqlLineRepository LineRepository { get; } = new();
    

    [Test]
    public void Get_cache_view_plus_lines_current() =>
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlLineEntity> lines = LineRepository.GetEnumerable(new()).ToList();
            Assert.That(lines.Any(), Is.True);

            bool isPrintFirst = false;
            foreach (SqlLineEntity line in lines)
            {
                if (isPrintFirst) break;
                isPrintFirst = true;
                TestsUtils.DataTests.ContextCache.LoadLocalViewPlusLines((ushort)line.IdentityValueId);
                Assert.That(TestsUtils.DataTests.ContextCache.LocalViewPlusLines.Any(), Is.True);
            }
        }, false);
}