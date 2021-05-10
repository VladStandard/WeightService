namespace ScalesUI.Forms
{
    partial class PluListForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.PluListGrid = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRightRoll = new System.Windows.Forms.Button();
            this.btnLeftRoll = new System.Windows.Forms.Button();
            this.lbCurrentPage = new System.Windows.Forms.Label();
            this.btnRightRoll2 = new System.Windows.Forms.Button();
            this.btnLeftRoll2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.Location = new System.Drawing.Point(703, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(170, 107);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PluListGrid
            // 
            this.PluListGrid.AutoSize = true;
            this.PluListGrid.ColumnCount = 3;
            this.PluListGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PluListGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PluListGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PluListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluListGrid.Location = new System.Drawing.Point(100, 0);
            this.PluListGrid.Name = "PluListGrid";
            this.PluListGrid.RowCount = 2;
            this.PluListGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PluListGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PluListGrid.Size = new System.Drawing.Size(676, 487);
            this.PluListGrid.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.btnRightRoll);
            this.flowLayoutPanel1.Controls.Add(this.btnLeftRoll);
            this.flowLayoutPanel1.Controls.Add(this.lbCurrentPage);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 487);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(876, 122);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(652, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 23);
            this.label1.TabIndex = 4;
            // 
            // btnRightRoll
            // 
            this.btnRightRoll.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRightRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRightRoll.Location = new System.Drawing.Point(476, 3);
            this.btnRightRoll.Name = "btnRightRoll";
            this.btnRightRoll.Size = new System.Drawing.Size(170, 107);
            this.btnRightRoll.TabIndex = 1;
            this.btnRightRoll.Text = ">>";
            this.btnRightRoll.UseVisualStyleBackColor = true;
            this.btnRightRoll.Click += new System.EventHandler(this.btnRightRoll_Click);
            // 
            // btnLeftRoll
            // 
            this.btnLeftRoll.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLeftRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLeftRoll.Location = new System.Drawing.Point(300, 3);
            this.btnLeftRoll.Name = "btnLeftRoll";
            this.btnLeftRoll.Size = new System.Drawing.Size(170, 107);
            this.btnLeftRoll.TabIndex = 2;
            this.btnLeftRoll.Text = "<<";
            this.btnLeftRoll.UseVisualStyleBackColor = true;
            this.btnLeftRoll.Click += new System.EventHandler(this.btnLeftRoll_Click);
            // 
            // lbCurrentPage
            // 
            this.lbCurrentPage.AutoSize = true;
            this.lbCurrentPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbCurrentPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCurrentPage.Location = new System.Drawing.Point(202, 0);
            this.lbCurrentPage.Name = "lbCurrentPage";
            this.lbCurrentPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbCurrentPage.Size = new System.Drawing.Size(92, 29);
            this.lbCurrentPage.TabIndex = 3;
            this.lbCurrentPage.Text = "lbPage";
            // 
            // btnRightRoll2
            // 
            this.btnRightRoll2.BackColor = System.Drawing.SystemColors.Control;
            this.btnRightRoll2.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRightRoll2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRightRoll2.Location = new System.Drawing.Point(776, 0);
            this.btnRightRoll2.Name = "btnRightRoll2";
            this.btnRightRoll2.Size = new System.Drawing.Size(100, 487);
            this.btnRightRoll2.TabIndex = 4;
            this.btnRightRoll2.Text = ">>";
            this.btnRightRoll2.UseVisualStyleBackColor = false;
            this.btnRightRoll2.Click += new System.EventHandler(this.btnRightRoll_Click);
            // 
            // btnLeftRoll2
            // 
            this.btnLeftRoll2.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLeftRoll2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLeftRoll2.Location = new System.Drawing.Point(0, 0);
            this.btnLeftRoll2.Name = "btnLeftRoll2";
            this.btnLeftRoll2.Size = new System.Drawing.Size(100, 487);
            this.btnLeftRoll2.TabIndex = 5;
            this.btnLeftRoll2.Text = "<<";
            this.btnLeftRoll2.UseVisualStyleBackColor = true;
            this.btnLeftRoll2.Click += new System.EventHandler(this.btnLeftRoll_Click);
            // 
            // PluListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(876, 609);
            this.Controls.Add(this.PluListGrid);
            this.Controls.Add(this.btnLeftRoll2);
            this.Controls.Add(this.btnRightRoll2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор PLU";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PluListForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel PluListGrid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnRightRoll;
        private System.Windows.Forms.Button btnLeftRoll;
        private System.Windows.Forms.Label lbCurrentPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRightRoll2;
        private System.Windows.Forms.Button btnLeftRoll2;
    }
}