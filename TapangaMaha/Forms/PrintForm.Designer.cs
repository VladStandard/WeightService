namespace TapangaMaha.Forms
{
    partial class PrintForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPalletSizeNext = new System.Windows.Forms.Button();
            this.btnPalletSizePrev = new System.Windows.Forms.Button();
            this.lbPluName = new System.Windows.Forms.Label();
            this.lbPalletSize = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbProdDate = new System.Windows.Forms.Label();
            this.lKneadingValue = new System.Windows.Forms.Label();
            this.lbKneading = new System.Windows.Forms.Label();
            this.lbProductDate = new System.Windows.Forms.Label();
            this.lbPalletSize1 = new System.Windows.Forms.Label();
            this.btnKneadingPrev = new System.Windows.Forms.Button();
            this.btnKneadingNext = new System.Windows.Forms.Button();
            this.btnPalletSize10 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnClose);
            this.flowLayoutPanel1.Controls.Add(this.btnPrint);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 384);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(902, 135);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClose.Location = new System.Drawing.Point(724, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(175, 132);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrint.Location = new System.Drawing.Point(543, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(175, 132);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.73159F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.26841F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tableLayoutPanel1.Controls.Add(this.btnPalletSizeNext, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnPalletSizePrev, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbPluName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbPalletSize, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbProdDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lKneadingValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbKneading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbProductDate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbPalletSize1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnKneadingPrev, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnKneadingNext, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPalletSize10, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.button3, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(902, 384);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnPalletSizeNext
            // 
            this.btnPalletSizeNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPalletSizeNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPalletSizeNext.Location = new System.Drawing.Point(576, 265);
            this.btnPalletSizeNext.Name = "btnPalletSizeNext";
            this.btnPalletSizeNext.Size = new System.Drawing.Size(154, 116);
            this.btnPalletSizeNext.TabIndex = 14;
            this.btnPalletSizeNext.Text = ">>";
            this.btnPalletSizeNext.UseVisualStyleBackColor = true;
            this.btnPalletSizeNext.Click += new System.EventHandler(this.btnPalletSizeNext_Click);
            // 
            // btnPalletSizePrev
            // 
            this.btnPalletSizePrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPalletSizePrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPalletSizePrev.Location = new System.Drawing.Point(416, 265);
            this.btnPalletSizePrev.Name = "btnPalletSizePrev";
            this.btnPalletSizePrev.Size = new System.Drawing.Size(154, 116);
            this.btnPalletSizePrev.TabIndex = 13;
            this.btnPalletSizePrev.Text = "<<";
            this.btnPalletSizePrev.UseVisualStyleBackColor = true;
            this.btnPalletSizePrev.Click += new System.EventHandler(this.btnPalletSizePrev_Click);
            // 
            // lbPluName
            // 
            this.lbPluName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lbPluName, 5);
            this.lbPluName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPluName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPluName.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbPluName.Location = new System.Drawing.Point(3, 0);
            this.lbPluName.Name = "lbPluName";
            this.lbPluName.Size = new System.Drawing.Size(896, 60);
            this.lbPluName.TabIndex = 2;
            this.lbPluName.Text = "label1";
            this.lbPluName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPalletSize
            // 
            this.lbPalletSize.AutoSize = true;
            this.lbPalletSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPalletSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPalletSize.Location = new System.Drawing.Point(221, 262);
            this.lbPalletSize.Name = "lbPalletSize";
            this.lbPalletSize.Size = new System.Drawing.Size(189, 122);
            this.lbPalletSize.TabIndex = 12;
            this.lbPalletSize.Text = "lbPalletSize";
            this.lbPalletSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(576, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 95);
            this.button2.TabIndex = 11;
            this.button2.Text = ">>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(416, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 95);
            this.button1.TabIndex = 10;
            this.button1.Text = "<<";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbProdDate
            // 
            this.lbProdDate.AutoSize = true;
            this.lbProdDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProdDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbProdDate.Location = new System.Drawing.Point(221, 161);
            this.lbProdDate.Name = "lbProdDate";
            this.lbProdDate.Size = new System.Drawing.Size(189, 101);
            this.lbProdDate.TabIndex = 9;
            this.lbProdDate.Text = "01.01.2020";
            this.lbProdDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lKneadingValue
            // 
            this.lKneadingValue.AutoSize = true;
            this.lKneadingValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lKneadingValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lKneadingValue.Location = new System.Drawing.Point(221, 60);
            this.lKneadingValue.Name = "lKneadingValue";
            this.lKneadingValue.Size = new System.Drawing.Size(189, 101);
            this.lKneadingValue.TabIndex = 5;
            this.lKneadingValue.Text = "lKneading";
            this.lKneadingValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbKneading
            // 
            this.lbKneading.AutoSize = true;
            this.lbKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbKneading.Location = new System.Drawing.Point(3, 60);
            this.lbKneading.Name = "lbKneading";
            this.lbKneading.Size = new System.Drawing.Size(212, 101);
            this.lbKneading.TabIndex = 0;
            this.lbKneading.Text = "Замес";
            this.lbKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbProductDate
            // 
            this.lbProductDate.AutoSize = true;
            this.lbProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbProductDate.Location = new System.Drawing.Point(3, 161);
            this.lbProductDate.Name = "lbProductDate";
            this.lbProductDate.Size = new System.Drawing.Size(212, 101);
            this.lbProductDate.TabIndex = 1;
            this.lbProductDate.Text = "Дата производства";
            this.lbProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPalletSize1
            // 
            this.lbPalletSize1.AutoSize = true;
            this.lbPalletSize1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPalletSize1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbPalletSize1.Location = new System.Drawing.Point(3, 262);
            this.lbPalletSize1.Name = "lbPalletSize1";
            this.lbPalletSize1.Size = new System.Drawing.Size(212, 122);
            this.lbPalletSize1.TabIndex = 2;
            this.lbPalletSize1.Text = "Количество на паллете";
            this.lbPalletSize1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnKneadingPrev
            // 
            this.btnKneadingPrev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKneadingPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnKneadingPrev.Location = new System.Drawing.Point(416, 63);
            this.btnKneadingPrev.Name = "btnKneadingPrev";
            this.btnKneadingPrev.Size = new System.Drawing.Size(154, 95);
            this.btnKneadingPrev.TabIndex = 3;
            this.btnKneadingPrev.Text = "<<";
            this.btnKneadingPrev.UseVisualStyleBackColor = true;
            this.btnKneadingPrev.Click += new System.EventHandler(this.btnKneadingPrev_Click);
            // 
            // btnKneadingNext
            // 
            this.btnKneadingNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKneadingNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnKneadingNext.Location = new System.Drawing.Point(576, 63);
            this.btnKneadingNext.Name = "btnKneadingNext";
            this.btnKneadingNext.Size = new System.Drawing.Size(154, 95);
            this.btnKneadingNext.TabIndex = 4;
            this.btnKneadingNext.Text = ">>";
            this.btnKneadingNext.UseVisualStyleBackColor = true;
            this.btnKneadingNext.Click += new System.EventHandler(this.btnKneadingNext_Click);
            // 
            // btnPalletSize10
            // 
            this.btnPalletSize10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPalletSize10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPalletSize10.Location = new System.Drawing.Point(736, 265);
            this.btnPalletSize10.Name = "btnPalletSize10";
            this.btnPalletSize10.Size = new System.Drawing.Size(163, 116);
            this.btnPalletSize10.TabIndex = 15;
            this.btnPalletSize10.Text = "+10";
            this.btnPalletSize10.UseVisualStyleBackColor = true;
            this.btnPalletSize10.Click += new System.EventHandler(this.btnPalletSize10_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(736, 63);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 95);
            this.button3.TabIndex = 16;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button3_KeyDown);
            this.button3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.button3_KeyUp);
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 519);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PrintForm";
            this.Load += new System.EventHandler(this.PrintForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbKneading;
        private System.Windows.Forms.Label lbProductDate;
        private System.Windows.Forms.Label lbPalletSize1;
        private System.Windows.Forms.Button btnKneadingPrev;
        private System.Windows.Forms.Button btnKneadingNext;
        private System.Windows.Forms.Label lKneadingValue;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPalletSizeNext;
        private System.Windows.Forms.Button btnPalletSizePrev;
        private System.Windows.Forms.Label lbPalletSize;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbProdDate;
        private System.Windows.Forms.Button btnPalletSize10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lbPluName;
    }
}