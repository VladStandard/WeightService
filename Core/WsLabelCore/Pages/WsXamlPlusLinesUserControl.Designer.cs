using System.Windows.Forms;

namespace WsLabelCore.Pages;

partial class WsXamlPlusLinesUserControl
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonLeftScroll = new System.Windows.Forms.Button();
            this.buttonCurrentPage = new System.Windows.Forms.Button();
            this.layoutPanelPlus.SuspendLayout();
            this.layoutPanelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRightScroll
            // 
            this.buttonRightScroll.BackColor = System.Drawing.Color.Transparent;
            this.buttonRightScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRightScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRightScroll.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRightScroll.Location = new System.Drawing.Point(450, 2);
            this.buttonRightScroll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRightScroll.Name = "buttonRightScroll";
            this.buttonRightScroll.Size = new System.Drawing.Size(220, 52);
            this.buttonRightScroll.TabIndex = 6;
            this.buttonRightScroll.Text = ">>";
            this.buttonRightScroll.UseVisualStyleBackColor = false;
            this.buttonRightScroll.Click += new System.EventHandler(this.ButtonNextScroll_Click);
            // 
            // layoutPanelPlus
            // 
            this.layoutPanelPlus.AutoSize = true;
            this.layoutPanelPlus.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelPlus.ColumnCount = 4;
            this.layoutPanelPlus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelPlus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelPlus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelPlus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelPlus.Controls.Add(this.layoutPanelActions, 0, 4);
            this.layoutPanelPlus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelPlus.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutPanelPlus.Location = new System.Drawing.Point(0, 0);
            this.layoutPanelPlus.Margin = new System.Windows.Forms.Padding(2);
            this.layoutPanelPlus.Name = "layoutPanelPlus";
            this.layoutPanelPlus.RowCount = 5;
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.layoutPanelPlus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.layoutPanelPlus.Size = new System.Drawing.Size(900, 500);
            this.layoutPanelPlus.TabIndex = 4;
            // 
            // layoutPanelActions
            // 
            this.layoutPanelActions.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelActions.ColumnCount = 4;
            this.layoutPanelPlus.SetColumnSpan(this.layoutPanelActions, 4);
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelActions.Controls.Add(this.buttonCurrentPage, 1, 0);
            this.layoutPanelActions.Controls.Add(this.buttonCancel, 3, 0);
            this.layoutPanelActions.Controls.Add(this.buttonRightScroll, 2, 0);
            this.layoutPanelActions.Controls.Add(this.buttonLeftScroll, 0, 0);
            this.layoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelActions.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutPanelActions.Location = new System.Drawing.Point(2, 442);
            this.layoutPanelActions.Margin = new System.Windows.Forms.Padding(2);
            this.layoutPanelActions.Name = "layoutPanelActions";
            this.layoutPanelActions.RowCount = 1;
            this.layoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelActions.Size = new System.Drawing.Size(896, 56);
            this.layoutPanelActions.TabIndex = 5;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(674, 2);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(220, 52);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonLeftScroll
            // 
            this.buttonLeftScroll.BackColor = System.Drawing.Color.Transparent;
            this.buttonLeftScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeftScroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLeftScroll.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLeftScroll.Location = new System.Drawing.Point(2, 2);
            this.buttonLeftScroll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLeftScroll.Name = "buttonLeftScroll";
            this.buttonLeftScroll.Size = new System.Drawing.Size(220, 52);
            this.buttonLeftScroll.TabIndex = 4;
            this.buttonLeftScroll.Text = "<<";
            this.buttonLeftScroll.UseVisualStyleBackColor = false;
            this.buttonLeftScroll.Click += new System.EventHandler(this.ButtonPreviousScroll_Click);
            // 
            // buttonCurrentPage
            // 
            this.buttonCurrentPage.BackColor = System.Drawing.Color.Transparent;
            this.buttonCurrentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCurrentPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCurrentPage.Font = new System.Drawing.Font("Cascadia Code", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCurrentPage.Location = new System.Drawing.Point(226, 2);
            this.buttonCurrentPage.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCurrentPage.Name = "buttonCurrentPage";
            this.buttonCurrentPage.Size = new System.Drawing.Size(220, 52);
            this.buttonCurrentPage.TabIndex = 8;
            this.buttonCurrentPage.Text = "Cancel";
            this.buttonCurrentPage.UseVisualStyleBackColor = false;
            // 
            // WsPlusUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.layoutPanelPlus);
            this.Name = "WsXamlPlusLinesUserControl";
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
    private Button buttonRightScroll;
    private Button buttonCancel;
    private Button buttonCurrentPage;
}
