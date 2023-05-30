// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WsLabelCore.Controls;

partial class WsFormNavigationUserControl
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
            this.layoutPanelUser = new System.Windows.Forms.TableLayoutPanel();
            this.layoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxReturn = new System.Windows.Forms.PictureBox();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.layoutPanelUser.SuspendLayout();
            this.layoutPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutPanelUser
            // 
            this.layoutPanelUser.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelUser.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.layoutPanelUser.ColumnCount = 1;
            this.layoutPanelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelUser.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanelUser.Controls.Add(this.layoutPanelTop, 0, 0);
            this.layoutPanelUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelUser.Location = new System.Drawing.Point(0, 0);
            this.layoutPanelUser.Name = "layoutPanelUser";
            this.layoutPanelUser.RowCount = 2;
            this.layoutPanelUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.layoutPanelUser.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.layoutPanelUser.Size = new System.Drawing.Size(1024, 668);
            this.layoutPanelUser.TabIndex = 0;
            // 
            // layoutPanelTop
            // 
            this.layoutPanelTop.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelTop.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.layoutPanelTop.ColumnCount = 3;
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanelTop.Controls.Add(this.pictureBoxReturn, 0, 0);
            this.layoutPanelTop.Controls.Add(this.fieldTitle, 1, 0);
            this.layoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelTop.Location = new System.Drawing.Point(4, 4);
            this.layoutPanelTop.Name = "layoutPanelTop";
            this.layoutPanelTop.RowCount = 1;
            this.layoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelTop.Size = new System.Drawing.Size(1016, 33);
            this.layoutPanelTop.TabIndex = 70;
            // 
            // pictureBoxReturn
            // 
            this.pictureBoxReturn.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxReturn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxReturn.Image = global::WsLabelCore.Properties.Resources.left_1;
            this.pictureBoxReturn.Location = new System.Drawing.Point(4, 4);
            this.pictureBoxReturn.Name = "pictureBoxReturn";
            this.pictureBoxReturn.Size = new System.Drawing.Size(44, 25);
            this.pictureBoxReturn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxReturn.TabIndex = 59;
            this.pictureBoxReturn.TabStop = false;
            this.pictureBoxReturn.Visible = false;
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.Color.Transparent;
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.ForeColor = System.Drawing.Color.Black;
            this.fieldTitle.Location = new System.Drawing.Point(55, 4);
            this.fieldTitle.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(904, 25);
            this.fieldTitle.TabIndex = 21;
            this.fieldTitle.Text = "fieldTitle";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WsFormNavigationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.layoutPanelUser);
            this.Name = "WsFormNavigationUserControl";
            this.Size = new System.Drawing.Size(1024, 668);
            this.layoutPanelUser.ResumeLayout(false);
            this.layoutPanelTop.ResumeLayout(false);
            this.layoutPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReturn)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private TableLayoutPanel layoutPanelUser;
    private TableLayoutPanel layoutPanelTop;
    private PictureBox pictureBoxReturn;
    private Label fieldTitle;
}
