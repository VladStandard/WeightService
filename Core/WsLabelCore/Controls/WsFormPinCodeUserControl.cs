// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

/// <summary>
/// Контрол пин-кода.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsFormPinCodeUserControl : WsFormBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsPinCodeViewModel CastViewModel => (WsPinCodeViewModel)ViewModel;

    public WsFormPinCodeUserControl() : base(new WsPinCodeViewModel())
    {
        InitializeComponent();
        
        RefreshViewModel();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => CastViewModel.ToString();

    #endregion
}