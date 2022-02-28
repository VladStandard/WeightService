// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ActionsReload
    {
        #region Public and private fields and properties

        [Parameter] public string Title { get; set; } = string.Empty;
        [Parameter] public EventCallback<ParameterView> SetParameters { get; set; }
        [Parameter] public bool IsShowItemsCount { get; set; }
        public string ItemsCountResult => $"{LocalizationCore.Strings.Main.ItemsCount}: {(Items == null ? 0 : Items.Count):### ### ###}";

        #endregion

        #region Public and private methods

        private void OnChange(bool value, string name)
        {
            switch (name)
            {
                case nameof(IsShowMarkedItems):
                    IsShowMarkedItems = value;
                    break;
            }
            //SetParameters.InvokeAsync();
        }

        #endregion

    }
}
