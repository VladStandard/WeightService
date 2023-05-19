// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.Helpers;

[TestFixture]
public sealed class WsSqlContextCacheHelperTests
{
    [Test]
    public void Get_cache_view_plus_nesting()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlTableName.ViewPlusNesting);
            Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.ViewPlusNesting.Any());
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.ViewPlusNesting, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void Get_cache_view_plus_scales()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlTableName.ViewPlusScales);
            Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.ViewPlusScales.Any());
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.ViewPlusScales, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void Get_cache_view_plus_scales_current()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleModel> scales = WsTestsUtils.DataTests.ContextManager.ContextScale.GetList();
            Assert.IsTrue(scales.Any());

            bool isPrintFirst = false;
            foreach (WsSqlScaleModel scale in scales)
            {
                WsTestsUtils.DataTests.ContextCache.LoadCurrentViewPlusScales((ushort)scale.IdentityValueId);
                Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.CurrentViewPlusScales.Any());
                if (!isPrintFirst)
                {
                    isPrintFirst = true;
                    WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.CurrentViewPlusScales, 10);
                }
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test]
    public void Get_cache_view_plus_storage_methods()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsTestsUtils.DataTests.ContextCache.Load(WsSqlTableName.ViewPlusStorageMethods);
            Assert.IsTrue(WsTestsUtils.DataTests.ContextCache.ViewPlusStorageMethods.Any());
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.DataTests.ContextCache.ViewPlusStorageMethods, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}