namespace TscPrintDemoWinForm
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
            this.buttonPrintSendCmd = new System.Windows.Forms.Button();
            this.fieldPortName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fieldCmd = new System.Windows.Forms.RichTextBox();
            this.buttonLibSendCmd = new System.Windows.Forms.Button();
            this.buttonLibInit = new System.Windows.Forms.Button();
            this.buttonLibInitv2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fieldPortIp = new System.Windows.Forms.TextBox();
            this.fieldPortPort = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBoxLabelSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxLabelDpi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxLib = new System.Windows.Forms.ComboBox();
            this.buttonPrintSendCmdByTcp = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPrintSendCmd
            // 
            this.buttonPrintSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrintSendCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrintSendCmd.Location = new System.Drawing.Point(185, 80);
            this.buttonPrintSendCmd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrintSendCmd.Name = "buttonPrintSendCmd";
            this.buttonPrintSendCmd.Size = new System.Drawing.Size(165, 30);
            this.buttonPrintSendCmd.TabIndex = 21;
            this.buttonPrintSendCmd.Text = "Send cmd by driver";
            this.buttonPrintSendCmd.UseVisualStyleBackColor = true;
            this.buttonPrintSendCmd.Click += new System.EventHandler(this.ButtonPrintSendCmd_Click);
            // 
            // fieldPortName
            // 
            this.fieldPortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldPortName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPortName.Location = new System.Drawing.Point(125, 14);
            this.fieldPortName.Margin = new System.Windows.Forms.Padding(4);
            this.fieldPortName.Name = "fieldPortName";
            this.fieldPortName.Size = new System.Drawing.Size(225, 22);
            this.fieldPortName.TabIndex = 2;
            this.fieldPortName.Text = "SCALES-PRN-TSC";
            this.fieldPortName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldCmd
            // 
            this.fieldCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldCmd.Location = new System.Drawing.Point(10, 118);
            this.fieldCmd.Margin = new System.Windows.Forms.Padding(4);
            this.fieldCmd.Name = "fieldCmd";
            this.fieldCmd.Size = new System.Drawing.Size(815, 209);
            this.fieldCmd.TabIndex = 20;
            this.fieldCmd.Text = "";
            // 
            // buttonLibSendCmd
            // 
            this.buttonLibSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLibSendCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLibSendCmd.Location = new System.Drawing.Point(565, 80);
            this.buttonLibSendCmd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibSendCmd.Name = "buttonLibSendCmd";
            this.buttonLibSendCmd.Size = new System.Drawing.Size(260, 30);
            this.buttonLibSendCmd.TabIndex = 13;
            this.buttonLibSendCmd.Text = "TscPrintControl send cmd";
            this.buttonLibSendCmd.UseVisualStyleBackColor = true;
            this.buttonLibSendCmd.Click += new System.EventHandler(this.ButtonLibSendCmd_Click);
            // 
            // buttonLibInit
            // 
            this.buttonLibInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLibInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLibInit.Location = new System.Drawing.Point(565, 10);
            this.buttonLibInit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibInit.Name = "buttonLibInit";
            this.buttonLibInit.Size = new System.Drawing.Size(260, 30);
            this.buttonLibInit.TabIndex = 11;
            this.buttonLibInit.Text = "Init by name with size and dpi";
            this.buttonLibInit.UseVisualStyleBackColor = true;
            this.buttonLibInit.Click += new System.EventHandler(this.ButtonLibInit_Click);
            // 
            // buttonLibInitv2
            // 
            this.buttonLibInitv2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLibInitv2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLibInitv2.Location = new System.Drawing.Point(565, 45);
            this.buttonLibInitv2.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibInitv2.Name = "buttonLibInitv2";
            this.buttonLibInitv2.Size = new System.Drawing.Size(260, 30);
            this.buttonLibInitv2.TabIndex = 12;
            this.buttonLibInitv2.Text = "Init by eth with size and dpi";
            this.buttonLibInitv2.UseVisualStyleBackColor = true;
            this.buttonLibInitv2.Click += new System.EventHandler(this.ButtonLibInitv2_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(15, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP : port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldPortIp
            // 
            this.fieldPortIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldPortIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPortIp.Location = new System.Drawing.Point(125, 49);
            this.fieldPortIp.Margin = new System.Windows.Forms.Padding(4);
            this.fieldPortIp.Name = "fieldPortIp";
            this.fieldPortIp.Size = new System.Drawing.Size(160, 22);
            this.fieldPortIp.TabIndex = 3;
            this.fieldPortIp.Text = "127.0.0.1";
            this.fieldPortIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fieldPortPort
            // 
            this.fieldPortPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldPortPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPortPort.Location = new System.Drawing.Point(295, 49);
            this.fieldPortPort.Margin = new System.Windows.Forms.Padding(4);
            this.fieldPortPort.Name = "fieldPortPort";
            this.fieldPortPort.Size = new System.Drawing.Size(55, 22);
            this.fieldPortPort.TabIndex = 4;
            this.fieldPortPort.Text = "9100";
            this.fieldPortPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 339);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(834, 22);
            this.statusStrip.TabIndex = 11;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel.Text = "Status";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxLabelSize
            // 
            this.comboBoxLabelSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLabelSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabelSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLabelSize.FormattingEnabled = true;
            this.comboBoxLabelSize.Location = new System.Drawing.Point(447, 48);
            this.comboBoxLabelSize.Name = "comboBoxLabelSize";
            this.comboBoxLabelSize.Size = new System.Drawing.Size(111, 24);
            this.comboBoxLabelSize.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(357, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Label size";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(357, 85);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Label dpi";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxLabelDpi
            // 
            this.comboBoxLabelDpi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLabelDpi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabelDpi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLabelDpi.FormattingEnabled = true;
            this.comboBoxLabelDpi.Location = new System.Drawing.Point(447, 84);
            this.comboBoxLabelDpi.Name = "comboBoxLabelDpi";
            this.comboBoxLabelDpi.Size = new System.Drawing.Size(111, 24);
            this.comboBoxLabelDpi.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(357, 13);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Library";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxLib
            // 
            this.comboBoxLib.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLib.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxLib.FormattingEnabled = true;
            this.comboBoxLib.Location = new System.Drawing.Point(447, 12);
            this.comboBoxLib.Name = "comboBoxLib";
            this.comboBoxLib.Size = new System.Drawing.Size(111, 24);
            this.comboBoxLib.TabIndex = 6;
            // 
            // buttonPrintSendCmdByTcp
            // 
            this.buttonPrintSendCmdByTcp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrintSendCmdByTcp.Location = new System.Drawing.Point(10, 80);
            this.buttonPrintSendCmdByTcp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrintSendCmdByTcp.Name = "buttonPrintSendCmdByTcp";
            this.buttonPrintSendCmdByTcp.Size = new System.Drawing.Size(165, 30);
            this.buttonPrintSendCmdByTcp.TabIndex = 22;
            this.buttonPrintSendCmdByTcp.Text = "Send cmd by TCP";
            this.buttonPrintSendCmdByTcp.UseVisualStyleBackColor = true;
            this.buttonPrintSendCmdByTcp.Click += new System.EventHandler(this.buttonPrintSendCmdByTcp_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 361);
            this.Controls.Add(this.buttonPrintSendCmdByTcp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxLib);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxLabelDpi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxLabelSize);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.fieldPortPort);
            this.Controls.Add(this.buttonLibInitv2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fieldPortIp);
            this.Controls.Add(this.buttonLibInit);
            this.Controls.Add(this.buttonLibSendCmd);
            this.Controls.Add(this.fieldCmd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fieldPortName);
            this.Controls.Add(this.buttonPrintSendCmd);
            this.Font = new System.Drawing.Font("Cascadia Code", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(750, 300);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Demo TSC/HPRT print ZPL";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPrintSendCmd;
        private System.Windows.Forms.TextBox fieldPortName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox fieldCmd;
        private System.Windows.Forms.Button buttonLibSendCmd;
        private System.Windows.Forms.Button buttonLibInit;
        private System.Windows.Forms.Button buttonLibInitv2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fieldPortIp;
        private System.Windows.Forms.TextBox fieldPortPort;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ComboBox comboBoxLabelSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxLabelDpi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxLib;
        private System.Windows.Forms.Button buttonPrintSendCmdByTcp;
    }
}

