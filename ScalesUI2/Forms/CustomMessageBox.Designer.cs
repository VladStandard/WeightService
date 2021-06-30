
namespace ScalesUI.Forms
{
    partial class CustomMessageBox
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btOk = new System.Windows.Forms.Button();
            this.btAbort = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btIgnore = new System.Windows.Forms.Button();
            this.btNo = new System.Windows.Forms.Button();
            this.btRetry = new System.Windows.Forms.Button();
            this.btYes = new System.Windows.Forms.Button();
            this.fieldMessage = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btOk);
            this.flowLayoutPanel1.Controls.Add(this.btAbort);
            this.flowLayoutPanel1.Controls.Add(this.btCancel);
            this.flowLayoutPanel1.Controls.Add(this.btIgnore);
            this.flowLayoutPanel1.Controls.Add(this.btNo);
            this.flowLayoutPanel1.Controls.Add(this.btRetry);
            this.flowLayoutPanel1.Controls.Add(this.btYes);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 272);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(613, 87);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btOk
            // 
            this.btOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btOk.Location = new System.Drawing.Point(535, 3);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 84);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Visible = false;
            this.btOk.Click += new System.EventHandler(this.OnOk_Click);
            this.btOk.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            // 
            // btAbort
            // 
            this.btAbort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btAbort.Location = new System.Drawing.Point(454, 3);
            this.btAbort.Name = "btAbort";
            this.btAbort.Size = new System.Drawing.Size(75, 84);
            this.btAbort.TabIndex = 1;
            this.btAbort.Text = "Abort";
            this.btAbort.UseVisualStyleBackColor = true;
            this.btAbort.Visible = false;
            this.btAbort.Click += new System.EventHandler(this.OnAbort_Click);
            this.btAbort.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btCancel.Location = new System.Drawing.Point(373, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 84);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Visible = false;
            this.btCancel.Click += new System.EventHandler(this.OnCancel_Click);
            this.btCancel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            // 
            // btIgnore
            // 
            this.btIgnore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btIgnore.Location = new System.Drawing.Point(292, 3);
            this.btIgnore.Name = "btIgnore";
            this.btIgnore.Size = new System.Drawing.Size(75, 84);
            this.btIgnore.TabIndex = 3;
            this.btIgnore.Text = "Ignore";
            this.btIgnore.UseVisualStyleBackColor = true;
            this.btIgnore.Visible = false;
            this.btIgnore.Click += new System.EventHandler(this.OnIgnore_Click);
            this.btIgnore.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            // 
            // btNo
            // 
            this.btNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btNo.Location = new System.Drawing.Point(211, 3);
            this.btNo.Name = "btNo";
            this.btNo.Size = new System.Drawing.Size(75, 84);
            this.btNo.TabIndex = 4;
            this.btNo.Text = "No";
            this.btNo.UseVisualStyleBackColor = true;
            this.btNo.Visible = false;
            this.btNo.Click += new System.EventHandler(this.OnNo_Click);
            this.btNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            // 
            // btRetry
            // 
            this.btRetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btRetry.Location = new System.Drawing.Point(130, 3);
            this.btRetry.Name = "btRetry";
            this.btRetry.Size = new System.Drawing.Size(75, 84);
            this.btRetry.TabIndex = 5;
            this.btRetry.Text = "Retry";
            this.btRetry.UseVisualStyleBackColor = true;
            this.btRetry.Visible = false;
            this.btRetry.Click += new System.EventHandler(this.OnRetry_Click);
            this.btRetry.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            // 
            // btYes
            // 
            this.btYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btYes.Location = new System.Drawing.Point(49, 3);
            this.btYes.Name = "btYes";
            this.btYes.Size = new System.Drawing.Size(75, 84);
            this.btYes.TabIndex = 6;
            this.btYes.Text = "Yes";
            this.btYes.UseVisualStyleBackColor = true;
            this.btYes.Visible = false;
            this.btYes.Click += new System.EventHandler(this.OnYes_Click);
            this.btYes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            // 
            // fieldMessage
            // 
            this.fieldMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMessage.Location = new System.Drawing.Point(0, 0);
            this.fieldMessage.Name = "fieldMessage";
            this.fieldMessage.Size = new System.Drawing.Size(613, 272);
            this.fieldMessage.TabIndex = 2;
            this.fieldMessage.Text = "my message";
            this.fieldMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 359);
            this.ControlBox = false;
            this.Controls.Add(this.fieldMessage);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CustomMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CustomMessageBox";
            this.Shown += new System.EventHandler(this.CustomMessageBox_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomMessageBox_KeyUp);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btAbort;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btIgnore;
        private System.Windows.Forms.Button btNo;
        private System.Windows.Forms.Button btRetry;
        private System.Windows.Forms.Button btYes;
        private System.Windows.Forms.Label fieldMessage;
    }
}