// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableRefModels.Plus1CFk;
using WsStorageCore.Tables.TableScaleModels.Plus;
using WsStorageCore.Utils;
using WsWebApiCore.Utils;

namespace WsWebApiCoreTests.Common;

[TestFixture]
public class WsServiceControllerBaseTests
{
    protected static void PrintViewRecords<T>(List<T> items) where T : class
    {
        Assert.That(items.Any(), Is.True, "No data in database!!!");
        TestContext.WriteLine($"Print {items.Count} records.");
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
            PrintViewRecords(WsTestsUtils.ContextManager.Plu1CRepository.GetList());
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
            PrintViewRecords(WsTestsUtils.ContextManager.Plu1CRepository.GetList());
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