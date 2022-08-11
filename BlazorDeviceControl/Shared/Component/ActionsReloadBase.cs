﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component;

public class ActionsReloadBase : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    [Parameter] public string Title { get; set; } = string.Empty;
    protected string ItemsCountResult => $"{LocaleCore.Strings.ItemsCount}: {ParentRazor?.Items?.Count ?? 0:### ### ###}";

	#endregion
}
