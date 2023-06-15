// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsBlazorCoreTests;

[TestFixture]
public sealed class CssStylesTests
{
    #region Public and private methods

    [Test]
    public void Check_false_validate_css_style_radzen_column()
    {
        CssStyleRadzenColumnModel item = Substitute.For<CssStyleRadzenColumnModel>();

        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, false);

        item.Width = "";

        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, false);
    }

    [Test]
    public void Check_true_validate_css_style_radzen_column()
    {
        CssStyleRadzenColumnModel item = Substitute.For<CssStyleRadzenColumnModel>();

        item.Width = "10%";
        
        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, true);
    }

    [Test]
    public void Check_false_validate_css_style_table_body()
    {
        CssStyleTableBodyModel item = Substitute.For<CssStyleTableBodyModel>();
        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, false);

        item.IdentityName = WsSqlEnumFieldIdentity.Empty;
        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, false);
    }

    [Test]
    public void Check_true_validate_css_style_table_body()
    {
        CssStyleTableBodyModel item = Substitute.For<CssStyleTableBodyModel>();

        item.IdentityName = WsSqlEnumFieldIdentity.Uid;
        item.IsShowMarked = true;

        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, true);
    }

    [Test]
    public void Check_false_validate_css_style_table_head()
    {
        CssStyleTableHeadModel item = Substitute.For<CssStyleTableHeadModel>();
        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, false);

        item.Color = "";
        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, false);
    }

    [Test]
    public void Check_true_validate_css_style_table_head()
    {
        CssStyleTableHeadModel item = Substitute.For<CssStyleTableHeadModel>();

        item.ColumnsWidths = new() { 20, 30, 20, 30 };
        item.ColumnsTitles = item.GetColumnsTitles();
        item.Color = "blue";
        item.FontWeight = "bold";
        item.TextAlign = "center";

        WsTestsUtils.DataTests.AssertBlazorCssStylesValidate(item, true);
    }

    #endregion
}