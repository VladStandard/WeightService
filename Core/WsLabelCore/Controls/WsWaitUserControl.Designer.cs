namespace WsLabelCore.Controls;

partial class WsWaitUserControl
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // layoutPanel
            // 
            this.layoutPanel.AutoSize = true;
            this.layoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.ColumnCount = 1;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 1;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanel.Size = new System.Drawing.Size(900, 500);
            this.layoutPanel.TabIndex = 4;
            // 
            // WsWaitUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.layoutPanel);
            this.Name = "WsWaitUserControl";
            this.Size = new System.Drawing.Size(900, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private TableLayoutPanel layoutPanel;
}
