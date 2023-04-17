// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Wpf.Pages;

/// <summary>
/// Interaction logic for WpfPagePluNestingFk.xaml
/// </summary>
public partial class WpfPagePluNestingFk
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public WpfPagePluNestingFk()
	{
		InitializeComponent();
		SetPluNestingFk(comboBoxPluNestingFks);
    }

    #endregion

	#region Public and private methods

	private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
	{
        // Didn't work! Go here: MainForm -> ActionPluBundleFk
        //UserSession.SetBundleFk(UserSession.PluBundleFk.IdentityValueUid);
        Result = System.Windows.Forms.DialogResult.OK;
		OnClose?.Invoke(sender, e);
	}

	private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
	{
        // Didn't work! Go here: MainForm -> ActionPluBundleFk
        //UserSession.SetBundleFk(null);
        Result = System.Windows.Forms.DialogResult.Cancel;
		OnClose?.Invoke(sender, e);
	}

	#endregion
}