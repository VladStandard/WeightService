// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
public sealed class PluNestingFkContentTests
{
    [Test]
    public void Validate_and_print_top_plus_nesting_fk_by_number()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluNestingFkModel> plusNestingFks = WsTestsUtils.DataTests.ContextManager.ContextPluNesting.GetListByNumber(312);
            TestContext.WriteLine($"{nameof(plusNestingFks)}.{nameof(plusNestingFks.Count)}: {plusNestingFks.Count}");
            WsTestsUtils.DataTests.PrintTopRecords(plusNestingFks, 10, false, true);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }

    [Test]
    public void Validate_and_print_top_plus_nesting_fk_by_uid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluNestingFkModel> plusNestingFks = WsTestsUtils.DataTests.ContextManager.ContextPluNesting.GetListByUid(Guid.Parse("030914a7-9d01-11eb-bcd7-00155d8a4603"));
            TestContext.WriteLine($"{nameof(plusNestingFks)}.{nameof(plusNestingFks.Count)}: {plusNestingFks.Count}");
            WsTestsUtils.DataTests.PrintTopRecords(plusNestingFks, 10, false, true);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
	}
}