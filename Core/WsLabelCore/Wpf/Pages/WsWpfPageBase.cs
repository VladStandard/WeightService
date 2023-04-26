// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables;
using ComboBox = System.Windows.Controls.ComboBox;

namespace WsLabelCore.Wpf.Pages;

#nullable enable
public class WsWpfPageBase : System.Windows.Controls.UserControl
{
    #region Public and private fields, properties, constructor

    public WsUserSessionHelper UserSession { get; private set; }
    public System.Windows.Forms.DialogResult Result { get; protected set; }
    public RoutedEventHandler? OnClose { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected WsWpfPageBase()
    {
        UserSession = WsUserSessionHelper.Instance;
        Result = System.Windows.Forms.DialogResult.Cancel;
        OnClose = null;

        if (!Resources.Contains(nameof(UserSession)))
            Resources.Add(nameof(UserSession), WsUserSessionHelper.Instance);
        object context = FindResource(nameof(UserSession));
        if (context is WsUserSessionHelper userSession)
            UserSession = userSession;
        else
            UserSession = WsUserSessionHelper.Instance;
    }

    #endregion

    #region Public and private methods

    private void SetItemFromList<T>(ComboBox comboBox, List<T> list, T item) where T : WsSqlTableBase, new()
    {
        //PluNestingView.SetList(UserSession.PluScale.Plu);
        int i = 0;
        foreach (T it in list)
        {
            switch (item.IsIdentityUid)
            {
                case true:
                    if (Equals(item.IdentityValueUid, it.IdentityValueUid))
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                    break;
                default:
                    if (Equals(item.IdentityValueId, it.IdentityValueId))
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                    break;
            }
            i++;
        }
        if (comboBox.SelectedIndex == -1)
            comboBox.SelectedIndex = 0;
    }

    protected void SetLine(ComboBox comboBox) => 
        SetItemFromList(comboBox, UserSession.Scales, UserSession.Scale);

    protected void SetProductionFacility(ComboBox comboBox) => 
        SetItemFromList(comboBox, UserSession.ProductionFacilities, UserSession.ProductionFacility);
    
    protected void SetPluNestingFk(ComboBox comboBox) =>
        SetItemFromList(comboBox, UserSession.PluNestingView.List, UserSession.PluNestingView.Item);

    #endregion
}