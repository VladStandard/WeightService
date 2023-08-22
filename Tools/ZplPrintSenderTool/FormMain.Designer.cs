namespace ZplPrintSenderTool
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonPrintSendCmd = new System.Windows.Forms.Button();
            this.fieldName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.fieldCmd = new System.Windows.Forms.RichTextBox();
            this.buttonLibrarySendCmd = new System.Windows.Forms.Button();
            this.buttonLibraryInit = new System.Windows.Forms.Button();
            this.buttonLibInitv2 = new System.Windows.Forms.Button();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.fieldIpAddress = new System.Windows.Forms.TextBox();
            this.fieldIpPort = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBoxLabelSize = new System.Windows.Forms.ComboBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelLabelDpi = new System.Windows.Forms.Label();
            this.comboBoxLabelDpi = new System.Windows.Forms.ComboBox();
            this.labelLibrary = new System.Windows.Forms.Label();
            this.comboBoxLibrary = new System.Windows.Forms.ComboBox();
            this.buttonPrintSendCmdByTcp = new System.Windows.Forms.Button();
            this.comboBoxSendType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.fieldException = new System.Windows.Forms.RichTextBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPrintSendCmd
            // 
            this.buttonPrintSendCmd.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrintSendCmd.Location = new System.Drawing.Point(198, 105);
            this.buttonPrintSendCmd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrintSendCmd.Name = "buttonPrintSendCmd";
            this.buttonPrintSendCmd.Size = new System.Drawing.Size(202, 27);
            this.buttonPrintSendCmd.TabIndex = 21;
            this.buttonPrintSendCmd.Text = "Send ZPL by TSC-driver";
            this.buttonPrintSendCmd.UseVisualStyleBackColor = true;
            this.buttonPrintSendCmd.Click += new System.EventHandler(this.ButtonPrintSendCmd_Click);
            // 
            // fieldName
            // 
            this.fieldName.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldName.Location = new System.Drawing.Point(522, 8);
            this.fieldName.Margin = new System.Windows.Forms.Padding(4);
            this.fieldName.Name = "fieldName";
            this.fieldName.Size = new System.Drawing.Size(353, 23);
            this.fieldName.TabIndex = 2;
            this.fieldName.Text = "SCALES-PRN-TSC";
            this.fieldName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(410, 10);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(104, 17);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Printer name";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldCmd
            // 
            this.fieldCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldCmd.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldCmd.Location = new System.Drawing.Point(10, 196);
            this.fieldCmd.Margin = new System.Windows.Forms.Padding(4);
            this.fieldCmd.Name = "fieldCmd";
            this.fieldCmd.Size = new System.Drawing.Size(865, 339);
            this.fieldCmd.TabIndex = 20;
            this.fieldCmd.Text = "";
            // 
            // buttonLibrarySendCmd
            // 
            this.buttonLibrarySendCmd.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLibrarySendCmd.Location = new System.Drawing.Point(615, 104);
            this.buttonLibrarySendCmd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibrarySendCmd.Name = "buttonLibrarySendCmd";
            this.buttonLibrarySendCmd.Size = new System.Drawing.Size(260, 27);
            this.buttonLibrarySendCmd.TabIndex = 13;
            this.buttonLibrarySendCmd.Text = "TscPrintControl send cmd";
            this.buttonLibrarySendCmd.UseVisualStyleBackColor = true;
            this.buttonLibrarySendCmd.Click += new System.EventHandler(this.ButtonLibSendCmd_Click);
            // 
            // buttonLibraryInit
            // 
            this.buttonLibraryInit.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLibraryInit.Location = new System.Drawing.Point(615, 34);
            this.buttonLibraryInit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibraryInit.Name = "buttonLibraryInit";
            this.buttonLibraryInit.Size = new System.Drawing.Size(260, 27);
            this.buttonLibraryInit.TabIndex = 11;
            this.buttonLibraryInit.Text = "Init by name with size and dpi";
            this.buttonLibraryInit.UseVisualStyleBackColor = true;
            this.buttonLibraryInit.Click += new System.EventHandler(this.ButtonLibInit_Click);
            // 
            // buttonLibInitv2
            // 
            this.buttonLibInitv2.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLibInitv2.Location = new System.Drawing.Point(615, 69);
            this.buttonLibInitv2.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibInitv2.Name = "buttonLibInitv2";
            this.buttonLibInitv2.Size = new System.Drawing.Size(260, 27);
            this.buttonLibInitv2.TabIndex = 12;
            this.buttonLibInitv2.Text = "Init by eth with size and dpi";
            this.buttonLibInitv2.UseVisualStyleBackColor = true;
            this.buttonLibInitv2.Click += new System.EventHandler(this.ButtonLibInitv2_Click);
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIpAddress.Location = new System.Drawing.Point(10, 40);
            this.labelIpAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(24, 17);
            this.labelIpAddress.TabIndex = 1;
            this.labelIpAddress.Text = "IP";
            this.labelIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldIpAddress
            // 
            this.fieldIpAddress.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldIpAddress.Location = new System.Drawing.Point(40, 35);
            this.fieldIpAddress.Margin = new System.Windows.Forms.Padding(4);
            this.fieldIpAddress.Name = "fieldIpAddress";
            this.fieldIpAddress.Size = new System.Drawing.Size(210, 23);
            this.fieldIpAddress.TabIndex = 3;
            this.fieldIpAddress.Text = "127.0.0.1";
            this.fieldIpAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fieldIpPort
            // 
            this.fieldIpPort.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldIpPort.Location = new System.Drawing.Point(320, 35);
            this.fieldIpPort.Margin = new System.Windows.Forms.Padding(4);
            this.fieldIpPort.Name = "fieldIpPort";
            this.fieldIpPort.Size = new System.Drawing.Size(80, 23);
            this.fieldIpPort.TabIndex = 4;
            this.fieldIpPort.Text = "9100";
            this.fieldIpPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(884, 22);
            this.statusStrip.TabIndex = 11;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel.Text = "Status";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxLabelSize
            // 
            this.comboBoxLabelSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabelSize.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLabelSize.FormattingEnabled = true;
            this.comboBoxLabelSize.Location = new System.Drawing.Point(500, 70);
            this.comboBoxLabelSize.Name = "comboBoxLabelSize";
            this.comboBoxLabelSize.Size = new System.Drawing.Size(111, 25);
            this.comboBoxLabelSize.TabIndex = 8;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSize.Location = new System.Drawing.Point(410, 75);
            this.labelSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(88, 17);
            this.labelSize.TabIndex = 7;
            this.labelSize.Text = "Label size";
            this.labelSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLabelDpi
            // 
            this.labelLabelDpi.AutoSize = true;
            this.labelLabelDpi.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLabelDpi.Location = new System.Drawing.Point(410, 110);
            this.labelLabelDpi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLabelDpi.Name = "labelLabelDpi";
            this.labelLabelDpi.Size = new System.Drawing.Size(80, 17);
            this.labelLabelDpi.TabIndex = 9;
            this.labelLabelDpi.Text = "Label dpi";
            this.labelLabelDpi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxLabelDpi
            // 
            this.comboBoxLabelDpi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabelDpi.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLabelDpi.FormattingEnabled = true;
            this.comboBoxLabelDpi.Location = new System.Drawing.Point(500, 105);
            this.comboBoxLabelDpi.Name = "comboBoxLabelDpi";
            this.comboBoxLabelDpi.Size = new System.Drawing.Size(111, 25);
            this.comboBoxLabelDpi.TabIndex = 10;
            // 
            // labelLibrary
            // 
            this.labelLibrary.AutoSize = true;
            this.labelLibrary.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLibrary.Location = new System.Drawing.Point(410, 40);
            this.labelLibrary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLibrary.Name = "labelLibrary";
            this.labelLibrary.Size = new System.Drawing.Size(64, 17);
            this.labelLibrary.TabIndex = 5;
            this.labelLibrary.Text = "Library";
            this.labelLibrary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxLibrary
            // 
            this.comboBoxLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLibrary.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLibrary.FormattingEnabled = true;
            this.comboBoxLibrary.Location = new System.Drawing.Point(500, 35);
            this.comboBoxLibrary.Name = "comboBoxLibrary";
            this.comboBoxLibrary.Size = new System.Drawing.Size(111, 25);
            this.comboBoxLibrary.TabIndex = 6;
            // 
            // buttonPrintSendCmdByTcp
            // 
            this.buttonPrintSendCmdByTcp.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrintSendCmdByTcp.Location = new System.Drawing.Point(10, 105);
            this.buttonPrintSendCmdByTcp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrintSendCmdByTcp.Name = "buttonPrintSendCmdByTcp";
            this.buttonPrintSendCmdByTcp.Size = new System.Drawing.Size(180, 27);
            this.buttonPrintSendCmdByTcp.TabIndex = 22;
            this.buttonPrintSendCmdByTcp.Text = "Send ZPL by TCP";
            this.buttonPrintSendCmdByTcp.UseVisualStyleBackColor = true;
            this.buttonPrintSendCmdByTcp.Click += new System.EventHandler(this.buttonPrintSendCmdByTcp_Click);
            // 
            // comboBoxSendType
            // 
            this.comboBoxSendType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSendType.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSendType.FormattingEnabled = true;
            this.comboBoxSendType.Items.AddRange(new object[] {
            "TCP",
            "TSC-driver"});
            this.comboBoxSendType.Location = new System.Drawing.Point(97, 5);
            this.comboBoxSendType.Name = "comboBoxSendType";
            this.comboBoxSendType.Size = new System.Drawing.Size(303, 25);
            this.comboBoxSendType.TabIndex = 23;
            this.comboBoxSendType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSendType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(10, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 24;
            this.label6.Text = "Send type";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPort.Location = new System.Drawing.Point(270, 40);
            this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(40, 17);
            this.labelPort.TabIndex = 25;
            this.labelPort.Text = "Port";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldException
            // 
            this.fieldException.Location = new System.Drawing.Point(10, 139);
            this.fieldException.Name = "fieldException";
            this.fieldException.ReadOnly = true;
            this.fieldException.Size = new System.Drawing.Size(865, 50);
            this.fieldException.TabIndex = 26;
            this.fieldException.Text = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.fieldException);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxSendType);
            this.Controls.Add(this.buttonPrintSendCmdByTcp);
            this.Controls.Add(this.labelLibrary);
            this.Controls.Add(this.comboBoxLibrary);
            this.Controls.Add(this.labelLabelDpi);
            this.Controls.Add(this.comboBoxLabelDpi);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.comboBoxLabelSize);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.fieldIpPort);
            this.Controls.Add(this.buttonLibInitv2);
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.fieldIpAddress);
            this.Controls.Add(this.buttonLibraryInit);
            this.Controls.Add(this.buttonLibrarySendCmd);
            this.Controls.Add(this.fieldCmd);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.fieldName);
            this.Controls.Add(this.buttonPrintSendCmd);
            this.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(900, 1800);
            this.MinimumSize = new System.Drawing.Size(900, 39);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ZplPrintSenderTool";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPrintSendCmd;
        private System.Windows.Forms.TextBox fieldName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.RichTextBox fieldCmd;
        private System.Windows.Forms.Button buttonLibrarySendCmd;
        private System.Windows.Forms.Button buttonLibraryInit;
        private System.Windows.Forms.Button buttonLibInitv2;
        private System.Windows.Forms.Label labelIpAddress;
        private System.Windows.Forms.TextBox fieldIpAddress;
        private System.Windows.Forms.TextBox fieldIpPort;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ComboBox comboBoxLabelSize;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelLabelDpi;
        private System.Windows.Forms.ComboBox comboBoxLabelDpi;
        private System.Windows.Forms.Label labelLibrary;
        private System.Windows.Forms.ComboBox comboBoxLibrary;
        private System.Windows.Forms.Button buttonPrintSendCmdByTcp;
        private ComboBox comboBoxSendType;
        private Label label6;
        private Label labelPort;
        private RichTextBox fieldException;
    }
}

