namespace WsWebApiCoreTests.Common;

[TestFixture]
public class WsServiceControllerBaseTests
{
    private static void PrintViewRecords<T>(IList<T> items) where T : class
    {
        Assert.That(items.Any(), Is.True, $"{WsLocaleCore.Tests.NoDataInDb}!");
        TestContext.WriteLine($"{WsLocaleCore.Tests.Print} {items.Count} {WsLocaleCore.Tests.Records}.");
        foreach (T item in items)
            TestContext.WriteLine(WsSqlQueries.TrimQuery(item.ToString() ?? string.Empty));
    }
    
    [Test]
    public void Fill_plus_1c_fks()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            if (WsTestsUtils.ContextManager.SqlCore.SessionFactory is null) 
                throw new ArgumentException(nameof(WsTestsUtils.ContextManager.SqlCore.SessionFactory));
            WsServiceUtilsUpdate.FillPlus1CFksDb();
            Assert.IsTrue(WsServiceUtilsCheck.CheckExistsAllPlus1CFksDb());
            PrintViewRecords(WsTestsUtils.ContextManager.Plu1CRepository.GetEnumerable(WsSqlCrudConfigFactory.GetCrudAll()).ToList());
        }, false, new() { WsEnumConfiguration.DevelopVS });
    }

    [Test]
    public void Fill_plus_1c_fks_next_check_all_exists()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            if (WsTestsUtils.ContextManager.SqlCore.SessionFactory is null)
                throw new ArgumentException(nameof(WsTestsUtils.ContextManager.SqlCore.SessionFactory));
            // Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
            bool flag = WsServiceUtilsCheck.CheckExistsAllPlus1CFksDb();
            if (!flag)
                TestContext.WriteLine($"Run {nameof(Fill_plus_1c_fks)} first!");
            Assert.IsTrue(flag);
            PrintViewRecords(WsTestsUtils.ContextManager.Plu1CRepository.GetEnumerable(WsSqlCrudConfigFactory.GetCrudAll()).ToList());
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test]
    public void Get_list_plus_1c_fks()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            if (WsTestsUtils.ContextManager.SqlCore.SessionFactory is null)
                throw new ArgumentException(nameof(WsTestsUtils.ContextManager.SqlCore.SessionFactory));
            WsSqlPluModel plu301 = WsTestsUtils.ContextManager.PluRepository.GetItemByNumber(301);
            TestContext.WriteLine($"{nameof(plu301)}: {plu301}");
            // Получить список связей обмена ПЛУ 1С по GUID_1C.
            List<WsSqlPlu1CFkModel> plus1CFks = WsServiceUtilsGet.GetPlus1CFksByGuid1C(plu301.Uid1C);
            Assert.IsTrue(plus1CFks.Any());
            PrintViewRecords(plus1CFks);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}