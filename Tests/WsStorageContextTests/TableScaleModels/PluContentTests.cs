// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
public sealed class PluContentTests
{
    [Test]
    public void Validate_and_print_top_plus_by_number()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<PluModel> plus = WsTestsUtils.DataTests.ContextManager.ContextPlu.GetListByNumber(301);
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");
            WsTestsUtils.DataTests.PrintTopRecords(plus, 10, true, true);
        }, false, new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS });
    }
	
    [Test]
    public void Validate_and_print_top_plus_by_numbers()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<PluModel> plus = WsTestsUtils.DataTests.ContextManager.ContextPlu.GetListByNumbers(new() { 301, 1301 });
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");
            WsTestsUtils.DataTests.PrintTopRecords(plus, 10, true, true);
        }, false, new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS });
    }
	
    [Test]
    public void Validate_and_print_top_plus_by_min_max_numbers()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<PluModel> plus = WsTestsUtils.DataTests.ContextManager.ContextPlu.GetListByNumbers(300, 399);
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");
            WsTestsUtils.DataTests.PrintTopRecords(plus, 10, true, true);
        }, false, new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS });
    }
	
    [Test]
    public void Validate_and_print_top_plus_by_uid1c()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<PluModel> plus = WsTestsUtils.DataTests.ContextManager.ContextPlu.GetListByUid1c(Guid.Parse("B912B17D-E328-11EC-BD1B-00155D8A460F"));
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");
            WsTestsUtils.DataTests.PrintTopRecords(plus, 10, true, true);
        }, false, new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS });
	}
}