// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;

namespace ScalesUI.Forms
{
    internal class FontsSettingsEntity
    {
        #region Public and private fields and properties

        public Font FontButtons { get; set; }
        public Font FontComboBox { get; set; }
        public Font FontLabelsBlack { get; set; }
        public Font FontLabelsGray { get; set; }
        public Font FontLabelsMaximum { get; set; }
        public Font FontLabelsTitle { get; set; }

        #endregion

        #region Constructor and destructor

        public FontsSettingsEntity()
        {
            //FontComboBox = new("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular);
            //FontLabelsGray = new("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular);
            //FontLabelsBlack = new("Microsoft Sans Serif", 12.75f, System.Drawing.FontStyle.Bold);
            //FontButtons = new Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 204);
            //FontLabelsTitle = new("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold & System.Drawing.FontStyle.Underline);
            //FontLabelsMaximum = new("Microsoft Sans Serif", 36f, System.Drawing.FontStyle.Bold);
            FontComboBox = new("Microsoft Sans Serif", 8.00f, FontStyle.Regular);
            FontLabelsGray = new("Microsoft Sans Serif", 9.00f, FontStyle.Regular);
            FontLabelsBlack = new("Microsoft Sans Serif", 12.00f, FontStyle.Bold);
            FontButtons = new Font("Microsoft Sans Serif", 12.00F, FontStyle.Bold, GraphicsUnit.Point, 204);
            FontLabelsTitle = new("Microsoft Sans Serif", 20.00f, FontStyle.Bold & FontStyle.Underline);
            FontLabelsMaximum = new("Microsoft Sans Serif", 35f, FontStyle.Bold);
        }

        internal void Setup(float emSize)
        {
            FontComboBox = new Font(FontComboBox.FontFamily, emSize, GraphicsUnit.Point);
            FontLabelsBlack = new Font(FontLabelsBlack.FontFamily, emSize + 1.00f, GraphicsUnit.Point);
            FontLabelsGray = new Font(FontLabelsGray.FontFamily, emSize + 1.00f, GraphicsUnit.Point);
            FontButtons = new Font(FontButtons.FontFamily, emSize + 4.00f, GraphicsUnit.Point);
            FontLabelsTitle = new Font(FontLabelsTitle.FontFamily, emSize + 7.00f, FontStyle.Bold, GraphicsUnit.Point);
            FontLabelsMaximum = new Font(FontLabelsMaximum.FontFamily, emSize + 27.00f, FontStyle.Bold, GraphicsUnit.Point);
        }

        #endregion
    }
}
