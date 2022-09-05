// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Components;

namespace BlazorCore.Models;

public partial class RazorPageModel : LayoutComponentBase
{
    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        RunActionsInitialized();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet();
    }

    #endregion
}
