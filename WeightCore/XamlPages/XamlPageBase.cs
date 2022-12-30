// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.Scales;
using System.Windows;
using System.Windows.Controls;
using WeightCore.Helpers;

namespace WeightCore.XamlPages;
#nullable enable

public class XamlPageBase : UserControl
{
	#region Public and private fields, properties, constructor

	public UserSessionHelper UserSession { get; private set; }
	public System.Windows.Forms.DialogResult Result { get; protected set; }
	public RoutedEventHandler? OnClose { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	protected XamlPageBase()
	{
		UserSession = UserSessionHelper.Instance;
		Result = System.Windows.Forms.DialogResult.Cancel;
		OnClose = null;
        
        if (!Resources.Contains(nameof(UserSession)))
            Resources.Add(nameof(UserSession), UserSessionHelper.Instance);
        object context = FindResource(nameof(UserSession));
        if (context is UserSessionHelper userSession)
            UserSession = userSession;
        else
            UserSession = UserSessionHelper.Instance;
    }

    #endregion

    #region Public and private methods

    protected void SetScale(ComboBox comboBox)
    {
        int i = 0;
        foreach (ScaleModel scale in UserSession.Scales)
        {
            if (Equals(UserSession.Scale.IdentityValueId, scale.IdentityValueId))
            {
                comboBox.SelectedIndex = i;
                break;
            }
            i++;
        }
        if (comboBox.SelectedIndex == -1)
            comboBox.SelectedIndex = 0;
    }

    protected void SetProductionFacility(ComboBox comboBox)
    {
        int i = 0;
        foreach (ProductionFacilityModel productionFacility in UserSession.ProductionFacilities)
        {
            if (Equals(UserSession.ProductionFacility.IdentityValueId, productionFacility.IdentityValueId))
            {
                comboBox.SelectedIndex = i;
                break;
            }
            i++;
        }
        if (comboBox.SelectedIndex == -1)
            comboBox.SelectedIndex = 0;
    }

    protected void SetPluBundleFks(ComboBox comboBox)
    {
        int i = 0;
        foreach (PluNestingFkModel pluNestingFk in UserSession.PluNestingFks)
        {
            if (Equals(UserSession.PluNestingFk.IdentityValueUid, pluNestingFk.IdentityValueUid))
            {
                comboBox.SelectedIndex = i;
                break;
            }
            i++;
        }
        if (comboBox.SelectedIndex == -1)
            comboBox.SelectedIndex = 0;
    }

    #endregion
}