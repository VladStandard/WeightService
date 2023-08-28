namespace WsLabelCore.Utils;

/// <summary>
/// WinForms расширения.
/// </summary>
#nullable enable
public static class WsFormComboBoxExtensions
{
    public static void SetEventWithItems(this System.Windows.Forms.ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
    {
        if (!sourceList.Any() || selectedIndex < 0) return;

        if (comboBox.InvokeRequired)
        {
            comboBox.Invoke(() =>
            {
                SetEventWithItemsWork(comboBox, eventHandler, sourceList, selectedIndex);
            });
        }
        else
            SetEventWithItemsWork(comboBox, eventHandler, sourceList, selectedIndex);
    }

    private static void SetEventWithItemsWork(this System.Windows.Forms.ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, 
        int selectedIndex = 0)
    {
        comboBox.SelectedIndexChanged -= eventHandler;

        int backupIndex = comboBox.SelectedIndex;
        comboBox.Items.Clear();
        if (sourceList.Any())
        {
            foreach (string source in sourceList)
            {
                comboBox.Items.Add(source);
            }
        }
        comboBox.SelectedIndex = selectedIndex <= 0
            ? backupIndex <= 0 ? 0 : backupIndex
            : selectedIndex < comboBox.Items.Count ? selectedIndex : 0;

        comboBox.SelectedIndexChanged += eventHandler;
        eventHandler.Invoke(comboBox, null);
    }
}