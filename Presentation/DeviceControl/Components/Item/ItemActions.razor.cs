namespace DeviceControl.Components.Item;

public partial class ItemActions : ComponentBase
{
    [Inject] protected UserService UserService { get; set; } = default!;
    [Parameter] public EventCallback OnItemSave { get; set; }
    [Parameter] public EventCallback OnItemCancel { get; set; }
    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; } = default!;

    private bool GetDisableStatusOfSaveBtn => 
        !(User?.IsInRole(UserAccessStr.Write) == true && ButtonSettings.IsShowSave);
    private bool GetDisableStatusOfCancelBtn =>  
        !(User?.IsInRole(UserAccessStr.Read) == true && ButtonSettings.IsShowCancel);
    
    private ClaimsPrincipal? User { get; set; }
        
    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
    }
}
