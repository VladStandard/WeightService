namespace ScalesUI.Forms
{
    public partial class WsMainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components is not null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelNettoWeight = new System.Windows.Forms.Label();
            this.fieldTareWeight = new System.Windows.Forms.Label();
            this.fieldNettoWeight = new System.Windows.Forms.Label();
            this.layoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.fieldPrintShipping = new System.Windows.Forms.Label();
            this.fieldWarning = new System.Windows.Forms.Label();
            this.fieldMassa = new System.Windows.Forms.Label();
            this.labelTareWeight = new System.Windows.Forms.Label();
            this.labelKneading = new System.Windows.Forms.Label();
            this.labelProductDate = new System.Windows.Forms.Label();
            this.fieldProductDate = new System.Windows.Forms.Label();
            this.fieldKneading = new System.Windows.Forms.Label();
            this.fieldPrintMain = new System.Windows.Forms.Label();
            this.fieldPlu = new System.Windows.Forms.Label();
            this.layoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxSettings = new System.Windows.Forms.PictureBox();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.layoutPanelMain.SuspendLayout();
            this.layoutPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNettoWeight
            // 
            this.labelNettoWeight.AutoSize = true;
            this.labelNettoWeight.BackColor = System.Drawing.Color.Transparent;
            this.labelNettoWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNettoWeight.Enabled = false;
            this.labelNettoWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNettoWeight.ForeColor = System.Drawing.Color.Black;
            this.labelNettoWeight.Location = new System.Drawing.Point(141, 96);
            this.labelNettoWeight.Margin = new System.Windows.Forms.Padding(3);
            this.labelNettoWeight.Name = "labelNettoWeight";
            this.labelNettoWeight.Size = new System.Drawing.Size(198, 47);
            this.labelNettoWeight.TabIndex = 12;
            this.labelNettoWeight.Text = "labelNettoWeight";
            this.labelNettoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelNettoWeight.Visible = false;
            // 
            // fieldTareWeight
            // 
            this.fieldTareWeight.AutoSize = true;
            this.fieldTareWeight.BackColor = System.Drawing.Color.Transparent;
            this.fieldTareWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTareWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTareWeight.ForeColor = System.Drawing.Color.Black;
            this.fieldTareWeight.Location = new System.Drawing.Point(345, 149);
            this.fieldTareWeight.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTareWeight.Name = "fieldTareWeight";
            this.fieldTareWeight.Size = new System.Drawing.Size(536, 47);
            this.fieldTareWeight.TabIndex = 11;
            this.fieldTareWeight.Text = "0,000";
            this.fieldTareWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldNettoWeight
            // 
            this.fieldNettoWeight.AutoSize = true;
            this.fieldNettoWeight.BackColor = System.Drawing.Color.Transparent;
            this.fieldNettoWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldNettoWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldNettoWeight.ForeColor = System.Drawing.Color.Black;
            this.fieldNettoWeight.Location = new System.Drawing.Point(345, 96);
            this.fieldNettoWeight.Margin = new System.Windows.Forms.Padding(3);
            this.fieldNettoWeight.Name = "fieldNettoWeight";
            this.fieldNettoWeight.Size = new System.Drawing.Size(536, 47);
            this.fieldNettoWeight.TabIndex = 10;
            this.fieldNettoWeight.Text = "0,000";
            this.fieldNettoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldNettoWeight.Visible = false;
            // 
            // layoutPanelMain
            // 
            this.layoutPanelMain.ColumnCount = 6;
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.layoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5F));
            this.layoutPanelMain.Controls.Add(this.fieldPrintShipping, 2, 10);
            this.layoutPanelMain.Controls.Add(this.fieldWarning, 1, 6);
            this.layoutPanelMain.Controls.Add(this.fieldMassa, 2, 8);
            this.layoutPanelMain.Controls.Add(this.labelTareWeight, 2, 3);
            this.layoutPanelMain.Controls.Add(this.labelNettoWeight, 2, 2);
            this.layoutPanelMain.Controls.Add(this.fieldNettoWeight, 3, 2);
            this.layoutPanelMain.Controls.Add(this.fieldTareWeight, 3, 3);
            this.layoutPanelMain.Controls.Add(this.labelKneading, 2, 5);
            this.layoutPanelMain.Controls.Add(this.labelProductDate, 2, 4);
            this.layoutPanelMain.Controls.Add(this.fieldProductDate, 3, 4);
            this.layoutPanelMain.Controls.Add(this.fieldKneading, 3, 5);
            this.layoutPanelMain.Controls.Add(this.fieldPrintMain, 2, 9);
            this.layoutPanelMain.Controls.Add(this.fieldPlu, 1, 1);
            this.layoutPanelMain.Controls.Add(this.layoutPanelTop, 1, 0);
            this.layoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.layoutPanelMain.Name = "layoutPanelMain";
            this.layoutPanelMain.RowCount = 14;
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutPanelMain.Size = new System.Drawing.Size(1024, 668);
            this.layoutPanelMain.TabIndex = 7;
            this.layoutPanelMain.Visible = false;
            // 
            // fieldPrintShipping
            // 
            this.fieldPrintShipping.AutoSize = true;
            this.fieldPrintShipping.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelMain.SetColumnSpan(this.fieldPrintShipping, 3);
            this.fieldPrintShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintShipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintShipping.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintShipping.Location = new System.Drawing.Point(141, 427);
            this.fieldPrintShipping.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintShipping.Name = "fieldPrintShipping";
            this.fieldPrintShipping.Size = new System.Drawing.Size(873, 19);
            this.fieldPrintShipping.TabIndex = 66;
            this.fieldPrintShipping.Text = "fieldPrintShipping";
            this.fieldPrintShipping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintShipping.Visible = false;
            // 
            // fieldWarning
            // 
            this.fieldWarning.AutoSize = true;
            this.fieldWarning.BackColor = System.Drawing.Color.LightYellow;
            this.fieldWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.layoutPanelMain.SetColumnSpan(this.fieldWarning, 4);
            this.fieldWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWarning.Enabled = false;
            this.fieldWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWarning.ForeColor = System.Drawing.Color.Black;
            this.fieldWarning.Location = new System.Drawing.Point(8, 302);
            this.fieldWarning.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWarning.Name = "fieldWarning";
            this.fieldWarning.Size = new System.Drawing.Size(1006, 44);
            this.fieldWarning.TabIndex = 61;
            this.fieldWarning.Text = "Warning";
            this.fieldWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWarning.Visible = false;
            // 
            // fieldMassa
            // 
            this.fieldMassa.AutoSize = true;
            this.fieldMassa.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelMain.SetColumnSpan(this.fieldMassa, 3);
            this.fieldMassa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassa.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassa.Location = new System.Drawing.Point(141, 377);
            this.fieldMassa.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassa.Name = "fieldMassa";
            this.fieldMassa.Size = new System.Drawing.Size(873, 19);
            this.fieldMassa.TabIndex = 46;
            this.fieldMassa.Text = "fieldMassa";
            this.fieldMassa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTareWeight
            // 
            this.labelTareWeight.AutoSize = true;
            this.labelTareWeight.BackColor = System.Drawing.Color.Transparent;
            this.labelTareWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTareWeight.Enabled = false;
            this.labelTareWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTareWeight.ForeColor = System.Drawing.Color.Black;
            this.labelTareWeight.Location = new System.Drawing.Point(141, 149);
            this.labelTareWeight.Margin = new System.Windows.Forms.Padding(3);
            this.labelTareWeight.Name = "labelTareWeight";
            this.labelTareWeight.Size = new System.Drawing.Size(198, 47);
            this.labelTareWeight.TabIndex = 17;
            this.labelTareWeight.Text = "labelPackageWeight";
            this.labelTareWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKneading
            // 
            this.labelKneading.AutoSize = true;
            this.labelKneading.BackColor = System.Drawing.Color.Transparent;
            this.labelKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKneading.Enabled = false;
            this.labelKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKneading.ForeColor = System.Drawing.Color.Black;
            this.labelKneading.Location = new System.Drawing.Point(141, 252);
            this.labelKneading.Margin = new System.Windows.Forms.Padding(3);
            this.labelKneading.Name = "labelKneading";
            this.labelKneading.Size = new System.Drawing.Size(198, 44);
            this.labelKneading.TabIndex = 27;
            this.labelKneading.Text = "labelKneading";
            this.labelKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelProductDate
            // 
            this.labelProductDate.AutoSize = true;
            this.labelProductDate.BackColor = System.Drawing.Color.Transparent;
            this.labelProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductDate.Enabled = false;
            this.labelProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProductDate.ForeColor = System.Drawing.Color.Black;
            this.labelProductDate.Location = new System.Drawing.Point(141, 202);
            this.labelProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelProductDate.Name = "labelProductDate";
            this.labelProductDate.Size = new System.Drawing.Size(198, 44);
            this.labelProductDate.TabIndex = 28;
            this.labelProductDate.Text = "labelProductDate";
            this.labelProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.BackColor = System.Drawing.Color.Transparent;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.ForeColor = System.Drawing.Color.Black;
            this.fieldProductDate.Location = new System.Drawing.Point(345, 202);
            this.fieldProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(536, 44);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = "ProductDate";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.BackColor = System.Drawing.Color.Transparent;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.ForeColor = System.Drawing.Color.Black;
            this.fieldKneading.Location = new System.Drawing.Point(345, 252);
            this.fieldKneading.Margin = new System.Windows.Forms.Padding(3);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(536, 44);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = "Kneading";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldPrintMain
            // 
            this.fieldPrintMain.AutoSize = true;
            this.fieldPrintMain.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanelMain.SetColumnSpan(this.fieldPrintMain, 3);
            this.fieldPrintMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintMain.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintMain.Location = new System.Drawing.Point(141, 402);
            this.fieldPrintMain.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintMain.Name = "fieldPrintMain";
            this.fieldPrintMain.Size = new System.Drawing.Size(873, 19);
            this.fieldPrintMain.TabIndex = 37;
            this.fieldPrintMain.Text = "fieldPrintMain";
            this.fieldPrintMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.Color.LightYellow;
            this.fieldPlu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.layoutPanelMain.SetColumnSpan(this.fieldPlu, 4);
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPlu.ForeColor = System.Drawing.Color.Black;
            this.fieldPlu.Location = new System.Drawing.Point(8, 43);
            this.fieldPlu.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(1006, 47);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.Text = "PLU";
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // layoutPanelTop
            // 
            this.layoutPanelTop.ColumnCount = 5;
            this.layoutPanelMain.SetColumnSpan(this.layoutPanelTop, 4);
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.Controls.Add(this.pictureBoxSettings, 0, 0);
            this.layoutPanelTop.Controls.Add(this.pictureBoxClose, 4, 0);
            this.layoutPanelTop.Controls.Add(this.fieldTitle, 2, 0);
            this.layoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelTop.Location = new System.Drawing.Point(8, 3);
            this.layoutPanelTop.Name = "layoutPanelTop";
            this.layoutPanelTop.RowCount = 1;
            this.layoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelTop.Size = new System.Drawing.Size(1006, 34);
            this.layoutPanelTop.TabIndex = 69;
            // 
            // pictureBoxSettings
            // 
            this.pictureBoxSettings.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSettings.Image = global::ScalesUI.Properties.Resources.settings_3;
            this.pictureBoxSettings.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxSettings.Name = "pictureBoxSettings";
            this.pictureBoxSettings.Size = new System.Drawing.Size(44, 28);
            this.pictureBoxSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSettings.TabIndex = 23;
            this.pictureBoxSettings.TabStop = false;
            this.pictureBoxSettings.Click += new System.EventHandler(this.ActionSwitchDeviceSettings);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_1;
            this.pictureBoxClose.Location = new System.Drawing.Point(956, 3);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(47, 28);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 22;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.ActionClose);
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.Color.Transparent;
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.ForeColor = System.Drawing.Color.Black;
            this.fieldTitle.Location = new System.Drawing.Point(203, 3);
            this.fieldTitle.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(597, 28);
            this.fieldTitle.TabIndex = 21;
            this.fieldTitle.Text = "ScalesUI";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 668);
            this.Controls.Add(this.layoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WsMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать этикеток";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.layoutPanelMain.ResumeLayout(false);
            this.layoutPanelMain.PerformLayout();
            this.layoutPanelTop.ResumeLayout(false);
            this.layoutPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelNettoWeight;
        private System.Windows.Forms.Label fieldTareWeight;
        private System.Windows.Forms.Label fieldNettoWeight;
        private System.Windows.Forms.TableLayoutPanel layoutPanelMain;
        private System.Windows.Forms.Label fieldPlu;
        private System.Windows.Forms.Label labelTareWeight;
        private System.Windows.Forms.Label labelKneading;
        private System.Windows.Forms.Label labelProductDate;
        private System.Windows.Forms.Label fieldProductDate;
        private System.Windows.Forms.Label fieldKneading;
        private System.Windows.Forms.Label fieldPrintMain;
        private System.Windows.Forms.Label fieldMassa;
        private System.Windows.Forms.Label fieldWarning;
        private System.Windows.Forms.Label fieldPrintShipping;
        private TableLayoutPanel layoutPanelTop;
        private PictureBox pictureBoxClose;
        private Label fieldTitle;
        private PictureBox pictureBoxSettings;
    }
}

