// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;

namespace BlazorCore.Models
{
    public class ButtonSettingsEntity
    {
        #region Public and private fields and properties

        [Parameter] public bool IsShowCopy { get; set; }
        [Parameter] public bool IsShowDelete { get; set; }
        [Parameter] public bool IsShowEdit { get; set; }
        [Parameter] public bool IsShowMark { get; set; }
        [Parameter] public bool IsShowNew { get; set; }

        #endregion

        #region Constructor and destructor

        public ButtonSettingsEntity(bool isShowCopy, bool isShowDelete, bool isShowEdit, bool isShowMark, bool isShowNew)
        {
            IsShowCopy = isShowCopy;
            IsShowDelete = isShowDelete;
            IsShowEdit = isShowEdit;
            IsShowMark = isShowMark;
            IsShowNew = isShowNew;
        }

        public ButtonSettingsEntity() : this(false, false, false, false, false)
        {
            //
        }

        #endregion
    }
}
