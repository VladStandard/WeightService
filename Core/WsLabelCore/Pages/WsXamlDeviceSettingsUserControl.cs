// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол настроек устройства.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlDeviceSettingsUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlDeviceSettingsViewModel ViewModel
    {
        get
        {
            if (Page.GetType() == typeof(WsXamlDeviceSettingsPage))
                return ((WsXamlDeviceSettingsPage)Page).ViewModelCast;
            //return Page.ViewModel as WsXamlDeviceSettingsViewModel ?? new();
            throw new ArgumentException(nameof(Page.ViewModel));
        }
    }

    public WsXamlDeviceSettingsUserControl() : base(WsEnumNavigationPage.DeviceSettings)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl() => ((WsXamlDeviceSettingsPage)Page).SetupViewModel(ViewModel);

    #endregion
}