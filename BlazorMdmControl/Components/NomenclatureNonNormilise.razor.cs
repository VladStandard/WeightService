// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MdmControlBlazor.Utils;
using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor.HotKeys;

namespace MdmControlBlazor.Components
{
    public partial class NomenclatureNonNormilise
    {
        #region Public and private fields and properties

        [Parameter]
        public int? ItemId { get; set; }

        #endregion

        #region Public and private methods - Hotkeys

        private async Task HotKeysTabAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        }

        #endregion

        #region Public and private methods

        private async Task GuiRefreshAsync()
        {
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);

            HotKeysItem = HotKeys.CreateContext().Add(ModKeys.None, Keys.Tab, HotKeysTabAsync, LocalizationStrings.TableTab);
            BlazorSettings.Setup(JsonAppSettings, Notification, Dialog, Navigation, Tooltip, JsRuntime);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            ItemId ??= 0;
            await GetDataAsync().ConfigureAwait(false);
        }

        private void GetData()
        {
            //
        }

        private async Task GetDataAsync()
        {
            Task task = new Task(GetData);
            await BlazorSettings.RunTasks(LocalizationStrings.TableRead,
                "", LocalizationStrings.DialogResultFail, "",
                new List<Task> { task }, GuiRefreshAsync).ConfigureAwait(false);
        }

        private void OnChange(object value, string name)
        {
            StateHasChanged();
        }

        #endregion
    }
}