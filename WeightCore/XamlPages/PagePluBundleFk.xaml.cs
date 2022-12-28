// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using System.Linq;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

namespace WeightCore.XamlPages;

/// <summary>
/// Interaction logic for PageSqlSettings.xaml
/// </summary>
public partial class PagePluBundleFk
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public PagePluBundleFk()
	{
		InitializeComponent();
		Setup();
        
		int i = 0;
		if (UserSession.BundleFk.IdentityIsNew)
		{
			comboBoxPluBundleFks.SelectedIndex = 0;
		}
		else
		{
			foreach (PluBundleFkModel pluBundleFk in UserSession.PluBundlesFks)
			{
				if (Equals(UserSession.BundleFk.IdentityValueUid, pluBundleFk.IdentityValueUid))
				{
					comboBoxPluBundleFks.SelectedIndex = i;
					break;
				}
				i++;
			}
		}
	}

	#endregion

	#region Public and private methods

	private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
	{
        //UserSession.BundleFk = UserSession.PluBundlesFks.First().BundleFk;
        UserSession.SetBundleFk(UserSession.BundleFk.IdentityValueUid);
        Result = System.Windows.Forms.DialogResult.OK;
		OnClose?.Invoke(sender, e);
	}

	private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
	{
        UserSession.SetBundleFk(null);
        Result = System.Windows.Forms.DialogResult.Cancel;
		OnClose?.Invoke(sender, e);
	}

	#endregion
}