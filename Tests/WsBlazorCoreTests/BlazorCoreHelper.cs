// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation;
using System;
using System.Threading;
using WsBlazorCore.Razors;
using WsSqlTableBase = WsStorageCore.Models.WsSqlTableBase;

namespace WsBlazorCoreTests;

public sealed class BlazorCoreHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static BlazorCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static BlazorCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsDataTestsHelper DataCore => WsDataTestsHelper.Instance;

    #endregion

    #region Public and private methods

    private IValidator GetStyleValidator<T>(T item) where T : CssStyleBase, new()
    {
        return item switch
        {
            CssStyleRadzenColumnModel => new CssStyleRadzenColumnValidator(),
            CssStyleTableHeadModel => new CssStyleTableHeadValidator(),
            CssStyleTableBodyModel => new CssStyleTableBodyValidator(),
            _ => throw new NotImplementedException()
        };
    }

    public void Model_GetRoutePathItem_IsNotEmpty<T>() where T : WsSqlTableBase, new()
    {
        // Arrange.
        RazorComponentBase razorComponent = new();
        T item = DataCore.CreateNewSubstitute<T>(true);
        // Act.
        string urlItem = razorComponent.GetRouteItemPath(item);
        TestContext.WriteLine(urlItem);
        // Assert.
        Assert.IsNotEmpty(urlItem);
    }

    public void Model_GetRoutePathSection_IsNotEmpty<T>() where T : WsSqlTableBase, new()
    {
        // Arrange.
        RazorComponentBase razorComponent = new();
        // Act.
        string urlSection = razorComponent.GetRouteSectionPath<T>();
        TestContext.WriteLine(urlSection);
        // Assert.
        Assert.IsNotEmpty(urlSection);
    }

    public RazorComponentBase CreateNewSubstituteRazorComponentBase()
    {
        RazorComponentBase razorPage = Substitute.For<RazorComponentBase>();
        razorPage.GetRouteItemPathShort<WsSqlDeviceScaleFkModel>();
        return razorPage;
    }

    #endregion
}