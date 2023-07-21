// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.Helpers;

[TestFixture]
public sealed class WsSqlContextCacheHelperTests
{
    [Test]
    public void Get_cache_table_brands() =>
        WsTestsUtils.DataTests.AssertAction(() =>
            {
                WsTestsUtils.DataTests.ContextCache.Clear();
                Assert.That(WsTestsUtils.DataTests.ContextCache.Brands.Any(), Is.False);
                WsTestsUtils.DataTests.ContextCache.SmartLoad();
                Assert.That(WsTestsUtils.DataTests.ContextCache.Brands.Any(), Is.True);
            }, false,
            new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Measure_cache_smart_load_table() =>
        WsTestsUtils.DataTests.AssertAction(() =>
            {
                WsTestsUtils.DataTests.ContextCache.Clear();
                Assert.That(WsTestsUtils.DataTests.ContextCache.Brands.Any(), Is.False);

                Stopwatch stopwatch = Stopwatch.StartNew();
                WsTestsUtils.DataTests.ContextCache.SmartLoad();
                TimeSpan elapsedFirst = stopwatch.Elapsed;
                stopwatch.Stop();
                TestContext.WriteLine($"First {nameof(stopwatch.Elapsed)}: {elapsedFirst}");
                Assert.That(WsTestsUtils.DataTests.ContextCache.Brands.Any(), Is.True);

                stopwatch.Restart();
                WsTestsUtils.DataTests.ContextCache.SmartLoad();
                TimeSpan elapsedSecond = stopwatch.Elapsed;
                stopwatch.Stop();
                TestContext.WriteLine($"Second {nameof(stopwatch.Elapsed)}: {elapsedSecond}");
               
                Assert.That(WsTestsUtils.DataTests.ContextCache.Brands.Any(), Is.True);
                Assert.That(elapsedFirst, Is.GreaterThan(elapsedSecond));
            }, false,
            new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Get_cache_view_plus_lines() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlEnumTableName.ViewPlusLines);
            Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.ViewPlusLines.Any());
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Get_cache_view_plus_lines_current() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleModel> lines = WsTestsUtils.DataTests.ContextManager.LineRepository.GetList();
            Assert.That(lines.Any(), Is.True);

            bool isPrintFirst = false;
            foreach (WsSqlScaleModel line in lines)
            {
                if (isPrintFirst) break;
                isPrintFirst = true;
                WsTestsUtils.DataTests.ContextCache.LoadLocalViewPlusLines((ushort)line.IdentityValueId);
                Assert.That(WsTestsUtils.DataTests.ContextCache.LocalViewPlusLines.Any(), Is.True);
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Get_cache_view_plus_nesting() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
            Assert.That(WsTestsUtils.DataTests.ContextCache.ViewPlusNesting.Any(), Is.True);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    //[Test]
    //public void Get_cache_view_plus_nesting_current()
    //{
    //    WsTestsUtils.DataTests.AssertAction(() =>
    //    {
    //        List<WsSqlPluModel> plus = WsTestsUtils.DataTests.ContextManager.ContextPlus.GetList();
    //        Assert.IsTrue(plus.Any());

    //        bool isPrintFirst = false;
    //        foreach (WsSqlPluModel plu in plus)
    //        {
    //            if (isPrintFirst) break;
    //            if (plu.Number > 0)
    //            {
    //                WsTestsUtils.DataTests.ContextCache.LoadLocalViewPlusNesting((ushort)plu.Number);
    //                //Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.LocalViewPlusNesting.Any());
    //                if (WsTestsUtils.DataTests.ContextCache.LocalViewPlusNesting.Any())
    //                {
    //                    TestContext.WriteLine($"{plu.Number} | {plu.Name}");
    //                    isPrintFirst = true;
    //                    WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.LocalViewPlusNesting, 10);
    //                }
    //            }
    //        }
    //    }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    //}

    [Test]
    public void Get_cache_view_plus_storage_methods() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            // Обновить кэш.
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlEnumTableName.ViewPlusStorageMethods);
            Assert.That(WsTestsUtils.DataTests.ContextCache.ViewPlusStorageMethods.Any(), Is.True);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
}