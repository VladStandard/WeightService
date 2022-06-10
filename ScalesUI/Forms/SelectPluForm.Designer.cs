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
            this.tableLayoutPanelPlu = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelActions = new System.Windows.Forms.TableLayoutPanel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonRightRoll = new System.Windows.Forms.Button();
            this.labelCurrentPage = new System.Windows.Forms.Label();
            this.buttonLeftRoll = new System.Windows.Forms.Button();
            this.tableLayoutPanelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelPlu
            // 
            this.tableLayoutPanelPlu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelPlu.AutoSize = true;
            this.tableLayoutPanelPlu.ColumnCount = 3;
            this.tableLayoutPanelPlu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPlu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPlu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPlu.Font = new System.Drawing.Font("Cascadia Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableLayoutPanelPlu.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelPlu.Name = "tableLayoutPanelPlu";
            this.tableLayoutPanelPlu.RowCount = 2;
            this.tableLayoutPanelPlu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPlu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPlu.Size = new System.Drawing.Size(876, 503);
            this.tableLayoutPanelPlu.TabIndex = 3;
            // 
            // tableLayoutPanelActions
            // 
            this.tableLayoutPanelActions.ColumnCount = 4;
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelActions.Controls.Add(this.buttonClose, 3, 0);
            this.tableLayoutPanelActions.Controls.Add(this.buttonRightRoll, 2, 0);
            this.tableLayoutPanelActions.Controls.Add(this.labelCurrentPage, 1, 0);
            this.tableLayoutPanelActions.Controls.Add(this.buttonLeftRoll, 0, 0);
            this.tableLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelActions.Font = new System.Drawing.Font("Cascadia Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableLayoutPanelActions.Location = new System.Drawing.Point(0, 509);
            this.tableLayoutPanelActions.Name = "tableLayoutPanelActions";
            this.tableLayoutPanelActions.RowCount = 1;
            this.tableLayoutPanelActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelActions.Size = new System.Drawing.Size(876, 100);
            this.tableLayoutPanelActions.TabIndex = 4;
            // 
            // buttonClose
            // 
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonClose.Font = new System.Drawing.Font("Cascadia Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClose.Location = new System.Drawing.Point(659, 2);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(215, 96);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // buttonRightRoll
            // 
            this.buttonRightRoll.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRightRoll.Font = new System.Drawing.Font("Cascadia Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRightRoll.Location = new System.Drawing.Point(440, 2);
            this.buttonRightRoll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRightRoll.Name = "buttonRightRoll";
            this.buttonRightRoll.Size = new System.Drawing.Size(215, 96);
            this.buttonRightRoll.TabIndex = 6;
            this.buttonRightRoll.Text = ">>";
            this.buttonRightRoll.UseVisualStyleBackColor = true;
            this.buttonRightRoll.Click += new System.EventHandler(this.ButtonNextRoll_Click);
            // 
            // labelCurrentPage
            // 
            this.labelCurrentPage.AutoSize = true;
            this.labelCurrentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCurrentPage.Font = new System.Drawing.Font("Cascadia Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCurrentPage.Location = new System.Drawing.Point(221, 2);
            this.labelCurrentPage.Margin = new System.Windows.Forms.Padding(2);
            this.labelCurrentPage.Name = "labelCurrentPage";
            this.labelCurrentPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelCurrentPage.Size = new System.Drawing.Size(215, 96);
            this.labelCurrentPage.TabIndex = 5;
            this.labelCurrentPage.Text = "labelCurrentPage";
            this.labelCurrentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLeftRoll
            // 
            this.buttonLeftRoll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeftRoll.Font = new System.Drawing.Font("Cascadia Code", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLeftRoll.Location = new System.Drawing.Point(2, 2);
            this.buttonLeftRoll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLeftRoll.Name = "buttonLeftRoll";
            this.buttonLeftRoll.Size = new System.Drawing.Size(215, 96);
            this.buttonLeftRoll.TabIndex = 4;
            this.buttonLeftRoll.Text = "<<";
            this.buttonLeftRoll.UseVisualStyleBackColor = true;
            this.buttonLeftRoll.Click += new System.EventHandler(this.ButtonPreviousRoll_Click);
            // 
            // SelectPluForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(876, 609);
            this.Controls.Add(this.tableLayoutPanelActions);
            this.Controls.Add(this.tableLayoutPanelPlu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectPluForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор PLU";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PluListForm_Load);
            this.tableLayoutPanelActions.ResumeLayout(false);
            this.tableLayoutPanelActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPlu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelActions;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonRightRoll;
        private System.Windows.Forms.Label labelCurrentPage;
        private System.Windows.Forms.Button buttonLeftRoll;
    }
}