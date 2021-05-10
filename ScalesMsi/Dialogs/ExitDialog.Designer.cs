using WixSharp;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    partial class ExitDialog
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
            this.imgPanel = new System.Windows.Forms.Panel();
            this.textPanel = new System.Windows.Forms.Panel();
            this.fieldRunScalesTerminal = new System.Windows.Forms.CheckBox();
            this.fieldLabelPrintConfig = new System.Windows.Forms.CheckBox();
            this.fieldTapangaMahaConfig = new System.Windows.Forms.CheckBox();
            this.fieldScalesUIConfig = new System.Windows.Forms.CheckBox();
            this.fieldLabelPrintRun = new System.Windows.Forms.CheckBox();
            this.fieldTapangaMahaRun = new System.Windows.Forms.CheckBox();
            this.fieldRunDriver = new System.Windows.Forms.CheckBox();
            this.fieldRunManual = new System.Windows.Forms.CheckBox();
            this.fieldScalesUIRun = new System.Windows.Forms.CheckBox();
            this.title = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.Label();
            this.image = new System.Windows.Forms.PictureBox();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.viewLog = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.back = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.border1 = new System.Windows.Forms.Panel();
            this.fieldErrorDriver = new System.Windows.Forms.Label();
            this.fieldErrorScalesTerminal = new System.Windows.Forms.Label();
            this.imgPanel.SuspendLayout();
            this.textPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgPanel
            // 
            this.imgPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgPanel.Controls.Add(this.textPanel);
            this.imgPanel.Controls.Add(this.image);
            this.imgPanel.Location = new System.Drawing.Point(0, 0);
            this.imgPanel.Name = "imgPanel";
            this.imgPanel.Size = new System.Drawing.Size(494, 312);
            this.imgPanel.TabIndex = 8;
            // 
            // textPanel
            // 
            this.textPanel.Controls.Add(this.fieldErrorScalesTerminal);
            this.textPanel.Controls.Add(this.fieldErrorDriver);
            this.textPanel.Controls.Add(this.fieldRunScalesTerminal);
            this.textPanel.Controls.Add(this.fieldLabelPrintConfig);
            this.textPanel.Controls.Add(this.fieldTapangaMahaConfig);
            this.textPanel.Controls.Add(this.fieldScalesUIConfig);
            this.textPanel.Controls.Add(this.fieldLabelPrintRun);
            this.textPanel.Controls.Add(this.fieldTapangaMahaRun);
            this.textPanel.Controls.Add(this.fieldRunDriver);
            this.textPanel.Controls.Add(this.fieldRunManual);
            this.textPanel.Controls.Add(this.fieldScalesUIRun);
            this.textPanel.Controls.Add(this.title);
            this.textPanel.Controls.Add(this.description);
            this.textPanel.Location = new System.Drawing.Point(177, 17);
            this.textPanel.Name = "textPanel";
            this.textPanel.Size = new System.Drawing.Size(305, 289);
            this.textPanel.TabIndex = 8;
            // 
            // fieldRunScalesTerminal
            // 
            this.fieldRunScalesTerminal.AutoSize = true;
            this.fieldRunScalesTerminal.Enabled = false;
            this.fieldRunScalesTerminal.Location = new System.Drawing.Point(10, 50);
            this.fieldRunScalesTerminal.Name = "fieldRunScalesTerminal";
            this.fieldRunScalesTerminal.Size = new System.Drawing.Size(171, 17);
            this.fieldRunScalesTerminal.TabIndex = 16;
            this.fieldRunScalesTerminal.Text = "[ExitDialogRunScalesTerminal]";
            this.fieldRunScalesTerminal.UseVisualStyleBackColor = true;
            // 
            // fieldLabelPrintConfig
            // 
            this.fieldLabelPrintConfig.AutoSize = true;
            this.fieldLabelPrintConfig.Enabled = false;
            this.fieldLabelPrintConfig.Location = new System.Drawing.Point(10, 190);
            this.fieldLabelPrintConfig.Name = "fieldLabelPrintConfig";
            this.fieldLabelPrintConfig.Size = new System.Drawing.Size(146, 17);
            this.fieldLabelPrintConfig.TabIndex = 15;
            this.fieldLabelPrintConfig.Text = "[ExitDialogLabelPrintRun]";
            this.fieldLabelPrintConfig.UseVisualStyleBackColor = true;
            // 
            // fieldTapangaMahaConfig
            // 
            this.fieldTapangaMahaConfig.AutoSize = true;
            this.fieldTapangaMahaConfig.Enabled = false;
            this.fieldTapangaMahaConfig.Location = new System.Drawing.Point(10, 170);
            this.fieldTapangaMahaConfig.Name = "fieldTapangaMahaConfig";
            this.fieldTapangaMahaConfig.Size = new System.Drawing.Size(169, 17);
            this.fieldTapangaMahaConfig.TabIndex = 14;
            this.fieldTapangaMahaConfig.Text = "[ExitDialogTapangaMahaRun]";
            this.fieldTapangaMahaConfig.UseVisualStyleBackColor = true;
            // 
            // fieldScalesUIConfig
            // 
            this.fieldScalesUIConfig.AutoSize = true;
            this.fieldScalesUIConfig.Enabled = false;
            this.fieldScalesUIConfig.Location = new System.Drawing.Point(10, 150);
            this.fieldScalesUIConfig.Name = "fieldScalesUIConfig";
            this.fieldScalesUIConfig.Size = new System.Drawing.Size(142, 17);
            this.fieldScalesUIConfig.TabIndex = 13;
            this.fieldScalesUIConfig.Text = "[ExitDialogScalesUIRun]";
            this.fieldScalesUIConfig.UseVisualStyleBackColor = true;
            // 
            // fieldLabelPrintRun
            // 
            this.fieldLabelPrintRun.AutoSize = true;
            this.fieldLabelPrintRun.Location = new System.Drawing.Point(10, 130);
            this.fieldLabelPrintRun.Name = "fieldLabelPrintRun";
            this.fieldLabelPrintRun.Size = new System.Drawing.Size(146, 17);
            this.fieldLabelPrintRun.TabIndex = 12;
            this.fieldLabelPrintRun.Text = "[ExitDialogLabelPrintRun]";
            this.fieldLabelPrintRun.UseVisualStyleBackColor = true;
            // 
            // fieldTapangaMahaRun
            // 
            this.fieldTapangaMahaRun.AutoSize = true;
            this.fieldTapangaMahaRun.Location = new System.Drawing.Point(10, 110);
            this.fieldTapangaMahaRun.Name = "fieldTapangaMahaRun";
            this.fieldTapangaMahaRun.Size = new System.Drawing.Size(169, 17);
            this.fieldTapangaMahaRun.TabIndex = 11;
            this.fieldTapangaMahaRun.Text = "[ExitDialogTapangaMahaRun]";
            this.fieldTapangaMahaRun.UseVisualStyleBackColor = true;
            // 
            // fieldRunDriver
            // 
            this.fieldRunDriver.AutoSize = true;
            this.fieldRunDriver.Enabled = false;
            this.fieldRunDriver.Location = new System.Drawing.Point(10, 30);
            this.fieldRunDriver.Name = "fieldRunDriver";
            this.fieldRunDriver.Size = new System.Drawing.Size(127, 17);
            this.fieldRunDriver.TabIndex = 10;
            this.fieldRunDriver.Text = "[ExitDialogRunDriver]";
            this.fieldRunDriver.UseVisualStyleBackColor = true;
            // 
            // fieldRunManual
            // 
            this.fieldRunManual.AutoSize = true;
            this.fieldRunManual.Location = new System.Drawing.Point(10, 70);
            this.fieldRunManual.Name = "fieldRunManual";
            this.fieldRunManual.Size = new System.Drawing.Size(134, 17);
            this.fieldRunManual.TabIndex = 9;
            this.fieldRunManual.Text = "[ExitDialogManualRun]";
            this.fieldRunManual.UseVisualStyleBackColor = true;
            // 
            // fieldScalesUIRun
            // 
            this.fieldScalesUIRun.AutoSize = true;
            this.fieldScalesUIRun.Location = new System.Drawing.Point(10, 90);
            this.fieldScalesUIRun.Name = "fieldScalesUIRun";
            this.fieldScalesUIRun.Size = new System.Drawing.Size(142, 17);
            this.fieldScalesUIRun.TabIndex = 8;
            this.fieldScalesUIRun.Text = "[ExitDialogScalesUIRun]";
            this.fieldScalesUIRun.UseVisualStyleBackColor = true;
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(3, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(299, 25);
            this.title.TabIndex = 6;
            this.title.Text = "[ExitDialogTitle]";
            // 
            // description
            // 
            this.description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.description.BackColor = System.Drawing.Color.Transparent;
            this.description.Location = new System.Drawing.Point(10, 215);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(285, 60);
            this.description.TabIndex = 7;
            this.description.Text = "[ExitDialogDescription]";
            // 
            // image
            // 
            this.image.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.image.Location = new System.Drawing.Point(0, 0);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(156, 312);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image.TabIndex = 4;
            this.image.TabStop = false;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bottomPanel.Controls.Add(this.viewLog);
            this.bottomPanel.Controls.Add(this.tableLayoutPanel1);
            this.bottomPanel.Controls.Add(this.border1);
            this.bottomPanel.Location = new System.Drawing.Point(0, 312);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(494, 49);
            this.bottomPanel.TabIndex = 5;
            // 
            // viewLog
            // 
            this.viewLog.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.viewLog.AutoSize = true;
            this.viewLog.BackColor = System.Drawing.Color.Transparent;
            this.viewLog.Location = new System.Drawing.Point(16, 17);
            this.viewLog.Name = "viewLog";
            this.viewLog.Size = new System.Drawing.Size(54, 13);
            this.viewLog.TabIndex = 1;
            this.viewLog.TabStop = true;
            this.viewLog.Text = "[ViewLog]";
            this.viewLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.viewLog_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.back, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.next, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cancel, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(491, 43);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // back
            // 
            this.back.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.back.AutoSize = true;
            this.back.Enabled = false;
            this.back.Location = new System.Drawing.Point(168, 8);
            this.back.MinimumSize = new System.Drawing.Size(75, 0);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(92, 27);
            this.back.TabIndex = 0;
            this.back.Text = "[WixUIBack]";
            this.back.UseVisualStyleBackColor = true;
            // 
            // next
            // 
            this.next.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.next.AutoSize = true;
            this.next.Location = new System.Drawing.Point(266, 8);
            this.next.MinimumSize = new System.Drawing.Size(75, 0);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(98, 27);
            this.next.TabIndex = 0;
            this.next.Text = "[WixUIFinish]";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.finish_Click);
            // 
            // cancel
            // 
            this.cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cancel.AutoSize = true;
            this.cancel.Enabled = false;
            this.cancel.Location = new System.Drawing.Point(384, 8);
            this.cancel.MinimumSize = new System.Drawing.Size(75, 0);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(104, 27);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "[WixUICancel]";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // border1
            // 
            this.border1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.border1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.border1.Location = new System.Drawing.Point(0, 0);
            this.border1.Name = "border1";
            this.border1.Size = new System.Drawing.Size(494, 1);
            this.border1.TabIndex = 9;
            // 
            // fieldErrorDriver
            // 
            this.fieldErrorDriver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldErrorDriver.AutoSize = true;
            this.fieldErrorDriver.BackColor = System.Drawing.Color.Transparent;
            this.fieldErrorDriver.Location = new System.Drawing.Point(145, 30);
            this.fieldErrorDriver.Name = "fieldErrorDriver";
            this.fieldErrorDriver.Size = new System.Drawing.Size(110, 13);
            this.fieldErrorDriver.TabIndex = 17;
            this.fieldErrorDriver.Text = "[ExitDialogErrorDriver]";
            this.fieldErrorDriver.Visible = false;
            // 
            // fieldErrorScalesTerminal
            // 
            this.fieldErrorScalesTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldErrorScalesTerminal.AutoSize = true;
            this.fieldErrorScalesTerminal.BackColor = System.Drawing.Color.Transparent;
            this.fieldErrorScalesTerminal.Location = new System.Drawing.Point(145, 50);
            this.fieldErrorScalesTerminal.Name = "fieldErrorScalesTerminal";
            this.fieldErrorScalesTerminal.Size = new System.Drawing.Size(154, 13);
            this.fieldErrorScalesTerminal.TabIndex = 18;
            this.fieldErrorScalesTerminal.Text = "[ExitDialogErrorScalesTerminal]";
            this.fieldErrorScalesTerminal.Visible = false;
            // 
            // ExitDialog
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 361);
            this.Controls.Add(this.imgPanel);
            this.Controls.Add(this.bottomPanel);
            this.Name = "ExitDialog";
            this.Text = "[ExitDialog_Title]";
            this.Load += new System.EventHandler(this.exitDialog_Load);
            this.imgPanel.ResumeLayout(false);
            this.textPanel.ResumeLayout(false);
            this.textPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label description;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.LinkLabel viewLog;
        private System.Windows.Forms.Panel imgPanel;
        private System.Windows.Forms.Panel border1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Panel textPanel;
        private System.Windows.Forms.CheckBox fieldScalesUIRun;
        private System.Windows.Forms.CheckBox fieldRunManual;
        private System.Windows.Forms.CheckBox fieldRunDriver;
        private System.Windows.Forms.CheckBox fieldLabelPrintRun;
        private System.Windows.Forms.CheckBox fieldTapangaMahaRun;
        private System.Windows.Forms.CheckBox fieldLabelPrintConfig;
        private System.Windows.Forms.CheckBox fieldTapangaMahaConfig;
        private System.Windows.Forms.CheckBox fieldScalesUIConfig;
        private System.Windows.Forms.CheckBox fieldRunScalesTerminal;
        private System.Windows.Forms.Label fieldErrorDriver;
        private System.Windows.Forms.Label fieldErrorScalesTerminal;
    }
}