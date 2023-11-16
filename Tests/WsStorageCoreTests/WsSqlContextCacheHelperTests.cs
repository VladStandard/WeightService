using WsStorageCore.Entities.SchemaScale.Scales;

namespace WsStorageCoreTests;

[TestFixture]
public sealed class WsSqlContextCacheHelperTests
{
    private SqlLineRepository LineRepository { get; } = new();
    
    [Test]
    public void Get_cache_view_plus_lines() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            WsTestsUtils.DataTests.ContextCache.Load(SqlEnumTableName.ViewPlusLines);
            Assert.That(WsTestsUtils.DataTests.ContextCache.ViewPlusLines.Any(), Is.True);
        }, false);

    [Test]
    public void Get_cache_view_plus_lines_current() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlScaleEntity> lines = LineRepository.GetEnumerable(new()).ToList();
            Assert.That(lines.Any(), Is.True);

            bool isPrintFirst = false;
            foreach (SqlScaleEntity line in lines)
            {
                if (isPrintFirst) break;
                isPrintFirst = true;
                WsTestsUtils.DataTests.ContextCache.LoadLocalViewPlusLines((ushort)line.IdentityValueId);
                Assert.That(WsTestsUtils.DataTests.ContextCache.LocalViewPlusLines.Any(), Is.True);
            }
        }, false);

    [Test]
    public void Get_cache_view_plus_nesting() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            WsTestsUtils.DataTests.ContextCache.Load(SqlEnumTableName.ViewPlusNesting);
            Assert.That(WsTestsUtils.DataTests.ContextCache.ViewPlusNesting.Any(), Is.True);
        }, false);
    
    [Test]
    public void Get_cache_view_plus_storage_methods() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            WsTestsUtils.DataTests.ContextCache.Load(SqlEnumTableName.ViewPlusStorageMethods);
            Assert.That(WsTestsUtils.DataTests.ContextCache.ViewPlusStorageMethods.Any(), Is.True);
        }, false);
}