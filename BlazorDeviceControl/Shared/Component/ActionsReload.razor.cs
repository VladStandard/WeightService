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
        public string ItemsCountResult => $"{LocalizationCore.Strings.Main.ItemsCount}: {(Items == null ? 0 : Items.Count):### ### ###}";
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public ActionsReload()
        {
            Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            lock (_locker)
            {
                if (ParentRazor != null)
                    IsShowMarkedItems = ParentRazor.IsShowMarkedItems;
            }
        }

        #endregion
    }
}
