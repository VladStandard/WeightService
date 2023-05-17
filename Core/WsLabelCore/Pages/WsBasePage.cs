// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
#nullable enable

using ComboBox = System.Windows.Controls.ComboBox;

namespace WsLabelCore.Pages;

public class WsBasePage : System.Windows.Controls.UserControl
{
    #region Public and private fields, properties, constructor

    public RoutedEventHandler? OnClose { get; set; }

    protected WsBasePage()
    {
        OnClose = null;
    }

    #endregion

    #region Public and private methods

    protected void SetItemFromList<T>(ComboBox comboBox, List<T> list, T item) where T : WsSqlTableBase, new()
    {
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

    protected void SetItemViewFromList<T>(ComboBox comboBox, List<T> list, T item) where T : WsSqlViewBase, new()
    {
        int i = 0;
        foreach (T it in list)
        {
            switch (item.Identity.IsUid)
            {
                case true:
                    if (Equals(item.Identity.Uid, it.Identity.Uid))
                    {
                        comboBox.SelectedIndex = i;
                        return;
                    }
                    break;
                default:
                    if (Equals(item.Identity.Id, it.Identity.Id))
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

    #endregion
}