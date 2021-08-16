// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class EntitySave
    {
        #region Public and private fields and properties

        [Parameter] public BaseEntity Item { get; set; }

        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };
        DialogOptions closeButton = new DialogOptions() { CloseButton = true };
        DialogOptions noHeader = new DialogOptions() { NoHeader = true };
        DialogOptions disableBackdropClick = new DialogOptions() { DisableBackdropClick = true };
        DialogOptions fullScreen = new DialogOptions() { FullScreen = true, CloseButton = true };
        DialogOptions topCenter = new DialogOptions() { Position = DialogPosition.TopCenter };

        #endregion

        #region Public and private methods

        public async Task ItemSaveAsync1(DialogOptions options)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            Console.WriteLine("ItemSaveAsync 1");
            DialogService.Show<MudDialogExample>("Custom Options Dialog", options);

        }

        #endregion
    }
}