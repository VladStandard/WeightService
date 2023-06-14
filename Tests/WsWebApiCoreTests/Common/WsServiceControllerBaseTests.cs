// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCoreTests.Common;

[TestFixture]
public class WsServiceControllerBaseTests
{
    [Test]
    public void Check_all_exists_plus_1c_fks()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsServiceControllerBase wsServiceController = new(WsTestsUtils.ContextManager.AccessManager.SessionFactory);
            // Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
            bool flag = wsServiceController.CheckExistsAllPlus1CFksDb();
            if (!flag)
                TestContext.WriteLine($"Run {nameof(Fill_plus_1c_fks)} first!");
            Assert.IsTrue(flag);
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.ContextManager.ContextPlu1CFk.GetList(), 0);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    // TODO: FIX HERE
    [Test]
    public void Fill_plus_1c_fks()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsServiceControllerBase wsServiceController = new(WsTestsUtils.ContextManager.AccessManager.SessionFactory);
            // Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
            List<Exception> exceptions = wsServiceController.FillPlus1CFksDb();
            if (exceptions.Any())
                TestContext.WriteLine(string.Join(Environment.NewLine, exceptions));
            else
                TestContext.WriteLine("FillPlus1CFksDb is ok.");
            Assert.IsTrue(!exceptions.Any());

            Assert.IsTrue(wsServiceController.CheckExistsAllPlus1CFksDb());
            WsTestsUtils.DataTests.PrintTopRecords(WsTestsUtils.ContextManager.ContextPlu1CFk.GetList(), 0);
        }, false, new() { WsEnumConfiguration.DevelopVS });//WsEnumConfiguration.ReleaseVS
    }
}