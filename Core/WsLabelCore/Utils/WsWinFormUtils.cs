// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Utils;
#nullable enable
public static class WsWinFormUtils
{
    #region Public and private methods

    /// <summary>
    /// Create a TableLayoutPanel.
    /// </summary>
    /// <param name="layoutPanelMain"></param>
    /// <param name="name"></param>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <param name="columnSpan"></param>
    /// <param name="tabIndex"></param>
    /// <returns></returns>
    public static TableLayoutPanel NewTableLayoutPanel(TableLayoutPanel layoutPanelMain, string name,
        int column, int row, int columnSpan, int tabIndex)
    {
        TableLayoutPanel tableLayoutPanel = new()
        {
            Name = name,
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 1,
            TabIndex = tabIndex,
        };
        tableLayoutPanel.ColumnStyles.Clear();
        tableLayoutPanel.ColumnStyles.Add(new(SizeType.Percent, 100F));
        tableLayoutPanel.RowStyles.Clear();
        tableLayoutPanel.RowStyles.Add(new(SizeType.Percent, 100F));
        layoutPanelMain.Controls.Add(tableLayoutPanel, column, row);
        layoutPanelMain.SetColumnSpan(tableLayoutPanel, columnSpan);
        return tableLayoutPanel;
    }

    /// <summary>
    /// Create a Button.
    /// </summary>
    /// <param name="layoutPanel"></param>
    /// <param name="name"></param>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    public static Button NewTableLayoutPanelButton(TableLayoutPanel layoutPanel, string name, int column, int row)
    {
        Button button = new()
        {
            Name = name,
            Enabled = true,
            Visible = true,
            BackColor = Color.Transparent,
            Dock = DockStyle.Fill,
            ForeColor = System.Drawing.SystemColors.ControlText,
            Margin = new(5, 2, 5, 2),
            Size = new(100, 100),
            UseVisualStyleBackColor = false,
            TabIndex = 100 + column,
        };
        layoutPanel.Controls.Add(button, column - 1, row > 0 ? row : 0);
        return button;
    }

    /// <summary>
    /// Set the ColumnStyles for TableLayoutPanel.
    /// </summary>
    /// <param name="layoutPanel"></param>
    public static void SetTableLayoutPanelColumnStyles(TableLayoutPanel layoutPanel)
    {
        float columnSize = (float)100 / layoutPanel.ColumnCount;
        layoutPanel.ColumnStyles.Clear();
        for (int i = 0; i < layoutPanel.ColumnCount; i++)
        {
            layoutPanel.ColumnStyles.Add(new(SizeType.Percent, columnSize));
        }
    }

    /// <summary>
    /// Set the ColumnStyles for TableLayoutPanel.
    /// </summary>
    /// <param name="layoutPanel"></param>
    public static void SetTableLayoutPanelRowStyles(TableLayoutPanel layoutPanel)
    {
        float size = (float)100 / layoutPanel.RowCount;
        layoutPanel.RowStyles.Clear();
        for (int i = 0; i < layoutPanel.RowCount; i++)
        {
            layoutPanel.RowStyles.Add(new(SizeType.Percent, size));
        }
    }

    #endregion
}