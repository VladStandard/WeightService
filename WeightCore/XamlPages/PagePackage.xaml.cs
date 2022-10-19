// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using System.Linq;
using DataCore.Sql.TableScaleModels;

namespace WeightCore.XamlPages;

/// <summary>
/// Interaction logic for PageSqlSettings.xaml
/// </summary>
public partial class PagePackage
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public PagePackage()
	{
		InitializeComponent();
		Setup();
        
		int i = 0;
		if (UserSession.PluPackage.IdentityIsNew)
		{
			comboBoxPluPackage.SelectedIndex = 0;
		}
		else
		{
			foreach (PluPackageModel pluPackage in UserSession.PluPackages)
			{
				if (UserSession.PluPackage.Equals(pluPackage))
				{
					comboBoxPluPackage.SelectedIndex = i;
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
		Result = System.Windows.Forms.DialogResult.OK;
		OnClose?.Invoke(sender, e);
	}

	private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
	{
		UserSession.PluPackage = UserSession.PluPackages.First();
		Result = System.Windows.Forms.DialogResult.Cancel;
		OnClose?.Invoke(sender, e);
	}

	#endregion
}