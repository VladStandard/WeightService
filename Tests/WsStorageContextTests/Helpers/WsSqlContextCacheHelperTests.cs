// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsStorageContextTests.Helpers;

[TestFixture]
public sealed class WsSqlContextCacheHelperTests
{
    [Test]
    public void Get_cache_table_brands() =>
        WsTestsUtils.DataTests.AssertAction(() =>
            {
                WsTestsUtils.DataTests.ContextCache.Clear();
                Assert.IsFalse(WsTestsUtils.DataTests.ContextCache.Brands.Any());
                WsTestsUtils.DataTests.ContextCache.SmartLoad();
                Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.Brands.Any());
                WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.Brands, 10);
            }, false,
            new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Measure_cache_smart_load_table() =>
        WsTestsUtils.DataTests.AssertAction(() =>
            {
                WsTestsUtils.DataTests.ContextCache.Clear();
                Assert.IsFalse(WsTestsUtils.DataTests.ContextCache.Brands.Any());

                Stopwatch stopwatch = Stopwatch.StartNew();
                WsTestsUtils.DataTests.ContextCache.SmartLoad();
                TimeSpan elapsedFirst = stopwatch.Elapsed;
                stopwatch.Stop();
                TestContext.WriteLine($"First {nameof(stopwatch.Elapsed)}: {elapsedFirst}");
                Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.Brands.Any());

                stopwatch.Restart();
                WsTestsUtils.DataTests.ContextCache.SmartLoad();
                TimeSpan elapsedSecond = stopwatch.Elapsed;
                stopwatch.Stop();
                TestContext.WriteLine($"Second {nameof(stopwatch.Elapsed)}: {elapsedSecond}");
               
                Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.Brands.Any());
                Assert.IsTrue(elapsedFirst > elapsedSecond);
            }, false,
            new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Get_cache_view_plus_lines() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlEnumTableName.ViewPlusLines);
            Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.ViewPlusLines.Any());
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.ViewPlusLines, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Get_cache_view_plus_lines_current() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleModel> lines = WsTestsUtils.DataTests.ContextManager.ContextLines.GetList();
            Assert.IsTrue(lines.Any());

            bool isPrintFirst = false;
            foreach (WsSqlScaleModel line in lines)
            {
                if (isPrintFirst) break;
                isPrintFirst = true;
                WsTestsUtils.DataTests.ContextCache.LoadLocalViewPlusLines((ushort)line.IdentityValueId);
                Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.LocalViewPlusLines.Any());
                WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.LocalViewPlusLines, 10);
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });

    [Test]
    public void Get_cache_view_plus_nesting() =>
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlEnumTableName.ViewPlusNesting);
            Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.ViewPlusNesting.Any());
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.ViewPlusNesting, 10);
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
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlEnumTableName.ViewPlusStorageMethods);
            Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.ViewPlusStorageMethods.Any());
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.ViewPlusStorageMethods, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
}