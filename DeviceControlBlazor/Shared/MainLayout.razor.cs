﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControlBlazor.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorCore.Utils;
using Toolbelt.Blazor.HotKeys;

namespace DeviceControlBlazor.Shared
{
    public partial class MainLayout
    {
        #region Public and private fields and properties - Inject

        [Inject] public AuthenticationStateProvider AuthenticationState { get; private set; }
        [Inject] public JsonAppSettingsEntity JsonAppSettings { get; private set; }
        [Inject] public HotKeys HotKeysItem { get; private set; }

        #endregion

        #region Public and private methods

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);

            await RunTasks(LocalizationStrings.DeviceControl.MethodOnInitializedAsync, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    //new Task(delegate {
                    new(() => {
                        AppSettings.Setup(AuthenticationState, JsonAppSettings, HotKeysItem);
                        if (AppSettings.HotKeysItem != null)
                            AppSettings.HotKeysContextItem = AppSettings.HotKeysItem.CreateContext()
                                .Add(ModKeys.Alt, Keys.Num1, HotKeysMenuRoot, "Menu root");
                    }),
                }, GuiRefreshAsync).ConfigureAwait(false);

            await RunTasks(LocalizationStrings.DeviceControl.MethodOnInitializedAsync, "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(() => {
                        AppSettings.MemoryOpen(GuiRefreshAsync);
                    }),
                }, null, false).ConfigureAwait(false);
        }

        #endregion
    }
}