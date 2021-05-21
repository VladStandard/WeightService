// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsExamples
{
    public partial class FormMain : Form
    {

        #region Methods

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            GetFonts(fieldFontFamily);
            SetFont(fieldFontFamily);
        }


        private void GetFonts(ComboBox comboBoxFontFamily)
        {
            comboBoxFontFamily.Items.Clear();
            using (var fontCollection = new InstalledFontCollection())
            {
                foreach (var fontFamily in fontCollection.Families)
                {
                    comboBoxFontFamily.Items.Add(fontFamily.Name);
                }
            }
        }

        private void SetFont(ComboBox comboBoxFontFamily)
        {
            if (comboBoxFontFamily?.Items?.Count > 0)
            {
                for (var i = 0; i < comboBoxFontFamily.Items.Count; i++)
                {
                    if (comboBoxFontFamily.Items[i].Equals(Font.FontFamily.Name))
                    {
                        comboBoxFontFamily.SelectedIndex = i;
                        break;
                    }
                }
            }
            //textBoxSize.Text = Font.Size.ToString(CultureInfo.InvariantCulture);
        }



        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        
        #endregion
    }
}
