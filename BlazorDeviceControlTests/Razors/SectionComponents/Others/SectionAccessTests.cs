// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using AngleSharp.Dom;
using BlazorCore.Services;
using BlazorCoreTests;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace DeviceControlBlazorTests.Razors.SectionComponents.Others;

[TestFixture]
internal class SectionAccessTests : BunitTestContext
{
    [Test]
    public void Foo_Test()
    {
        DataCore.AssertAction(() => {
            //SectionAccess - RazorSectionReload - RadzenButton - Icon="refresh"

            // Arrange
            // Act
            IRenderedFragment cut = TestContext.RenderComponent<SectionAccess>();

            // Assert
            //cut.MarkupMatches("");
            IElement razorSectionReload = cut.Find("RazorSectionReload");
        }, true);
    }
}
