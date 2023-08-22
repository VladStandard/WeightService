// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.Profiles;

public partial class Profile : ComponentBase
{
    #region Public and private fields, properties, constructor

    [Inject] private WsLocalStorageService LocalStorage { get; set; } = default!;
    [Inject] private WsUserService UserService { get; set; } = default!;
    [Inject] private IHttpContextAccessor HttpContextAccess { get; set; } = default!;
    private HttpContext? HttpContext => HttpContextAccess?.HttpContext;
    private ClaimsPrincipal? User { get; set; }
    private List<WsEnumTypeModel<WsEnumLanguage>>? TemplateLanguages { get; set; }
    private List<WsEnumLanguage> Langs { get; set; }
    private int DefaultRowCount { get; set; }

    private string IpAddress =>
        HttpContext?.Connection.RemoteIpAddress is null
            ? string.Empty
            : HttpContext.Connection.RemoteIpAddress.ToString();

    public Profile()
    {
        Langs = new();
        foreach (WsEnumLanguage lang in Enum.GetValues(typeof(WsEnumLanguage)))
            Langs.Add(lang);
        TemplateLanguages = BlazorAppSettingsHelper.Instance.DataSourceDics.GetTemplateLanguages();
    }

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            string? rowCount = await LocalStorage.GetItem("DefaultRowCount");
            DefaultRowCount = int.TryParse(rowCount, out int parsedNumber) ? parsedNumber : 200;
            StateHasChanged();
        }
    }

    #endregion

    #region Public and private methods

    private async Task OnDefaultRowCountChanged()
    {
        if (DefaultRowCount == 0)
            DefaultRowCount = 200;
        await LocalStorage.SetItem("DefaultRowCount", DefaultRowCount.ToString());
    }
    
    private static string GetAccessRightsDescription(ClaimsPrincipal? user)
    {
        if (user == null)
            return string.Empty;
        string right = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
            .OrderByDescending(int.Parse).First();
        return (WsEnumAccessRights)int.Parse(right) switch
        {
            WsEnumAccessRights.Read => WsLocaleCore.Strings.AccessRightsRead,
            WsEnumAccessRights.Write => WsLocaleCore.Strings.AccessRightsWrite,
            WsEnumAccessRights.Admin => WsLocaleCore.Strings.AccessRightsAdmin,
            _ => WsLocaleCore.Strings.AccessRightsNone
        };
    }


    #endregion
}
