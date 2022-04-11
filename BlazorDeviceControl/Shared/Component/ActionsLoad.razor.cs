// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ActionsLoad
    {
        #region Public and private fields and properties

        [Parameter] public ShareEnums.ActionLoad DataLoadItem { get; set; }
        [Parameter] public bool IsShowProgress { get; set; }

        #endregion

        #region Constructor and destructor

        public ActionsLoad() : base()
        {
            //
        }

        #endregion
    }
}
