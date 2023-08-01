// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Components.Item;

public partial class ItemActions : ComponentBase
{
    #region Public and private fields, properties, constructor
    
    [Inject] protected UserService UserService { get; set; }
    [Parameter] public EventCallback OnItemSave { get; set; }
    [Parameter] public EventCallback OnItemCancel { get; set; }

    [Parameter] public ButtonSettingsModel ButtonSettings { get; set; }

    private bool GetDisableStatusOfSaveBtn => 
        !(User?.IsInRole(UserAccessStr.Write) == true && ButtonSettings.IsShowSave);
    private bool GetDisableStatusOfCancelBtn =>  
        !(User?.IsInRole(UserAccessStr.Read) == true && ButtonSettings.IsShowCancel);
    
    private ClaimsPrincipal? User { get; set; }
        
    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        await base.OnInitializedAsync();
    }

    #endregion
}