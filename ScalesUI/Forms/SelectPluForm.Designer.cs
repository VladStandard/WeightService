namespace ScalesUI.Forms
{
    partial class SelectPluForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.PluListGrid = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRightRoll = new System.Windows.Forms.Button();
            this.buttonLeftRoll = new System.Windows.Forms.Button();
            this.labelCurrentPage = new System.Windows.Forms.Label();
            this.btnRightRoll2 = new System.Windows.Forms.Button();
            this.btnLeftRoll2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClose.Location = new System.Drawing.Point(716, 3);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(150, 107);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // PluListGrid
            // 
            this.PluListGrid.AutoSize = true;
            this.PluListGrid.ColumnCount = 3;
            this.PluListGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PluListGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PluListGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.PluListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluListGrid.Location = new System.Drawing.Point(100, 0);
            this.PluListGrid.Name = "PluListGrid";
            this.PluListGrid.RowCount = 2;
            this.PluListGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PluListGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PluListGrid.Size = new System.Drawing.Size(676, 487);
            this.PluListGrid.TabIndex = 3;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.buttonClose);
            this.flowLayoutPanel.Controls.Add(this.label1);
            this.flowLayoutPanel.Controls.Add(this.buttonRightRoll);
            this.flowLayoutPanel.Controls.Add(this.buttonLeftRoll);
            this.flowLayoutPanel.Controls.Add(this.labelCurrentPage);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 487);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(876, 122);
            this.flowLayoutPanel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(658, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 23);
            this.label1.TabIndex = 4;
            // 
            // buttonRightRoll
            // 
            this.buttonRightRoll.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRightRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRightRoll.Location = new System.Drawing.Point(495, 3);
            this.buttonRightRoll.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonRightRoll.Name = "buttonRightRoll";
            this.buttonRightRoll.Size = new System.Drawing.Size(150, 107);
            this.buttonRightRoll.TabIndex = 1;
            this.buttonRightRoll.Text = ">>";
            this.buttonRightRoll.UseVisualStyleBackColor = true;
            this.buttonRightRoll.Click += new System.EventHandler(this.ButtonRightRoll_Click);
            // 
            // buttonLeftRoll
            // 
            this.buttonLeftRoll.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonLeftRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLeftRoll.Location = new System.Drawing.Point(325, 3);
            this.buttonLeftRoll.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonLeftRoll.Name = "buttonLeftRoll";
            this.buttonLeftRoll.Size = new System.Drawing.Size(150, 107);
            this.buttonLeftRoll.TabIndex = 2;
            this.buttonLeftRoll.Text = "<<";
            this.buttonLeftRoll.UseVisualStyleBackColor = true;
            this.buttonLeftRoll.Click += new System.EventHandler(this.ButtonLeftRoll_Click);
            // 
            // labelCurrentPage
            // 
            this.labelCurrentPage.AutoSize = true;
            this.labelCurrentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCurrentPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrentPage.Location = new System.Drawing.Point(103, 3);
            this.labelCurrentPage.Margin = new System.Windows.Forms.Padding(3);
            this.labelCurrentPage.Name = "labelCurrentPage";
            this.labelCurrentPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelCurrentPage.Size = new System.Drawing.Size(209, 107);
            this.labelCurrentPage.TabIndex = 3;
            this.labelCurrentPage.Text = "labelCurrentPage";
            this.labelCurrentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.btnRightRoll2.Text = ">";
            this.btnRightRoll2.UseVisualStyleBackColor = false;
            this.btnRightRoll2.Click += new System.EventHandler(this.ButtonRightRoll_Click);
            // 
            // btnLeftRoll2
            // 
            this.btnLeftRoll2.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLeftRoll2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLeftRoll2.Location = new System.Drawing.Point(0, 0);
            this.btnLeftRoll2.Name = "btnLeftRoll2";
            this.btnLeftRoll2.Size = new System.Drawing.Size(100, 487);
            this.btnLeftRoll2.TabIndex = 5;
            this.btnLeftRoll2.Text = "<";
            this.btnLeftRoll2.UseVisualStyleBackColor = true;
            this.btnLeftRoll2.Click += new System.EventHandler(this.ButtonLeftRoll_Click);
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
            this.Controls.Add(this.flowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор PLU";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PluListForm_Load);
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TableLayoutPanel PluListGrid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button buttonRightRoll;
        private System.Windows.Forms.Button buttonLeftRoll;
        private System.Windows.Forms.Label labelCurrentPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRightRoll2;
        private System.Windows.Forms.Button btnLeftRoll2;
    }
}