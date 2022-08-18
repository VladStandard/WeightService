// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;

namespace BlazorDeviceControl.Shared;

public partial class NavMenu : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private bool CollapseNavMenu { get; set; } = true;
    private bool IsDebug { get; set; } =
#if DEBUG
        true;
#else
        false;
#endif

    #endregion

    #region Public and private methods

    private void ToggleNavMenu()
    {
        CollapseNavMenu = !CollapseNavMenu;
    }

    public NavMenu()
    {
        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
        Items = new();
    }

    #endregion

    #region Public and private methods

    //protected override void OnParametersSet()
    //{
    //	base.OnParametersSet();
    //       SetParametersWithAction(new()
    //       {
    //           () =>
    //           {
    //			//
    //		}
    //       });
    //   }

    #endregion
}
