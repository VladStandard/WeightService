using System.Windows.Forms;
using SystemColors=System.Drawing.SystemColors;

namespace Ws.LabelCore.Utils;

/// <summary>
/// WinForms утилиты.
/// </summary>
#nullable enable
public static class FormUtils
{
    #region Public and private methods

    /// <summary>
    /// Create a TableLayoutPanel.
    /// </summary>
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
    public static Button NewTableLayoutPanelButton(TableLayoutPanel layoutPanel, string name, int column, int row)
    {
        Button button = new()
        {
            Name = name,
            Enabled = true,
            Visible = true,
            BackColor = Color.Transparent,
            Dock = DockStyle.Fill,
            ForeColor = SystemColors.ControlText,
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
    public static void SetTableLayoutPanelRowStyles(TableLayoutPanel layoutPanel)
    {
        float size = (float)100 / layoutPanel.RowCount;
        layoutPanel.RowStyles.Clear();
        for (int i = 0; i < layoutPanel.RowCount; i++)
            layoutPanel.RowStyles.Add(new(SizeType.Percent, size));
    }

    #endregion
}