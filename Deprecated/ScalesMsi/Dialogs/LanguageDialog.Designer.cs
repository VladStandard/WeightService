using WixSharp;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    partial class LanguageDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.bottomBorder = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.next = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.buttonRunAs = new System.Windows.Forms.Button();
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.fieldLanguage = new System.Windows.Forms.ComboBox();
            this.groupBoxId = new System.Windows.Forms.GroupBox();
            this.buttonIdCheckInSql = new System.Windows.Forms.Button();
            this.buttonIdRegistryLoad = new System.Windows.Forms.Button();
            this.buttonIdPaste = new System.Windows.Forms.Button();
            this.fieldId = new System.Windows.Forms.MaskedTextBox();
            this.groupBoxSql = new System.Windows.Forms.GroupBox();
            this.buttonSqlConfigLoad = new System.Windows.Forms.Button();
            this.fieldSqlIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.buttonSqlCheckConnect = new System.Windows.Forms.Button();
            this.fieldSqlPassword = new System.Windows.Forms.MaskedTextBox();
            this.labelSqlPassword = new System.Windows.Forms.Label();
            this.fieldSqlUser = new System.Windows.Forms.MaskedTextBox();
            this.labelSqlUser = new System.Windows.Forms.Label();
            this.fieldSqlDb = new System.Windows.Forms.MaskedTextBox();
            this.labelSqlDb = new System.Windows.Forms.Label();
            this.fieldSqlServer = new System.Windows.Forms.MaskedTextBox();
            this.labelSqlServer = new System.Windows.Forms.Label();
            this.fieldConnectionString = new System.Windows.Forms.MaskedTextBox();
            this.fieldElevatedAccess = new System.Windows.Forms.Label();
            this.fieldIdFromDb = new System.Windows.Forms.ComboBox();
            this.bottomPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.groupBoxId.SuspendLayout();
            this.groupBoxSql.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottomPanel
            // 
            this.bottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bottomPanel.Controls.Add(this.bottomBorder);
            this.bottomPanel.Controls.Add(this.tableLayoutPanel1);
            this.bottomPanel.Location = new System.Drawing.Point(0, 312);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(494, 49);
            this.bottomPanel.TabIndex = 1;
            // 
            // bottomBorder
            // 
            this.bottomBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomBorder.Location = new System.Drawing.Point(0, 0);
            this.bottomBorder.Name = "bottomBorder";
            this.bottomBorder.Size = new System.Drawing.Size(494, 1);
            this.bottomBorder.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.next, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cancel, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRunAs, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(491, 41);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // next
            // 
            this.next.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.next.AutoSize = true;
            this.next.Location = new System.Drawing.Point(249, 7);
            this.next.MinimumSize = new System.Drawing.Size(75, 0);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(89, 27);
            this.next.TabIndex = 0;
            this.next.Text = "[WixUINext]";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cancel.AutoSize = true;
            this.cancel.Location = new System.Drawing.Point(344, 7);
            this.cancel.MinimumSize = new System.Drawing.Size(75, 0);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(104, 27);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "[WixUICancel]";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // buttonRunAs
            // 
            this.buttonRunAs.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonRunAs.AutoSize = true;
            this.buttonRunAs.Location = new System.Drawing.Point(68, 7);
            this.buttonRunAs.MinimumSize = new System.Drawing.Size(75, 0);
            this.buttonRunAs.Name = "buttonRunAs";
            this.buttonRunAs.Size = new System.Drawing.Size(175, 27);
            this.buttonRunAs.TabIndex = 1;
            this.buttonRunAs.Text = "[WixUIRunAs]";
            this.buttonRunAs.UseVisualStyleBackColor = true;
            this.buttonRunAs.Visible = false;
            this.buttonRunAs.Click += new System.EventHandler(this.buttonRunAs_Click);
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.fieldLanguage);
            this.groupBoxLanguage.Location = new System.Drawing.Point(5, 5);
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.Size = new System.Drawing.Size(485, 50);
            this.groupBoxLanguage.TabIndex = 19;
            this.groupBoxLanguage.TabStop = false;
            this.groupBoxLanguage.Text = "[MaintenanceWelcomeDlgLanguage]";
            // 
            // fieldLanguage
            // 
            this.fieldLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldLanguage.FormattingEnabled = true;
            this.fieldLanguage.Location = new System.Drawing.Point(15, 20);
            this.fieldLanguage.Name = "fieldLanguage";
            this.fieldLanguage.Size = new System.Drawing.Size(460, 21);
            this.fieldLanguage.TabIndex = 3;
            // 
            // groupBoxId
            // 
            this.groupBoxId.Controls.Add(this.fieldIdFromDb);
            this.groupBoxId.Controls.Add(this.buttonIdCheckInSql);
            this.groupBoxId.Controls.Add(this.buttonIdRegistryLoad);
            this.groupBoxId.Controls.Add(this.buttonIdPaste);
            this.groupBoxId.Controls.Add(this.fieldId);
            this.groupBoxId.Enabled = false;
            this.groupBoxId.Location = new System.Drawing.Point(5, 205);
            this.groupBoxId.Name = "groupBoxId";
            this.groupBoxId.Size = new System.Drawing.Size(485, 75);
            this.groupBoxId.TabIndex = 20;
            this.groupBoxId.TabStop = false;
            this.groupBoxId.Text = "[MaintenanceLocalizationDlgId]";
            // 
            // buttonIdCheckInSql
            // 
            this.buttonIdCheckInSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIdCheckInSql.Location = new System.Drawing.Point(330, 45);
            this.buttonIdCheckInSql.Name = "buttonIdCheckInSql";
            this.buttonIdCheckInSql.Size = new System.Drawing.Size(145, 25);
            this.buttonIdCheckInSql.TabIndex = 21;
            this.buttonIdCheckInSql.Text = "[MaintenanceLocalizationDlgIdPasteFromBuffer]";
            this.buttonIdCheckInSql.UseVisualStyleBackColor = true;
            this.buttonIdCheckInSql.Click += new System.EventHandler(this.buttonIdCheckInSql_Click);
            // 
            // buttonIdRegistryLoad
            // 
            this.buttonIdRegistryLoad.Location = new System.Drawing.Point(15, 45);
            this.buttonIdRegistryLoad.Name = "buttonIdRegistryLoad";
            this.buttonIdRegistryLoad.Size = new System.Drawing.Size(145, 25);
            this.buttonIdRegistryLoad.TabIndex = 20;
            this.buttonIdRegistryLoad.Text = "[MaintenanceLocalizationDlgLoadDefault]";
            this.buttonIdRegistryLoad.UseVisualStyleBackColor = true;
            this.buttonIdRegistryLoad.Click += new System.EventHandler(this.buttonIdReading_Click);
            // 
            // buttonIdPaste
            // 
            this.buttonIdPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIdPaste.Location = new System.Drawing.Point(167, 45);
            this.buttonIdPaste.Name = "buttonIdPaste";
            this.buttonIdPaste.Size = new System.Drawing.Size(155, 25);
            this.buttonIdPaste.TabIndex = 19;
            this.buttonIdPaste.Text = "[MaintenanceLocalizationDlgIdPasteFromBuffer]";
            this.buttonIdPaste.UseVisualStyleBackColor = true;
            this.buttonIdPaste.Click += new System.EventHandler(this.buttonIdPaste_Click);
            // 
            // fieldId
            // 
            this.fieldId.Location = new System.Drawing.Point(15, 20);
            this.fieldId.Name = "fieldId";
            this.fieldId.Size = new System.Drawing.Size(220, 20);
            this.fieldId.TabIndex = 7;
            this.fieldId.ValidatingType = typeof(System.DateTime);
            // 
            // groupBoxSql
            // 
            this.groupBoxSql.Controls.Add(this.buttonSqlConfigLoad);
            this.groupBoxSql.Controls.Add(this.fieldSqlIntegratedSecurity);
            this.groupBoxSql.Controls.Add(this.buttonSqlCheckConnect);
            this.groupBoxSql.Controls.Add(this.fieldSqlPassword);
            this.groupBoxSql.Controls.Add(this.labelSqlPassword);
            this.groupBoxSql.Controls.Add(this.fieldSqlUser);
            this.groupBoxSql.Controls.Add(this.labelSqlUser);
            this.groupBoxSql.Controls.Add(this.fieldSqlDb);
            this.groupBoxSql.Controls.Add(this.labelSqlDb);
            this.groupBoxSql.Controls.Add(this.fieldSqlServer);
            this.groupBoxSql.Controls.Add(this.labelSqlServer);
            this.groupBoxSql.Controls.Add(this.fieldConnectionString);
            this.groupBoxSql.Location = new System.Drawing.Point(5, 55);
            this.groupBoxSql.Name = "groupBoxSql";
            this.groupBoxSql.Size = new System.Drawing.Size(485, 150);
            this.groupBoxSql.TabIndex = 21;
            this.groupBoxSql.TabStop = false;
            this.groupBoxSql.Text = "[MaintenanceLocalizationDlgSqlConStr]";
            // 
            // buttonSqlConfigLoad
            // 
            this.buttonSqlConfigLoad.Location = new System.Drawing.Point(15, 120);
            this.buttonSqlConfigLoad.Name = "buttonSqlConfigLoad";
            this.buttonSqlConfigLoad.Size = new System.Drawing.Size(220, 25);
            this.buttonSqlConfigLoad.TabIndex = 29;
            this.buttonSqlConfigLoad.Text = "[MaintenanceLocalizationDlgSqlLoadConfig]";
            this.buttonSqlConfigLoad.UseVisualStyleBackColor = true;
            this.buttonSqlConfigLoad.Click += new System.EventHandler(this.buttonSqlLoadConfig_Click);
            // 
            // fieldSqlIntegratedSecurity
            // 
            this.fieldSqlIntegratedSecurity.AutoSize = true;
            this.fieldSqlIntegratedSecurity.Location = new System.Drawing.Point(15, 100);
            this.fieldSqlIntegratedSecurity.Margin = new System.Windows.Forms.Padding(2);
            this.fieldSqlIntegratedSecurity.Name = "fieldSqlIntegratedSecurity";
            this.fieldSqlIntegratedSecurity.Size = new System.Drawing.Size(267, 17);
            this.fieldSqlIntegratedSecurity.TabIndex = 27;
            this.fieldSqlIntegratedSecurity.Text = "[MaintenanceLocalizationDlgSqlIntegratedSecurity]";
            this.fieldSqlIntegratedSecurity.UseVisualStyleBackColor = true;
            // 
            // buttonSqlCheckConnect
            // 
            this.buttonSqlCheckConnect.Location = new System.Drawing.Point(255, 120);
            this.buttonSqlCheckConnect.Name = "buttonSqlCheckConnect";
            this.buttonSqlCheckConnect.Size = new System.Drawing.Size(220, 25);
            this.buttonSqlCheckConnect.TabIndex = 26;
            this.buttonSqlCheckConnect.Text = "[MaintenanceLocalizationDlgSqlCheckConnect]";
            this.buttonSqlCheckConnect.UseVisualStyleBackColor = true;
            this.buttonSqlCheckConnect.Click += new System.EventHandler(this.buttonSqlCheckConnect_Click);
            // 
            // fieldSqlPassword
            // 
            this.fieldSqlPassword.Location = new System.Drawing.Point(355, 73);
            this.fieldSqlPassword.Name = "fieldSqlPassword";
            this.fieldSqlPassword.PasswordChar = '*';
            this.fieldSqlPassword.Size = new System.Drawing.Size(120, 20);
            this.fieldSqlPassword.TabIndex = 25;
            this.fieldSqlPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fieldSqlPassword.ValidatingType = typeof(System.DateTime);
            // 
            // labelSqlPassword
            // 
            this.labelSqlPassword.Location = new System.Drawing.Point(245, 76);
            this.labelSqlPassword.Name = "labelSqlPassword";
            this.labelSqlPassword.Size = new System.Drawing.Size(110, 15);
            this.labelSqlPassword.TabIndex = 24;
            this.labelSqlPassword.Text = "[MaintenanceLocalizationDlgSqlPassword]";
            this.labelSqlPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldSqlUser
            // 
            this.fieldSqlUser.Location = new System.Drawing.Point(120, 73);
            this.fieldSqlUser.Name = "fieldSqlUser";
            this.fieldSqlUser.Size = new System.Drawing.Size(120, 20);
            this.fieldSqlUser.TabIndex = 23;
            this.fieldSqlUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fieldSqlUser.ValidatingType = typeof(System.DateTime);
            // 
            // labelSqlUser
            // 
            this.labelSqlUser.Location = new System.Drawing.Point(10, 76);
            this.labelSqlUser.Name = "labelSqlUser";
            this.labelSqlUser.Size = new System.Drawing.Size(110, 15);
            this.labelSqlUser.TabIndex = 22;
            this.labelSqlUser.Text = "[MaintenanceLocalizationDlgSqlUser]";
            this.labelSqlUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldSqlDb
            // 
            this.fieldSqlDb.Location = new System.Drawing.Point(355, 47);
            this.fieldSqlDb.Name = "fieldSqlDb";
            this.fieldSqlDb.Size = new System.Drawing.Size(120, 20);
            this.fieldSqlDb.TabIndex = 21;
            this.fieldSqlDb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fieldSqlDb.ValidatingType = typeof(System.DateTime);
            // 
            // labelSqlDb
            // 
            this.labelSqlDb.Location = new System.Drawing.Point(245, 50);
            this.labelSqlDb.Name = "labelSqlDb";
            this.labelSqlDb.Size = new System.Drawing.Size(110, 15);
            this.labelSqlDb.TabIndex = 20;
            this.labelSqlDb.Text = "[MaintenanceLocalizationDlgSqlDb]";
            this.labelSqlDb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldSqlServer
            // 
            this.fieldSqlServer.Location = new System.Drawing.Point(120, 47);
            this.fieldSqlServer.Name = "fieldSqlServer";
            this.fieldSqlServer.Size = new System.Drawing.Size(120, 20);
            this.fieldSqlServer.TabIndex = 19;
            this.fieldSqlServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fieldSqlServer.ValidatingType = typeof(System.DateTime);
            // 
            // labelSqlServer
            // 
            this.labelSqlServer.Location = new System.Drawing.Point(10, 50);
            this.labelSqlServer.Name = "labelSqlServer";
            this.labelSqlServer.Size = new System.Drawing.Size(110, 15);
            this.labelSqlServer.TabIndex = 18;
            this.labelSqlServer.Text = "[MaintenanceLocalizationDlgSqlServer]";
            this.labelSqlServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldConnectionString
            // 
            this.fieldConnectionString.Location = new System.Drawing.Point(15, 20);
            this.fieldConnectionString.Name = "fieldConnectionString";
            this.fieldConnectionString.ReadOnly = true;
            this.fieldConnectionString.Size = new System.Drawing.Size(460, 20);
            this.fieldConnectionString.TabIndex = 9;
            this.fieldConnectionString.ValidatingType = typeof(System.DateTime);
            // 
            // fieldElevatedAccess
            // 
            this.fieldElevatedAccess.Location = new System.Drawing.Point(5, 280);
            this.fieldElevatedAccess.Name = "fieldElevatedAccess";
            this.fieldElevatedAccess.Size = new System.Drawing.Size(486, 25);
            this.fieldElevatedAccess.TabIndex = 22;
            this.fieldElevatedAccess.Text = "[fieldElevatedAccess]";
            this.fieldElevatedAccess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldElevatedAccess.Visible = false;
            // 
            // fieldIdFromDb
            // 
            this.fieldIdFromDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldIdFromDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldIdFromDb.FormattingEnabled = true;
            this.fieldIdFromDb.Location = new System.Drawing.Point(255, 20);
            this.fieldIdFromDb.Name = "fieldIdFromDb";
            this.fieldIdFromDb.Size = new System.Drawing.Size(220, 21);
            this.fieldIdFromDb.TabIndex = 22;
            this.fieldIdFromDb.SelectedIndexChanged += new System.EventHandler(this.fieldIdFromDb_SelectedIndexChanged);
            // 
            // LanguageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 361);
            this.Controls.Add(this.fieldElevatedAccess);
            this.Controls.Add(this.groupBoxSql);
            this.Controls.Add(this.groupBoxId);
            this.Controls.Add(this.groupBoxLanguage);
            this.Controls.Add(this.bottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LanguageDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки локализации";
            this.Load += new System.EventHandler(this.LanguageDialog_Load);
            this.bottomPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxId.ResumeLayout(false);
            this.groupBoxId.PerformLayout();
            this.groupBoxSql.ResumeLayout(false);
            this.groupBoxSql.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Panel bottomBorder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxLanguage;
        private System.Windows.Forms.ComboBox fieldLanguage;
        private System.Windows.Forms.GroupBox groupBoxId;
        private System.Windows.Forms.Button buttonIdRegistryLoad;
        private System.Windows.Forms.Button buttonIdPaste;
        private System.Windows.Forms.MaskedTextBox fieldId;
        private System.Windows.Forms.GroupBox groupBoxSql;
        private System.Windows.Forms.MaskedTextBox fieldSqlPassword;
        private System.Windows.Forms.Label labelSqlPassword;
        private System.Windows.Forms.MaskedTextBox fieldSqlUser;
        private System.Windows.Forms.Label labelSqlUser;
        private System.Windows.Forms.MaskedTextBox fieldSqlDb;
        private System.Windows.Forms.Label labelSqlDb;
        private System.Windows.Forms.MaskedTextBox fieldSqlServer;
        private System.Windows.Forms.Label labelSqlServer;
        private System.Windows.Forms.MaskedTextBox fieldConnectionString;
        private System.Windows.Forms.Button buttonSqlCheckConnect;
        private System.Windows.Forms.Button buttonIdCheckInSql;
        private System.Windows.Forms.CheckBox fieldSqlIntegratedSecurity;
        private System.Windows.Forms.Button buttonSqlConfigLoad;
        private System.Windows.Forms.Label fieldElevatedAccess;
        private System.Windows.Forms.Button buttonRunAs;
        private System.Windows.Forms.ComboBox fieldIdFromDb;
    }
}