// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.TableScaleModels;
using System.Windows;
using WeightCore.Helpers;

namespace WeightCore.Gui.XamlPages;

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
        if (UserSessionHelper.Instance.PluPackage.Identity.IsNew())
        {
	        comboBoxPluPackage.SelectedIndex = 0;
        }
        else
        {
	        foreach (PluPackageModel pluPackage in UserSessionHelper.Instance.PluPackages)
	        {
		        if (UserSessionHelper.Instance.PluPackage.Equals(pluPackage))
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

    private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
    {
		Result = System.Windows.Forms.DialogResult.Cancel;
		OnClose?.Invoke(sender, e);
	}

    #endregion
}
