using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;

namespace MdmControlBlazor.Data
{
    public class BaseRazorEntity : LayoutComponentBase, IBaseRazorEntity
    {
        #region Public and private fields and properties - Inject

        [Inject] public AuthenticationStateProvider AuthenticationState { get; set; }
        //[Inject] public BlazorSettingsEntity BlazorSettings { get; set; }
        [Inject] public Data.JsonAppSettingsEntity JsonAppSettings { get; set; }
        [Inject] public DialogService Dialog { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public NotificationService Notification { get; set; }
        [Inject] public TooltipService Tooltip { get; set; }

        #endregion

        #region Public and private fields and properties - Parameter

        [Parameter] public int FontSizeHeader { get; set; }
        [Parameter] public int FontSize { get; set; }
        
        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dialog.Dispose();
            Tooltip.Dispose();
        }

        #endregion
    }
}