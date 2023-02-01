// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCoreTests;
using TestContext = Bunit.TestContext;

namespace DeviceControlBlazorTests.Razors.SectionComponents.Others;

[TestFixture]
internal class SectionAccessTests : BunitTestContext
{
    [Test]
    public void Foo_Test()
    {
        // Arrange.
        using TestContext ctx = new();
        //RenderComponent<SectionAccess> cut = ctx.RenderComponent<SectionAccess>();
        // Act.
        //cut.Find("RazorSectionReload");
        //cut.Find("MudPaper");
        //cut.Find("MudSimpleTable");
        //cut.Find("tr");
        //cut.Find("td");
        //cut.Find("RadzenButton").Click();
        // Assert.
        //cut.Find("MudPaper");//.MarkupMatches("<p>Current count: 1</p>");
    }
}
