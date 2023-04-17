namespace ControlsExamples
{
    partial class FormMain
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
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonDefault = new System.Windows.Forms.Button();
			this.groupBoxSettings = new System.Windows.Forms.GroupBox();
			this.labelCount = new System.Windows.Forms.Label();
			this.fieldFontSize = new System.Windows.Forms.TextBox();
			this.labelFontSize = new System.Windows.Forms.Label();
			this.fieldFontFamily = new System.Windows.Forms.ComboBox();
			this.labelFontFamilty = new System.Windows.Forms.Label();
			this.labelTopText = new System.Windows.Forms.Label();
			this.fieldHeader = new System.Windows.Forms.TextBox();
			this.labelHeight = new System.Windows.Forms.Label();
			this.fieldHeight = new System.Windows.Forms.TextBox();
			this.labelWidth = new System.Windows.Forms.Label();
			this.fieldWidth = new System.Windows.Forms.TextBox();
			this.fieldTopUseShift = new System.Windows.Forms.CheckBox();
			this.fieldLeftUseShift = new System.Windows.Forms.CheckBox();
			this.labelTopShift = new System.Windows.Forms.Label();
			this.fieldTopShift = new System.Windows.Forms.TextBox();
			this.labelLeftShift = new System.Windows.Forms.Label();
			this.fieldLeftShift = new System.Windows.Forms.TextBox();
			this.labelTop = new System.Windows.Forms.Label();
			this.fieldTop = new System.Windows.Forms.TextBox();
			this.labelLeft = new System.Windows.Forms.Label();
			this.fieldLeft = new System.Windows.Forms.TextBox();
			this.buttonRemoveAll = new System.Windows.Forms.Button();
			this.buttonChange = new System.Windows.Forms.Button();
			this.fieldTopText = new System.Windows.Forms.TextBox();
			this.groupBoxSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(0, 0);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 13;
			// 
			// buttonRemove
			// 
			this.buttonRemove.Location = new System.Drawing.Point(0, 0);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(75, 23);
			this.buttonRemove.TabIndex = 12;
			// 
			// buttonDefault
			// 
			this.buttonDefault.Location = new System.Drawing.Point(0, 0);
			this.buttonDefault.Name = "buttonDefault";
			this.buttonDefault.Size = new System.Drawing.Size(75, 23);
			this.buttonDefault.TabIndex = 11;
			// 
			// groupBoxSettings
			// 
			this.groupBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxSettings.Controls.Add(this.labelCount);
			this.groupBoxSettings.Controls.Add(this.fieldFontSize);
			this.groupBoxSettings.Controls.Add(this.labelFontSize);
			this.groupBoxSettings.Controls.Add(this.fieldFontFamily);
			this.groupBoxSettings.Controls.Add(this.labelFontFamilty);
			this.groupBoxSettings.Controls.Add(this.labelTopText);
			this.groupBoxSettings.Controls.Add(this.fieldHeader);
			this.groupBoxSettings.Controls.Add(this.labelHeight);
			this.groupBoxSettings.Controls.Add(this.fieldHeight);
			this.groupBoxSettings.Controls.Add(this.labelWidth);
			this.groupBoxSettings.Controls.Add(this.fieldWidth);
			this.groupBoxSettings.Controls.Add(this.fieldTopUseShift);
			this.groupBoxSettings.Controls.Add(this.fieldLeftUseShift);
			this.groupBoxSettings.Controls.Add(this.labelTopShift);
			this.groupBoxSettings.Controls.Add(this.fieldTopShift);
			this.groupBoxSettings.Controls.Add(this.labelLeftShift);
			this.groupBoxSettings.Controls.Add(this.fieldLeftShift);
			this.groupBoxSettings.Controls.Add(this.labelTop);
			this.groupBoxSettings.Controls.Add(this.fieldTop);
			this.groupBoxSettings.Controls.Add(this.labelLeft);
			this.groupBoxSettings.Controls.Add(this.fieldLeft);
			this.groupBoxSettings.Location = new System.Drawing.Point(155, 15);
			this.groupBoxSettings.Name = "groupBoxSettings";
			this.groupBoxSettings.Size = new System.Drawing.Size(845, 135);
			this.groupBoxSettings.TabIndex = 10;
			this.groupBoxSettings.TabStop = false;
			this.groupBoxSettings.Text = "Settings";
			// 
			// labelCount
			// 
			this.labelCount.Location = new System.Drawing.Point(9, 96);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new System.Drawing.Size(305, 30);
			this.labelCount.TabIndex = 33;
			this.labelCount.Text = "Entities count";
			this.labelCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldFontSize
			// 
			this.fieldFontSize.Location = new System.Drawing.Point(540, 20);
			this.fieldFontSize.Name = "fieldFontSize";
			this.fieldFontSize.Size = new System.Drawing.Size(60, 31);
			this.fieldFontSize.TabIndex = 32;
			this.fieldFontSize.Text = "8";
			this.fieldFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelFontSize
			// 
			this.labelFontSize.Location = new System.Drawing.Point(435, 20);
			this.labelFontSize.Name = "labelFontSize";
			this.labelFontSize.Size = new System.Drawing.Size(100, 30);
			this.labelFontSize.TabIndex = 31;
			this.labelFontSize.Text = "Font size";
			this.labelFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldFontFamily
			// 
			this.fieldFontFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fieldFontFamily.FormattingEnabled = true;
			this.fieldFontFamily.Location = new System.Drawing.Point(115, 19);
			this.fieldFontFamily.Name = "fieldFontFamily";
			this.fieldFontFamily.Size = new System.Drawing.Size(295, 31);
			this.fieldFontFamily.TabIndex = 30;
			// 
			// labelFontFamilty
			// 
			this.labelFontFamilty.Location = new System.Drawing.Point(9, 20);
			this.labelFontFamilty.Name = "labelFontFamilty";
			this.labelFontFamilty.Size = new System.Drawing.Size(100, 30);
			this.labelFontFamilty.TabIndex = 29;
			this.labelFontFamilty.Text = "Font family";
			this.labelFontFamilty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTopText
			// 
			this.labelTopText.Location = new System.Drawing.Point(9, 57);
			this.labelTopText.Name = "labelTopText";
			this.labelTopText.Size = new System.Drawing.Size(100, 30);
			this.labelTopText.TabIndex = 11;
			this.labelTopText.Text = "Top text";
			this.labelTopText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldHeader
			// 
			this.fieldHeader.Location = new System.Drawing.Point(115, 56);
			this.fieldHeader.Name = "fieldHeader";
			this.fieldHeader.Size = new System.Drawing.Size(199, 31);
			this.fieldHeader.TabIndex = 12;
			this.fieldHeader.Text = "Header";
			this.fieldHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelHeight
			// 
			this.labelHeight.Location = new System.Drawing.Point(710, 97);
			this.labelHeight.Name = "labelHeight";
			this.labelHeight.Size = new System.Drawing.Size(60, 30);
			this.labelHeight.TabIndex = 27;
			this.labelHeight.Text = "Height";
			this.labelHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldHeight
			// 
			this.fieldHeight.Location = new System.Drawing.Point(780, 96);
			this.fieldHeight.Name = "fieldHeight";
			this.fieldHeight.Size = new System.Drawing.Size(50, 31);
			this.fieldHeight.TabIndex = 28;
			this.fieldHeight.Text = "100";
			this.fieldHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelWidth
			// 
			this.labelWidth.Location = new System.Drawing.Point(710, 57);
			this.labelWidth.Name = "labelWidth";
			this.labelWidth.Size = new System.Drawing.Size(60, 30);
			this.labelWidth.TabIndex = 25;
			this.labelWidth.Text = "Width";
			this.labelWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldWidth
			// 
			this.fieldWidth.Location = new System.Drawing.Point(780, 57);
			this.fieldWidth.Name = "fieldWidth";
			this.fieldWidth.Size = new System.Drawing.Size(50, 31);
			this.fieldWidth.TabIndex = 26;
			this.fieldWidth.Text = "80";
			this.fieldWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// fieldTopUseShift
			// 
			this.fieldTopUseShift.AutoSize = true;
			this.fieldTopUseShift.Location = new System.Drawing.Point(418, 99);
			this.fieldTopUseShift.Name = "fieldTopUseShift";
			this.fieldTopUseShift.Size = new System.Drawing.Size(120, 27);
			this.fieldTopUseShift.TabIndex = 20;
			this.fieldTopUseShift.Text = "TopUseShift";
			this.fieldTopUseShift.UseVisualStyleBackColor = true;
			// 
			// fieldLeftUseShift
			// 
			this.fieldLeftUseShift.AutoSize = true;
			this.fieldLeftUseShift.Checked = true;
			this.fieldLeftUseShift.CheckState = System.Windows.Forms.CheckState.Checked;
			this.fieldLeftUseShift.Location = new System.Drawing.Point(418, 60);
			this.fieldLeftUseShift.Name = "fieldLeftUseShift";
			this.fieldLeftUseShift.Size = new System.Drawing.Size(122, 27);
			this.fieldLeftUseShift.TabIndex = 19;
			this.fieldLeftUseShift.Text = "LeftUseShift";
			this.fieldLeftUseShift.UseVisualStyleBackColor = true;
			// 
			// labelTopShift
			// 
			this.labelTopShift.Location = new System.Drawing.Point(550, 97);
			this.labelTopShift.Name = "labelTopShift";
			this.labelTopShift.Size = new System.Drawing.Size(80, 30);
			this.labelTopShift.TabIndex = 23;
			this.labelTopShift.Text = "TopShift";
			this.labelTopShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldTopShift
			// 
			this.fieldTopShift.Location = new System.Drawing.Point(635, 96);
			this.fieldTopShift.Name = "fieldTopShift";
			this.fieldTopShift.Size = new System.Drawing.Size(50, 31);
			this.fieldTopShift.TabIndex = 24;
			this.fieldTopShift.Text = "10";
			this.fieldTopShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelLeftShift
			// 
			this.labelLeftShift.Location = new System.Drawing.Point(550, 57);
			this.labelLeftShift.Name = "labelLeftShift";
			this.labelLeftShift.Size = new System.Drawing.Size(80, 30);
			this.labelLeftShift.TabIndex = 21;
			this.labelLeftShift.Text = "LeftShift";
			this.labelLeftShift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldLeftShift
			// 
			this.fieldLeftShift.Location = new System.Drawing.Point(635, 57);
			this.fieldLeftShift.Name = "fieldLeftShift";
			this.fieldLeftShift.Size = new System.Drawing.Size(50, 31);
			this.fieldLeftShift.TabIndex = 22;
			this.fieldLeftShift.Text = "10";
			this.fieldLeftShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelTop
			// 
			this.labelTop.Location = new System.Drawing.Point(320, 97);
			this.labelTop.Name = "labelTop";
			this.labelTop.Size = new System.Drawing.Size(40, 30);
			this.labelTop.TabIndex = 17;
			this.labelTop.Text = "Top";
			this.labelTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldTop
			// 
			this.fieldTop.Location = new System.Drawing.Point(360, 96);
			this.fieldTop.Name = "fieldTop";
			this.fieldTop.Size = new System.Drawing.Size(50, 31);
			this.fieldTop.TabIndex = 18;
			this.fieldTop.Text = "150";
			this.fieldTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelLeft
			// 
			this.labelLeft.Location = new System.Drawing.Point(320, 57);
			this.labelLeft.Name = "labelLeft";
			this.labelLeft.Size = new System.Drawing.Size(40, 30);
			this.labelLeft.TabIndex = 15;
			this.labelLeft.Text = "Left";
			this.labelLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fieldLeft
			// 
			this.fieldLeft.Location = new System.Drawing.Point(360, 57);
			this.fieldLeft.Name = "fieldLeft";
			this.fieldLeft.Size = new System.Drawing.Size(50, 31);
			this.fieldLeft.TabIndex = 16;
			this.fieldLeft.Text = "15";
			this.fieldLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// buttonRemoveAll
			// 
			this.buttonRemoveAll.Location = new System.Drawing.Point(0, 0);
			this.buttonRemoveAll.Name = "buttonRemoveAll";
			this.buttonRemoveAll.Size = new System.Drawing.Size(75, 23);
			this.buttonRemoveAll.TabIndex = 1;
			// 
			// buttonChange
			// 
			this.buttonChange.Location = new System.Drawing.Point(0, 0);
			this.buttonChange.Name = "buttonChange";
			this.buttonChange.Size = new System.Drawing.Size(75, 23);
			this.buttonChange.TabIndex = 0;
			// 
			// fieldTopText
			// 
			this.fieldTopText.Location = new System.Drawing.Point(115, 56);
			this.fieldTopText.Name = "fieldTopText";
			this.fieldTopText.Size = new System.Drawing.Size(199, 20);
			this.fieldTopText.TabIndex = 12;
			this.fieldTopText.Text = "Header";
			this.fieldTopText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 529);
			this.Controls.Add(this.buttonChange);
			this.Controls.Add(this.buttonRemoveAll);
			this.Controls.Add(this.groupBoxSettings);
			this.Controls.Add(this.buttonDefault);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonAdd);
			this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "FormMain";
			this.Text = "Controls examples";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.groupBoxSettings.ResumeLayout(false);
			this.groupBoxSettings.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.TextBox fieldHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.TextBox fieldWidth;
        private System.Windows.Forms.CheckBox fieldTopUseShift;
        private System.Windows.Forms.CheckBox fieldLeftUseShift;
        private System.Windows.Forms.Label labelTopShift;
        private System.Windows.Forms.TextBox fieldTopShift;
        private System.Windows.Forms.Label labelLeftShift;
        private System.Windows.Forms.TextBox fieldLeftShift;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.TextBox fieldTop;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.TextBox fieldLeft;
        private System.Windows.Forms.Label labelTopText;
        private System.Windows.Forms.TextBox fieldHeader;
        private System.Windows.Forms.Button buttonRemoveAll;
        private System.Windows.Forms.ComboBox fieldFontFamily;
        private System.Windows.Forms.Label labelFontFamilty;
        private System.Windows.Forms.TextBox fieldFontSize;
        private System.Windows.Forms.Label labelFontSize;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button buttonChange;
        private System.Windows.Forms.TextBox fieldTopText;
    }
}

