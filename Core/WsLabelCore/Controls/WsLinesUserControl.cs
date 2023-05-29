// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

/// <summary>
/// Контрол смены линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsLinesUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsLinesViewModel CastViewModel => (WsLinesViewModel)ViewModel;

    public WsLinesUserControl() : base(new WsLinesViewModel())
    {
        InitializeComponent();

        RefreshViewModel();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => CastViewModel.ToString();

    #endregion
}