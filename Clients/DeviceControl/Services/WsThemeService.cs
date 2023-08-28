// Статьи по теме
// https://blazor.radzen.com/themes
// https://forum.radzen.com/t/how-to-implement-dark-light-theme/13054
// https://github.com/radzenhq/radzen-blazor/blob/master/RadzenBlazorDemos/Services/ThemeService.cs
// https://github.com/radzenhq/radzen-blazor/blob/f4c776f10ec6bce372452b8d302c377c3785d712/RadzenBlazorDemos/Shared/MainLayout.razor#L33

using System.Collections.Specialized;
using System.Web;

namespace DeviceControl.Services;

public sealed class WsThemeService
{
    public class Theme
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Primary { get; set; }
        public string Secondary { get; set; }
        public string Base { get; set; }
        public string Header { get; set; }
        public string Sidebar { get; set; }
        public string Content { get; set; }
        public string TitleText { get; set; }
        public string ContentText { get; set; }
        public bool Premium { get; set; }
    }

    public static readonly Theme[] FreeThemes = new[]
    {
        new Theme {
            Text = "Default",
            Value = "default",
            Primary = "#ff6d41",
            Secondary = "#35a0d7",
            Base = "#f6f7fa",
            Header = "#ffffff",
            Sidebar = "#3a474d",
            Content = "#ffffff",
            TitleText = "#28363c",
            ContentText = "#95a4a8"
        },
        new Theme {
            Text = "Standard",
            Value = "standard",
            Primary = "#1151f3",
            Secondary = "rgba(17, 81, 243, 0.16)",
            Base = "#f4f5f9",
            Header = "#ffffff",
            Sidebar = "#262526",
            Content = "#ffffff",
            TitleText = "#262526",
            ContentText = "#afafb2"
        },
        new Theme {
            Text = "Dark",
            Value="dark",
            Primary = "#ff6d41",
            Secondary = "#35a0d7",
            Base = "#28363c",
            Header = "#38474e",
            Sidebar = "#38474e",
            Content = "#38474e",
            TitleText = "#ffffff",
            ContentText = "#a8b4b8"
        },
        new Theme {
            Text = "Humanistic",
            Value = "humanistic",
            Primary = "#d64d42",
            Secondary = "#3ba5fc",
            Base = "#f3f5f7",
            Header = "#ffffff",
            Sidebar = "#30445f",
            Content = "#ffffff",
            TitleText = "#2b3a50",
            ContentText = "#7293b6"
        },
        new Theme {
            Text = "Software",
            Value = "software",
            Primary = "#598087",
            Secondary = "#80a4ab",
            Base = "#f6f7fa",
            Header = "#ffffff",
            Sidebar = "#3a474d",
            Content = "#ffffff",
            TitleText = "#28363c",
            ContentText = "#95a4a8"
        }
    };

    private const string DefaultTheme = "Default";
    private const string QueryParameter = "theme";

    public string CurrentTheme { get; private set; } = DefaultTheme;

    public void Initialize(NavigationManager navigationManager)
    {
        Uri uri = new(navigationManager.ToAbsoluteUri(navigationManager.Uri).ToString());
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
        string? value = query.Get(QueryParameter);

        if (FreeThemes.Any(theme => theme.Value == value))
        {
            CurrentTheme = value;
        }
    }

    public void Change(NavigationManager navigationManager, string theme)
    {
        string url = navigationManager.GetUriWithQueryParameter(QueryParameter, theme);
        navigationManager.NavigateTo(url, true);
    }
}
