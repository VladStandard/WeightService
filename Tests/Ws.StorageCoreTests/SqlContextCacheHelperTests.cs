using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.StorageCoreTests;

[TestFixture]
public sealed class SqlContextCacheHelperTests
{
    private SqlLineRepository LineRepository { get; } = new();
    
    [Test]
    public void Get_cache_view_plus_lines() =>
        TestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            TestsUtils.DataTests.ContextCache.Load(SqlEnumTableName.ViewPlusLines);
            Assert.That(TestsUtils.DataTests.ContextCache.ViewPlusLines.Any(), Is.True);
        }, false);

    [Test]
    public void Get_cache_view_plus_lines_current() =>
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlScaleEntity> lines = LineRepository.GetEnumerable(new()).ToList();
            Assert.That(lines.Any(), Is.True);

            bool isPrintFirst = false;
            foreach (SqlScaleEntity line in lines)
            {
                if (isPrintFirst) break;
                isPrintFirst = true;
                TestsUtils.DataTests.ContextCache.LoadLocalViewPlusLines((ushort)line.IdentityValueId);
                Assert.That(TestsUtils.DataTests.ContextCache.LocalViewPlusLines.Any(), Is.True);
            }
        }, false);

    [Test]
    public void Get_cache_view_plus_nesting() =>
        TestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            TestsUtils.DataTests.ContextCache.Load(SqlEnumTableName.ViewPlusNesting);
            Assert.That(TestsUtils.DataTests.ContextCache.ViewPlusNesting.Any(), Is.True);
        }, false);
    
    [Test]
    public void Get_cache_view_plus_storage_methods() =>
        TestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            TestsUtils.DataTests.ContextCache.Load(SqlEnumTableName.ViewPlusStorageMethods);
            Assert.That(TestsUtils.DataTests.ContextCache.ViewPlusStorageMethods.Any(), Is.True);
        }, false);
}