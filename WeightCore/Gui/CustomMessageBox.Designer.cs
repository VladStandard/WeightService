//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace WeightCore.Gui
//{
//    partial class CustomMessageBox
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.buttonAbort = new System.Windows.Forms.Button();
//            this.buttonCancel = new System.Windows.Forms.Button();
//            this.buttonIgnore = new System.Windows.Forms.Button();
//            this.buttonNo = new System.Windows.Forms.Button();
//            this.buttonRetry = new System.Windows.Forms.Button();
//            this.buttonYes = new System.Windows.Forms.Button();
//            this.fieldMessage = new System.Windows.Forms.Label();
//            this.buttonOk = new System.Windows.Forms.Button();
//            this.SuspendLayout();
//            // 
//            // buttonAbort
//            // 
//            this.buttonAbort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.buttonAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.buttonAbort.Location = new System.Drawing.Point(458, 263);
//            this.buttonAbort.Name = "buttonAbort";
//            this.buttonAbort.Size = new System.Drawing.Size(84, 84);
//            this.buttonAbort.TabIndex = 1;
//            this.buttonAbort.Text = "Abort";
//            this.buttonAbort.UseVisualStyleBackColor = false;
//            this.buttonAbort.Visible = false;
//            this.buttonAbort.Click += new System.EventHandler(this.OnAbort_Click);
//            this.buttonAbort.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            // 
//            // buttonCancel
//            // 
//            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.buttonCancel.Location = new System.Drawing.Point(368, 263);
//            this.buttonCancel.Name = "buttonCancel";
//            this.buttonCancel.Size = new System.Drawing.Size(84, 84);
//            this.buttonCancel.TabIndex = 2;
//            this.buttonCancel.Text = "Cancel";
//            this.buttonCancel.UseVisualStyleBackColor = false;
//            this.buttonCancel.Visible = false;
//            this.buttonCancel.Click += new System.EventHandler(this.OnCancel_Click);
//            this.buttonCancel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            // 
//            // buttonIgnore
//            // 
//            this.buttonIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.buttonIgnore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.buttonIgnore.Location = new System.Drawing.Point(278, 263);
//            this.buttonIgnore.Name = "buttonIgnore";
//            this.buttonIgnore.Size = new System.Drawing.Size(84, 84);
//            this.buttonIgnore.TabIndex = 3;
//            this.buttonIgnore.Text = "Ignore";
//            this.buttonIgnore.UseVisualStyleBackColor = false;
//            this.buttonIgnore.Visible = false;
//            this.buttonIgnore.Click += new System.EventHandler(this.OnIgnore_Click);
//            this.buttonIgnore.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            // 
//            // buttonNo
//            // 
//            this.buttonNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.buttonNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.buttonNo.Location = new System.Drawing.Point(188, 263);
//            this.buttonNo.Name = "buttonNo";
//            this.buttonNo.Size = new System.Drawing.Size(84, 84);
//            this.buttonNo.TabIndex = 4;
//            this.buttonNo.Text = "No";
//            this.buttonNo.UseVisualStyleBackColor = false;
//            this.buttonNo.Visible = false;
//            this.buttonNo.Click += new System.EventHandler(this.OnNo_Click);
//            this.buttonNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            // 
//            // buttonRetry
//            // 
//            this.buttonRetry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.buttonRetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.buttonRetry.Location = new System.Drawing.Point(98, 263);
//            this.buttonRetry.Name = "buttonRetry";
//            this.buttonRetry.Size = new System.Drawing.Size(84, 84);
//            this.buttonRetry.TabIndex = 5;
//            this.buttonRetry.Text = "Retry";
//            this.buttonRetry.UseVisualStyleBackColor = false;
//            this.buttonRetry.Visible = false;
//            this.buttonRetry.Click += new System.EventHandler(this.OnRetry_Click);
//            this.buttonRetry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            // 
//            // buttonYes
//            // 
//            this.buttonYes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.buttonYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.buttonYes.Location = new System.Drawing.Point(8, 263);
//            this.buttonYes.Name = "buttonYes";
//            this.buttonYes.Size = new System.Drawing.Size(84, 84);
//            this.buttonYes.TabIndex = 6;
//            this.buttonYes.Text = "Yes";
//            this.buttonYes.UseVisualStyleBackColor = false;
//            this.buttonYes.Visible = false;
//            this.buttonYes.Click += new System.EventHandler(this.OnYes_Click);
//            this.buttonYes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            // 
//            // fieldMessage
//            // 
//            this.fieldMessage.BackColor = System.Drawing.Color.Transparent;
//            this.fieldMessage.Dock = System.Windows.Forms.DockStyle.Top;
//            this.fieldMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.fieldMessage.Location = new System.Drawing.Point(0, 0);
//            this.fieldMessage.Name = "fieldMessage";
//            this.fieldMessage.Size = new System.Drawing.Size(644, 255);
//            this.fieldMessage.TabIndex = 2;
//            this.fieldMessage.Text = "my message";
//            this.fieldMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // buttonOk
//            // 
//            this.buttonOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//            this.buttonOk.Location = new System.Drawing.Point(548, 263);
//            this.buttonOk.Name = "buttonOk";
//            this.buttonOk.Size = new System.Drawing.Size(84, 84);
//            this.buttonOk.TabIndex = 21;
//            this.buttonOk.Text = "Ok";
//            this.buttonOk.UseVisualStyleBackColor = false;
//            this.buttonOk.Visible = false;
//            this.buttonOk.Click += new System.EventHandler(this.OnOk_Click);
//            this.buttonOk.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            // 
//            // CustomMessageBox
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(644, 359);
//            this.ControlBox = false;
//            this.Controls.Add(this.fieldMessage);
//            this.Controls.Add(this.buttonOk);
//            this.Controls.Add(this.buttonAbort);
//            this.Controls.Add(this.buttonCancel);
//            this.Controls.Add(this.buttonIgnore);
//            this.Controls.Add(this.buttonNo);
//            this.Controls.Add(this.buttonRetry);
//            this.Controls.Add(this.buttonYes);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
//            this.Name = "CustomMessageBox";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
//            this.Text = "CustomMessageBox";
//            this.Shown += new System.EventHandler(this.CustomMessageBox_Shown);
//            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
//            this.ResumeLayout(false);

//        }

//        #endregion
//        private System.Windows.Forms.Button buttonAbort;
//        private System.Windows.Forms.Button buttonCancel;
//        private System.Windows.Forms.Button buttonIgnore;
//        private System.Windows.Forms.Button buttonNo;
//        private System.Windows.Forms.Button buttonRetry;
//        private System.Windows.Forms.Button buttonYes;
//        private System.Windows.Forms.Label fieldMessage;
//        private System.Windows.Forms.Button buttonOk;
//    }
//}