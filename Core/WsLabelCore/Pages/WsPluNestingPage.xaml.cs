// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsPluNestingPage.xaml
/// </summary>
public partial class WsPluNestingPage : WsBasePage
{
    #region Public and private fields, properties, constructor
    
    public WsPluNestingViewModel ViewModel { get; }

    public WsPluNestingPage(WsPluNestingViewModel viewModel)
    {
		InitializeComponent();
        ViewModel = viewModel;
        //SetItemViewFromList(comboBoxPluNesting, ViewModel.ViewPluNestings, ViewModel.ViewPluNesting);
    }

    #endregion

    #region Public and private methods

    private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
	{
        // Didn't work! Go here: MainForm -> ActionSwitchPluNesting
        ViewModel.Result = DialogResult.OK;
		OnClose?.Invoke(sender, e);
	}

	private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
	{
        // Didn't work! Go here: MainForm -> ActionSwitchPluNesting
        ViewModel.Result = DialogResult.Cancel;
		OnClose?.Invoke(sender, e);
	}

	#endregion
}