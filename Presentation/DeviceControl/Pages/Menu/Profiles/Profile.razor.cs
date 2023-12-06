using Ws.Shared.Enums;

namespace DeviceControl.Pages.Menu.Profiles;

public partial class Profile : ComponentBase
{
    #region Public and private fields, properties, constructor

    [Inject] private LocalStorageService LocalStorage { get; set; } = default!;
    [Inject] private UserService UserService { get; set; } = default!;
    [Inject] private IHttpContextAccessor HttpContextAccess { get; set; } = default!;
    
    private HttpContext? HttpContext => HttpContextAccess?.HttpContext;
    private ClaimsPrincipal? User { get; set; }
    private int DefaultRowCount { get; set; }

    private string IpAddress =>
        HttpContext?.Connection.RemoteIpAddress is null
            ? string.Empty
            : HttpContext.Connection.RemoteIpAddress.ToString();

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
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
            EnumAccessRights.Read => Locale.AccessRightsRead,
            EnumAccessRights.Write => Locale.AccessRightsWrite,
            EnumAccessRights.Admin => Locale.AccessRightsAdmin,
            _ => Locale.AccessRightsNone
        };
    }
    
    #endregion
}
