namespace WsLabelCore.Controls;

partial class WsPlusUserControl
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
            this.buttonRightScroll = new System.Windows.Forms.Button();
            this.layoutPanelPlus = new System.Windows.Forms.TableLayoutPanel();
            this.layoutPanelActions = new System.Windows.Forms.TableLayoutPanel();
            this.labelCurrentPage = new System.Windows.Forms.Label();
            this.buttonLeftScroll = new System.Windows.Forms.Button();
            this.layoutPanelPlus.SuspendLayout();
            this.layoutPanelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRightRoll
            // 
            this.buttonRightScroll.BackColor = System.Drawing.Color.Transparent;
            this.buttonRightScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRightScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRightScroll.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRightScroll.Location = new System.Drawing.Point(598, 2);
            this.buttonRightScroll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRightScroll.Name = "buttonRightScroll";
            this.buttonRightScroll.Size = new System.Drawing.Size(296, 92);
            this.buttonRightScroll.TabIndex = 6;
            this.buttonRightScroll.Text = ">>";
            this.buttonRightScroll.UseVisualStyleBackColor = false;
            this.buttonRightScroll.Click += new System.EventHandler(this.ButtonNextScroll_Click);
            // 
            // layoutPanel
            // 
            this.layoutPanelPlus.AutoSize = true;
            this.layoutPanelPlus.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelPlus.ColumnCount = 3;
            this.layoutPanelPlus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelPlus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelPlus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelPlus.Controls.Add(this.layoutPanelActions, 0, 2);
            this.layoutPanelPlus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelPlus.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutPanelPlus.Location = new System.Drawing.Point(0, 0);
            this.layoutPanelPlus.Margin = new System.Windows.Forms.Padding(2);
            this.layoutPanelPlus.Name = "layoutPanelPlus";
            this.layoutPanelPlus.RowCount = 3;
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutPanelPlus.Size = new System.Drawing.Size(900, 500);
            this.layoutPanelPlus.TabIndex = 4;
            // 
            // layoutPanelActions
            // 
            this.layoutPanelActions.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelActions.ColumnCount = 3;
            this.layoutPanelPlus.SetColumnSpan(this.layoutPanelActions, 3);
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanelActions.Controls.Add(this.buttonRightScroll, 2, 0);
            this.layoutPanelActions.Controls.Add(this.labelCurrentPage, 1, 0);
            this.layoutPanelActions.Controls.Add(this.buttonLeftScroll, 0, 0);
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
            this.buttonLeftScroll.BackColor = System.Drawing.Color.Transparent;
            this.buttonLeftScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeftScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLeftScroll.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLeftScroll.Location = new System.Drawing.Point(2, 2);
            this.buttonLeftScroll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLeftScroll.Name = "buttonLeftScroll";
            this.buttonLeftScroll.Size = new System.Drawing.Size(294, 92);
            this.buttonLeftScroll.TabIndex = 4;
            this.buttonLeftScroll.Text = "<<";
            this.buttonLeftScroll.UseVisualStyleBackColor = false;
            this.buttonLeftScroll.Click += new System.EventHandler(this.ButtonPreviousScroll_Click);
            // 
            // PluUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.layoutPanelPlus);
            this.Name = "PluUserControl";
            this.Size = new System.Drawing.Size(900, 500);
            this.layoutPanelPlus.ResumeLayout(false);
            this.layoutPanelActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TableLayoutPanel layoutPanelPlus;
    private TableLayoutPanel layoutPanelActions;
    private Button buttonLeftScroll;
    private Label labelCurrentPage;
    private Button buttonRightScroll;
}
