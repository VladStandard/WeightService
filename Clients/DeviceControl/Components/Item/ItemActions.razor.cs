namespace DeviceControl.Components.Item;

public partial class ItemActions : ComponentBase
{
    #region Public and private fields, properties, constructor
    
    [Inject] protected WsUserService UserService { get; set; } = default!;
    [Parameter] public EventCallback OnItemSave { get; set; }
    [Parameter] public EventCallback OnItemCancel { get; set; }
    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; } = default!;

    private bool GetDisableStatusOfSaveBtn => 
        !(User?.IsInRole(WsUserAccessStr.Write) == true && ButtonSettings.IsShowSave);
    private bool GetDisableStatusOfCancelBtn =>  
        !(User?.IsInRole(WsUserAccessStr.Read) == true && ButtonSettings.IsShowCancel);
    
    private ClaimsPrincipal? User { get; set; }
        
    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        await base.OnInitializedAsync();
    }

    #endregion
}
