// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Toolbelt.Blazor.HotKeys;

namespace MdmControlBlazor.Data
{
    public interface IBaseRazorEntity : IDisposable
    {
        #region Public and private fields and properties - Inject

        [Inject] BlazorSettingsEntity BlazorSettings { get; set; }
        [Inject] DialogService Dialog { get; set; }
        [Inject] HotKeys HotKeys { get; set; }
        [Inject] JsonAppSettingsEntity JsonAppSettings { get; set; }
        [Inject] IJSRuntime JsRuntime { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] NotificationService Notification { get; set; }
        [Inject] TooltipService Tooltip { get; set; }

        #endregion

        #region Public and private fields and properties - Parameter

        [Parameter] int FontSizeHeader { get; set; }
        [Parameter] int FontSize { get; set; }
        [Parameter] HotKeysContext HotKeysItem { get; set; }

        #endregion
    }
}