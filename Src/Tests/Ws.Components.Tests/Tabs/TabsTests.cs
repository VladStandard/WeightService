using AngleSharp.Dom;

namespace Ws.Components.Tests.Tabs;

public class TabsTests : TestContext
{
    [Fact]
    public void Tab_Shows_Correct_Active_Tab()
    {
        using TestContext ctx = new();
        IRenderedComponent<TabsComponent> cut = ctx.RenderComponent<TabsComponent>();

        IElement tabContent = cut.Find("p");
        tabContent.InnerHtml.Should().Be("First tab");
    }

    [Fact]
    public async Task Tab_Changes_Active_Tab()
    {
        using TestContext ctx = new();
        IRenderedComponent<TabsComponent> cut = ctx.RenderComponent<TabsComponent>();

        await cut.InvokeAsync(() => cut.Instance.SetCurrentTabId("second"));

        IElement tabContent = cut.Find("p");
        tabContent.InnerHtml.Should().Be("Second tab");
    }
}
