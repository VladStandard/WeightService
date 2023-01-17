namespace ScalesUI.Controls;

partial class PluUserControl
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
            this.buttonRightRoll = new System.Windows.Forms.Button();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.layoutPanelActions = new System.Windows.Forms.TableLayoutPanel();
            this.labelCurrentPage = new System.Windows.Forms.Label();
            this.buttonLeftRoll = new System.Windows.Forms.Button();
            this.layoutPanel.SuspendLayout();
            this.layoutPanelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRightRoll
            // 
            this.buttonRightRoll.BackColor = System.Drawing.Color.Transparent;
            this.buttonRightRoll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRightRoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRightRoll.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRightRoll.Location = new System.Drawing.Point(598, 2);
            this.buttonRightRoll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRightRoll.Name = "buttonRightRoll";
            this.buttonRightRoll.Size = new System.Drawing.Size(296, 92);
            this.buttonRightRoll.TabIndex = 6;
            this.buttonRightRoll.Text = ">>";
            this.buttonRightRoll.UseVisualStyleBackColor = false;
            this.buttonRightRoll.Click += new System.EventHandler(this.ButtonNextRoll_Click);
            // 
            // layoutPanel
            // 
            this.layoutPanel.AutoSize = true;
            this.layoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.ColumnCount = 3;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanel.Controls.Add(this.layoutPanelActions, 0, 2);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 3;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutPanel.Size = new System.Drawing.Size(900, 500);
            this.layoutPanel.TabIndex = 4;
            // 
            // layoutPanelActions
            // 
            this.layoutPanelActions.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelActions.ColumnCount = 3;
            this.layoutPanel.SetColumnSpan(this.layoutPanelActions, 3);
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanelActions.Controls.Add(this.buttonRightRoll, 2, 0);
            this.layoutPanelActions.Controls.Add(this.labelCurrentPage, 1, 0);
            this.layoutPanelActions.Controls.Add(this.buttonLeftRoll, 0, 0);
            this.layoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelActions.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutPanelActions.Location = new System.Drawing.Point(2, 402);
            this.layoutPanelActions.Margin = new System.Windows.Forms.Padding(2);
            this.layoutPanelActions.Name = "layoutPanelActions";
            this.layoutPanelActions.RowCount = 1;
            this.layoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelActions.Size = new System.Drawing.Size(896, 96);
            this.layoutPanelActions.TabIndex = 5;
            // 
            // labelCurrentPage
            // 
            this.labelCurrentPage.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCurrentPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCurrentPage.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrentPage.Location = new System.Drawing.Point(299, 1);
            this.labelCurrentPage.Margin = new System.Windows.Forms.Padding(1);
            this.labelCurrentPage.Name = "labelCurrentPage";
            this.labelCurrentPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelCurrentPage.Size = new System.Drawing.Size(296, 94);
            this.labelCurrentPage.TabIndex = 5;
            this.labelCurrentPage.Text = "labelCurrentPage";
            this.labelCurrentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLeftRoll
            // 
            this.buttonLeftRoll.BackColor = System.Drawing.Color.Transparent;
            this.buttonLeftRoll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeftRoll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLeftRoll.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLeftRoll.Location = new System.Drawing.Point(2, 2);
            this.buttonLeftRoll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLeftRoll.Name = "buttonLeftRoll";
            this.buttonLeftRoll.Size = new System.Drawing.Size(294, 92);
            this.buttonLeftRoll.TabIndex = 4;
            this.buttonLeftRoll.Text = "<<";
            this.buttonLeftRoll.UseVisualStyleBackColor = false;
            this.buttonLeftRoll.Click += new System.EventHandler(this.ButtonPreviousRoll_Click);
            // 
            // PluUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.layoutPanel);
            this.Name = "PluUserControl";
            this.Size = new System.Drawing.Size(900, 500);
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanelActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button buttonRightRoll;
    private TableLayoutPanel layoutPanel;
    private TableLayoutPanel layoutPanelActions;
    private Label labelCurrentPage;
    private Button buttonLeftRoll;
}
