// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor.HotKeys;

namespace BlazorDeviceControl.Shared;

public partial class MainLayout
{
    #region Public and private fields, properties, constructor

    [Inject] public HotKeys? HotKeysItem { get; private set; }
    [Parameter] public EventCallback<ParameterView> SetParameters { get; set; }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Default);
        Items = new();
        UserSettings.SetupHotKeys(HotKeysItem);
        if (UserSettings.HotKeys != null)
            UserSettings.HotKeysContext = UserSettings.HotKeys.CreateContext()
                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
    }

    private static async void MemoryClearAsync(Radzen.MenuItemEventArgs args)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        GC.Collect();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SetParametersWithAction(new()
        {
            () => UserSettings.SetupAccessRights(),
            () =>
            {
	            AppSettings.SetupMemory();
	            //await AppSettings.Memory.OpenAsync(GuiRefreshAsync).ConfigureAwait(false);
	            AppSettings.Memory.OpenAsync().ConfigureAwait(false);
            }
		});

        //// Don't change it, because GuiRefreshAsync can get exception!
        //RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
        //    new Task(async () =>
        //    {
        //        AppSettings.SetupMemory();
        //        //await AppSettings.Memory.OpenAsync(GuiRefreshAsync).ConfigureAwait(false);
        //        await AppSettings.Memory.OpenAsync().ConfigureAwait(false);
        //    }), true);
    }

    #endregion
}
