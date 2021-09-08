namespace TapangaMaha.Forms
{
    partial class SettingsForm
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
            this.btnSaveOption = new System.Windows.Forms.Button();
            this.btnUploadResources = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonDescription = new System.Windows.Forms.Button();
            this.fieldZebraTcpPortStatus = new System.Windows.Forms.Label();
            this.fieldZebraTcpAddressStatus = new System.Windows.Forms.Label();
            this.buttonZebraTcpPort = new System.Windows.Forms.Button();
            this.buttonZebraTcpAddress = new System.Windows.Forms.Button();
            this.fieldZebraTcpAddress = new System.Windows.Forms.MaskedTextBox();
            this.fieldZebraTcpPort = new System.Windows.Forms.MaskedTextBox();
            this.fieldDescription = new System.Windows.Forms.TextBox();
            this.fieldScaleFactor = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.fieldCurrentWeightFact = new System.Windows.Forms.TextBox();
            this.btnPrintList = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveOption
            // 
            this.btnSaveOption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSaveOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveOption.Location = new System.Drawing.Point(636, 3);
            this.btnSaveOption.Name = "btnSaveOption";
            this.btnSaveOption.Size = new System.Drawing.Size(208, 150);
            this.btnSaveOption.TabIndex = 1;
            this.btnSaveOption.Text = "Сохранить настройки";
            this.btnSaveOption.UseVisualStyleBackColor = false;
            this.btnSaveOption.Click += new System.EventHandler(this.btnSaveOption_Click);
            // 
            // btnUploadResources
            // 
            this.btnUploadResources.BackColor = System.Drawing.Color.Transparent;
            this.btnUploadResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUploadResources.Location = new System.Drawing.Point(422, 3);
            this.btnUploadResources.Name = "btnUploadResources";
            this.btnUploadResources.Size = new System.Drawing.Size(208, 150);
            this.btnUploadResources.TabIndex = 2;
            this.btnUploadResources.Text = "Выгрузить ресурсы для текущего шаблона";
            this.btnUploadResources.UseVisualStyleBackColor = false;
            this.btnUploadResources.Visible = false;
            this.btnUploadResources.Click += new System.EventHandler(this.btnUploadResources_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.BackColor = System.Drawing.Color.Transparent;
            this.btnCalibrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalibrate.Location = new System.Drawing.Point(208, 3);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(208, 150);
            this.btnCalibrate.TabIndex = 3;
            this.btnCalibrate.Text = "Калибровать Зебру";
            this.btnCalibrate.UseVisualStyleBackColor = false;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 90);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1073, 645);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 94);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1065, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные настройки";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.buttonDescription, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.fieldZebraTcpPortStatus, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.fieldZebraTcpAddressStatus, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonZebraTcpPort, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonZebraTcpAddress, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.fieldZebraTcpAddress, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.fieldZebraTcpPort, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.fieldDescription, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.fieldScaleFactor, 1, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(554, 541);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(3, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 24);
            this.button1.TabIndex = 22;
            this.button1.Text = "Коэфф. взвеш.";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // buttonDescription
            // 
            this.buttonDescription.BackColor = System.Drawing.Color.Transparent;
            this.buttonDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDescription.Location = new System.Drawing.Point(3, 63);
            this.buttonDescription.Name = "buttonDescription";
            this.buttonDescription.Size = new System.Drawing.Size(132, 24);
            this.buttonDescription.TabIndex = 21;
            this.buttonDescription.Text = "Описание";
            this.buttonDescription.UseVisualStyleBackColor = false;
            // 
            // fieldZebraTcpPortStatus
            // 
            this.fieldZebraTcpPortStatus.AutoSize = true;
            this.fieldZebraTcpPortStatus.BackColor = System.Drawing.Color.Transparent;
            this.fieldZebraTcpPortStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldZebraTcpPortStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldZebraTcpPortStatus.Location = new System.Drawing.Point(279, 30);
            this.fieldZebraTcpPortStatus.Name = "fieldZebraTcpPortStatus";
            this.fieldZebraTcpPortStatus.Size = new System.Drawing.Size(272, 30);
            this.fieldZebraTcpPortStatus.TabIndex = 18;
            this.fieldZebraTcpPortStatus.Text = "Нет доступа!";
            this.fieldZebraTcpPortStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldZebraTcpPortStatus.Visible = false;
            // 
            // fieldZebraTcpAddressStatus
            // 
            this.fieldZebraTcpAddressStatus.AutoSize = true;
            this.fieldZebraTcpAddressStatus.BackColor = System.Drawing.Color.Transparent;
            this.fieldZebraTcpAddressStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldZebraTcpAddressStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldZebraTcpAddressStatus.Location = new System.Drawing.Point(279, 0);
            this.fieldZebraTcpAddressStatus.Name = "fieldZebraTcpAddressStatus";
            this.fieldZebraTcpAddressStatus.Size = new System.Drawing.Size(272, 30);
            this.fieldZebraTcpAddressStatus.TabIndex = 17;
            this.fieldZebraTcpAddressStatus.Text = "Нет доступа!";
            this.fieldZebraTcpAddressStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldZebraTcpAddressStatus.Visible = false;
            // 
            // buttonZebraTcpPort
            // 
            this.buttonZebraTcpPort.BackColor = System.Drawing.Color.Transparent;
            this.buttonZebraTcpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonZebraTcpPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonZebraTcpPort.Location = new System.Drawing.Point(3, 33);
            this.buttonZebraTcpPort.Name = "buttonZebraTcpPort";
            this.buttonZebraTcpPort.Size = new System.Drawing.Size(132, 24);
            this.buttonZebraTcpPort.TabIndex = 16;
            this.buttonZebraTcpPort.Text = "Зебра TCP-порт";
            this.buttonZebraTcpPort.UseVisualStyleBackColor = false;
            // 
            // buttonZebraTcpAddress
            // 
            this.buttonZebraTcpAddress.BackColor = System.Drawing.Color.Transparent;
            this.buttonZebraTcpAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonZebraTcpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonZebraTcpAddress.Location = new System.Drawing.Point(3, 3);
            this.buttonZebraTcpAddress.Name = "buttonZebraTcpAddress";
            this.buttonZebraTcpAddress.Size = new System.Drawing.Size(132, 24);
            this.buttonZebraTcpAddress.TabIndex = 15;
            this.buttonZebraTcpAddress.Text = "Зебра TCP-адрес";
            this.buttonZebraTcpAddress.UseVisualStyleBackColor = false;
            // 
            // fieldZebraTcpAddress
            // 
            this.fieldZebraTcpAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldZebraTcpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldZebraTcpAddress.Location = new System.Drawing.Point(141, 3);
            this.fieldZebraTcpAddress.Name = "fieldZebraTcpAddress";
            this.fieldZebraTcpAddress.Size = new System.Drawing.Size(132, 23);
            this.fieldZebraTcpAddress.TabIndex = 8;
            this.fieldZebraTcpAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fieldZebraTcpPort
            // 
            this.fieldZebraTcpPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldZebraTcpPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldZebraTcpPort.Location = new System.Drawing.Point(141, 33);
            this.fieldZebraTcpPort.Name = "fieldZebraTcpPort";
            this.fieldZebraTcpPort.Size = new System.Drawing.Size(132, 23);
            this.fieldZebraTcpPort.TabIndex = 9;
            this.fieldZebraTcpPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fieldZebraTcpPort.ValidatingType = typeof(int);
            // 
            // fieldDescription
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.fieldDescription, 2);
            this.fieldDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldDescription.Location = new System.Drawing.Point(141, 63);
            this.fieldDescription.Name = "fieldDescription";
            this.fieldDescription.Size = new System.Drawing.Size(410, 23);
            this.fieldDescription.TabIndex = 11;
            this.fieldDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fieldScaleFactor
            // 
            this.fieldScaleFactor.Location = new System.Drawing.Point(141, 93);
            this.fieldScaleFactor.Name = "fieldScaleFactor";
            this.fieldScaleFactor.Size = new System.Drawing.Size(132, 23);
            this.fieldScaleFactor.TabIndex = 23;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel2);
            this.tabPage2.Controls.Add(this.fieldCurrentWeightFact);
            this.tabPage2.Location = new System.Drawing.Point(4, 94);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1065, 547);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Образец XML-данных";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanel2.Controls.Add(this.btnCopyToClipboard);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(953, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(109, 541);
            this.flowLayoutPanel2.TabIndex = 24;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Location = new System.Drawing.Point(3, 3);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(106, 84);
            this.btnCopyToClipboard.TabIndex = 0;
            this.btnCopyToClipboard.Text = "Copy";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // fieldCurrentWeightFact
            // 
            this.fieldCurrentWeightFact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldCurrentWeightFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldCurrentWeightFact.Location = new System.Drawing.Point(3, 3);
            this.fieldCurrentWeightFact.Multiline = true;
            this.fieldCurrentWeightFact.Name = "fieldCurrentWeightFact";
            this.fieldCurrentWeightFact.ReadOnly = true;
            this.fieldCurrentWeightFact.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.fieldCurrentWeightFact.Size = new System.Drawing.Size(1059, 541);
            this.fieldCurrentWeightFact.TabIndex = 23;
            // 
            // btnPrintList
            // 
            this.btnPrintList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrintList.Location = new System.Drawing.Point(3, 3);
            this.btnPrintList.Name = "btnPrintList";
            this.btnPrintList.Size = new System.Drawing.Size(199, 150);
            this.btnPrintList.TabIndex = 4;
            this.btnPrintList.Text = "Список ресурсов напечатать";
            this.btnPrintList.UseVisualStyleBackColor = true;
            this.btnPrintList.Click += new System.EventHandler(this.btnPrintList_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnPrintList);
            this.flowLayoutPanel1.Controls.Add(this.btnCalibrate);
            this.flowLayoutPanel1.Controls.Add(this.btnUploadResources);
            this.flowLayoutPanel1.Controls.Add(this.btnSaveOption);
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 486);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1073, 159);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.Location = new System.Drawing.Point(850, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(211, 150);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 645);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(740, 598);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSaveOption;
        private System.Windows.Forms.Button btnUploadResources;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonDescription;
        private System.Windows.Forms.Label fieldZebraTcpPortStatus;
        private System.Windows.Forms.Label fieldZebraTcpAddressStatus;
        private System.Windows.Forms.Button buttonZebraTcpPort;
        private System.Windows.Forms.Button buttonZebraTcpAddress;
        private System.Windows.Forms.MaskedTextBox fieldZebraTcpAddress;
        private System.Windows.Forms.MaskedTextBox fieldZebraTcpPort;
        private System.Windows.Forms.TextBox fieldDescription;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox fieldCurrentWeightFact;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrintList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox fieldScaleFactor;
    }
}