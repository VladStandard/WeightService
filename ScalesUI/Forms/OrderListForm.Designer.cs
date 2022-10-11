//namespace ScalesUI.Forms
//{
//    partial class OrderListForm
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
//            if (disposing && (components is not null))
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
//            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
//            this.btnClose = new System.Windows.Forms.Button();
//            this.btnRightRoll = new System.Windows.Forms.Button();
//            this.btnLeftRoll = new System.Windows.Forms.Button();
//            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
//            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
//            this.flowLayoutPanel1.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // flowLayoutPanel1
//            // 
//            this.flowLayoutPanel1.Controls.Add(this.btnClose);
//            this.flowLayoutPanel1.Controls.Add(this.btnRightRoll);
//            this.flowLayoutPanel1.Controls.Add(this.btnLeftRoll);
//            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
//            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 435);
//            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
//            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 70);
//            this.flowLayoutPanel1.TabIndex = 0;
//            // 
//            // btnClose
//            // 
//            this.btnClose.Location = new System.Drawing.Point(627, 3);
//            this.btnClose.Name = "btnClose";
//            this.btnClose.Size = new System.Drawing.Size(170, 67);
//            this.btnClose.TabIndex = 0;
//            this.btnClose.Text = "Закрыть";
//            this.btnClose.UseVisualStyleBackColor = true;
//            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
//            // 
//            // btnRightRoll
//            // 
//            this.btnRightRoll.Location = new System.Drawing.Point(451, 3);
//            this.btnRightRoll.Name = "btnRightRoll";
//            this.btnRightRoll.Size = new System.Drawing.Size(170, 67);
//            this.btnRightRoll.TabIndex = 1;
//            this.btnRightRoll.Text = ">>";
//            this.btnRightRoll.UseVisualStyleBackColor = true;
//            this.btnRightRoll.Click += new System.EventHandler(this.BtnRightRoll_Click);
//            // 
//            // btnLeftRoll
//            // 
//            this.btnLeftRoll.Location = new System.Drawing.Point(275, 3);
//            this.btnLeftRoll.Name = "btnLeftRoll";
//            this.btnLeftRoll.Size = new System.Drawing.Size(170, 67);
//            this.btnLeftRoll.TabIndex = 2;
//            this.btnLeftRoll.Text = "<<";
//            this.btnLeftRoll.UseVisualStyleBackColor = true;
//            this.btnLeftRoll.Click += new System.EventHandler(this.BtnLeftRoll_Click);
//            // 
//            // tableLayoutPanel1
//            // 
//            this.tableLayoutPanel1.ColumnCount = 3;
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
//            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
//            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
//            this.tableLayoutPanel1.RowCount = 2;
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 435);
//            this.tableLayoutPanel1.TabIndex = 1;
//            // 
//            // tableLayoutPanel2
//            // 
//            this.tableLayoutPanel2.ColumnCount = 2;
//            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel2.Location = new System.Drawing.Point(445, 229);
//            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
//            this.tableLayoutPanel2.RowCount = 2;
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
//            this.tableLayoutPanel2.Size = new System.Drawing.Size(1, 1);
//            this.tableLayoutPanel2.TabIndex = 0;
//            // 
//            // OrderListForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(800, 505);
//            this.Controls.Add(this.tableLayoutPanel2);
//            this.Controls.Add(this.tableLayoutPanel1);
//            this.Controls.Add(this.flowLayoutPanel1);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.Name = "OrderListForm";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
//            this.Text = "Надо выбрать задание";
//            this.TopMost = true;
//            this.Load += new System.EventHandler(this.OrderListForm_Load);
//            this.flowLayoutPanel1.ResumeLayout(false);
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
//        private System.Windows.Forms.Button btnClose;
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
//        private System.Windows.Forms.Button btnRightRoll;
//        private System.Windows.Forms.Button btnLeftRoll;
//    }
//}