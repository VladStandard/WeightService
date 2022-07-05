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
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPrintSendCmd
            // 
            this.buttonPrintSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrintSendCmd.Location = new System.Drawing.Point(10, 80);
            this.buttonPrintSendCmd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrintSendCmd.Name = "buttonPrintSendCmd";
            this.buttonPrintSendCmd.Size = new System.Drawing.Size(470, 30);
            this.buttonPrintSendCmd.TabIndex = 0;
            this.buttonPrintSendCmd.Text = "Send cmd";
            this.buttonPrintSendCmd.UseVisualStyleBackColor = true;
            this.buttonPrintSendCmd.Click += new System.EventHandler(this.ButtonPrintSendCmd_Click);
            // 
            // fieldPortName
            // 
            this.fieldPortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldPortName.Location = new System.Drawing.Point(125, 14);
            this.fieldPortName.Margin = new System.Windows.Forms.Padding(4);
            this.fieldPortName.Name = "fieldPortName";
            this.fieldPortName.Size = new System.Drawing.Size(355, 23);
            this.fieldPortName.TabIndex = 1;
            this.fieldPortName.Text = "SCALES-PRN-TSC";
            this.fieldPortName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Printer name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldCmd
            // 
            this.fieldCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldCmd.Location = new System.Drawing.Point(10, 118);
            this.fieldCmd.Margin = new System.Windows.Forms.Padding(4);
            this.fieldCmd.Name = "fieldCmd";
            this.fieldCmd.Size = new System.Drawing.Size(765, 509);
            this.fieldCmd.TabIndex = 4;
            this.fieldCmd.Text = "";
            // 
            // buttonLibSendCmd
            // 
            this.buttonLibSendCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLibSendCmd.Location = new System.Drawing.Point(495, 80);
            this.buttonLibSendCmd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibSendCmd.Name = "buttonLibSendCmd";
            this.buttonLibSendCmd.Size = new System.Drawing.Size(280, 30);
            this.buttonLibSendCmd.TabIndex = 5;
            this.buttonLibSendCmd.Text = "TscPrintControl send cmd";
            this.buttonLibSendCmd.UseVisualStyleBackColor = true;
            this.buttonLibSendCmd.Click += new System.EventHandler(this.ButtonLibSendCmd_Click);
            // 
            // buttonLibInit
            // 
            this.buttonLibInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLibInit.Location = new System.Drawing.Point(495, 10);
            this.buttonLibInit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibInit.Name = "buttonLibInit";
            this.buttonLibInit.Size = new System.Drawing.Size(280, 30);
            this.buttonLibInit.TabIndex = 6;
            this.buttonLibInit.Text = "TscPrintControl init v1";
            this.buttonLibInit.UseVisualStyleBackColor = true;
            this.buttonLibInit.Click += new System.EventHandler(this.ButtonLibInit_Click);
            // 
            // buttonLibInitv2
            // 
            this.buttonLibInitv2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLibInitv2.Location = new System.Drawing.Point(495, 45);
            this.buttonLibInitv2.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLibInitv2.Name = "buttonLibInitv2";
            this.buttonLibInitv2.Size = new System.Drawing.Size(280, 30);
            this.buttonLibInitv2.TabIndex = 9;
            this.buttonLibInitv2.Text = "TscPrintControl init v2";
            this.buttonLibInitv2.UseVisualStyleBackColor = true;
            this.buttonLibInitv2.Click += new System.EventHandler(this.ButtonLibInitv2_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Printer IP : port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldPortIp
            // 
            this.fieldPortIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldPortIp.Location = new System.Drawing.Point(170, 49);
            this.fieldPortIp.Margin = new System.Windows.Forms.Padding(4);
            this.fieldPortIp.Name = "fieldPortIp";
            this.fieldPortIp.Size = new System.Drawing.Size(230, 23);
            this.fieldPortIp.TabIndex = 7;
            this.fieldPortIp.Text = "192.168.4.159";
            this.fieldPortIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fieldPortPort
            // 
            this.fieldPortPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldPortPort.Location = new System.Drawing.Point(410, 49);
            this.fieldPortPort.Margin = new System.Windows.Forms.Padding(4);
            this.fieldPortPort.Name = "fieldPortPort";
            this.fieldPortPort.Size = new System.Drawing.Size(70, 23);
            this.fieldPortPort.TabIndex = 10;
            this.fieldPortPort.Text = "9100";
            this.fieldPortPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 639);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 11;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel.Size = new System.Drawing.Size(43, 17);
            this.toolStripStatusLabel.Text = "Status";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
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
            this.MinimumSize = new System.Drawing.Size(700, 300);
            this.Name = "FormMain";
            this.Text = "TSC print example";
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
    }
}

