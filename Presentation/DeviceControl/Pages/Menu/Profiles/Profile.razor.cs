namespace DeviceControl.Pages.Menu.Profiles;

public partial class Profile : ComponentBase
{
    #region Public and private fields, properties, constructor

    [Inject] private LocalStorageService LocalStorage { get; set; } = default!;
    [Inject] private UserService UserService { get; set; } = default!;
    [Inject] private IHttpContextAccessor HttpContextAccess { get; set; } = default!;
    
    private HttpContext? HttpContext => HttpContextAccess?.HttpContext;
    private ClaimsPrincipal? User { get; set; }
    private List<EnumTypeModel<EnumLanguage>>? TemplateLanguages { get; set; }
    private List<EnumLanguage> Langs { get; set; }
    private int DefaultRowCount { get; set; }

    private string IpAddress =>
        HttpContext?.Connection.RemoteIpAddress is null
            ? string.Empty
            : HttpContext.Connection.RemoteIpAddress.ToString();

    public Profile()
    {
        Langs = new();
        foreach (EnumLanguage lang in Enum.GetValues(typeof(EnumLanguage)))
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
        return (EnumAccessRights)int.Parse(right) switch
        {
            EnumAccessRights.Read => LocaleCore.Strings.AccessRightsRead,
            EnumAccessRights.Write => LocaleCore.Strings.AccessRightsWrite,
            EnumAccessRights.Admin => LocaleCore.Strings.AccessRightsAdmin,
            _ => LocaleCore.Strings.AccessRightsNone
        };
    }
    
    #endregion
}
