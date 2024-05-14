using AngleSharp.Dom;
using Ws.Components.Source.UI.Select;

namespace Ws.Components.Tests.Select;

public class SelectSingleTests
{
    [Fact]
    public void RendersComponentWithProvidedItems()
    {
        // Arrange
        string[] items = ["first", "second", "third"];

        using TestContext ctx = new();
        ctx.JSInterop.SetupVoid("subscribeElementResize", _ => true);
        ctx.JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/AnchoredRegion/FluentAnchoredRegion.razor.js");
        IRenderedComponent<SelectSingle<string>> component = ctx.RenderComponent<SelectSingle<string>>(parameters => parameters
            .Add(p => p.Items, items));

        // Act
        IRenderedComponent<SelectTrigger> selectTrigger = component.FindComponent<SelectTrigger>();
        selectTrigger.Find("button").Click();

        // Assert
        component.FindAll("li").Count.Should().Be(items.Length);
    }

    [Fact]
    public void SetsSelectedItemOnClick()
    {
        // Arrange
        string[] items = ["first", "second", "third"];
        string? selectedItem = null;

        using TestContext ctx = new();
        ctx.JSInterop.SetupVoid("subscribeElementResize", _ => true);
        ctx.JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/AnchoredRegion/FluentAnchoredRegion.razor.js");
        IRenderedComponent<SelectSingle<string>> component = ctx.RenderComponent<SelectSingle<string>>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.SelectedItem, selectedItem)
            .Add(p => p.SelectedItemChanged, value => selectedItem = value));

        // Act
        IRenderedComponent<SelectTrigger> selectTrigger =component.FindComponent<SelectTrigger>();
        selectTrigger.Find("button").Click();

        IElement? secondItem = component.FindAll("li")[1].FirstElementChild;
        secondItem?.Click();

        // Assert
        component.Instance.SelectedItem.Should().Be("second");
        selectedItem.Should().Be("second");
    }

    [Fact]
    public void DoesNotSetSelectedItemIfComponentIsDisabled()
    {
        // Arrange
        using TestContext ctx = new();
        ctx.JSInterop.SetupVoid("subscribeElementResize", _ => true);
        ctx.JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/AnchoredRegion/FluentAnchoredRegion.razor.js");
        IRenderedComponent<SelectSingle<string>> component = ctx.RenderComponent<SelectSingle<string>>(parameters => parameters
            .Add(p => p.Items, [])
            .Add(p => p.IsDisabled, true));

        // Act
        IRenderedComponent<SelectTrigger> selectTrigger = component.FindComponent<SelectTrigger>();
        IElement button = selectTrigger.Find("button");

        // Assert
        button.IsDisabled().Should().BeTrue();
    }

    [Fact]
    public void FiltersItemsBasedOnSearchString()
    {
        // Arrange
        string[] items = ["first", "second", "third"];

        using TestContext ctx = new();
        ctx.JSInterop.SetupVoid("subscribeElementResize", _ => true);
        ctx.JSInterop.SetupModule("./_content/Microsoft.FluentUI.AspNetCore.Components/Components/AnchoredRegion/FluentAnchoredRegion.razor.js");
        IRenderedComponent<SelectSingle<string>> component = ctx.RenderComponent<SelectSingle<string>>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.IsFilterable, true));

        // Act
        IRenderedComponent<SelectTrigger> selectTrigger = component.FindComponent<SelectTrigger>();
        selectTrigger.Find("button").Click();

        IRenderedComponent<SelectSearch> selectSearch = component.FindComponent<SelectSearch>();
        IElement searchInput = selectSearch.Find("input");
        searchInput.Input("sec");

        // Assert
        selectSearch.Instance.Value.Should().Be("sec");
        searchInput.GetAttribute("value").Should().Be("sec");
        component.FindAll("li").Count.Should().Be(1);
    }
}