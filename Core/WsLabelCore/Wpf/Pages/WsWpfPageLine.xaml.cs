// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Wpf.Pages;

/// <summary>
/// Interaction logic for WsWpfPageLine.xaml
/// </summary>
public partial class WsWpfPageLine
{
    #region Public and private fields, properties, constructor

    public WsPageLineViewModel ViewModel { get; }

    public WsWpfPageLine(WsPageLineViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        
        SetProductionFacility(comboBoxArea);
        SetLine(comboBoxScale);
    }

    #endregion

    #region Public and private methods

    private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
    {
        // Didn't work! Go here: MainForm -> ActionDevice
        //UserSession.SetMain(UserSession.Scale.IdentityValueId, UserSession.ProductionFacility.Name);
        Result = System.Windows.Forms.DialogResult.OK;
        OnClose?.Invoke(sender, e);
    }

    private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
    {
        // Didn't work! Go here: MainForm -> ActionDevice
        //UserSession.SetMain(UserSession.Scale.IdentityValueId, string.Empty);
        Result = System.Windows.Forms.DialogResult.Cancel;
        OnClose?.Invoke(sender, e);
    }

    #endregion
}