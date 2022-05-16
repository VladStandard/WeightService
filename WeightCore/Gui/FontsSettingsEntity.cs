// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;

namespace ScalesUI.Forms
{
    public class FontsSettingsEntity
    {
        #region Public and private fields and properties

        public Font FontButtons { get; set; }
        public Font FontMinimum { get; set; }
        public Font FontLabelsBlack { get; set; }
        public Font FontLabelsGray { get; set; }
        public Font FontLabelsMaximum { get; set; }
        public Font FontLabelsTitle { get; set; }

        #endregion

        #region Constructor and destructor

        public FontsSettingsEntity()
        {
            Resize(7.00f);
        }

        public void Transform(int width, int height)
        {
            float baseSize;
            if (width >= 1920 && height >= 1080)
            {
                baseSize = 15.00f;
            }
            else if (width >= 1600 && height >= 1024)
            {
                baseSize = 13.00f;
            }
            else if (width >= 1366 && height >= 768)
            {
                baseSize = 11.00f;
            }
            else if (width >= 1024 && height >= 768)
            {
                baseSize = 9.00f;
            }
            else
            {
                baseSize = 8.00f;
            }

            Resize(baseSize);
        }

        private void Resize(float baseSize)
        {
            FontMinimum = new Font("Microsoft Sans Serif", baseSize, FontStyle.Regular, GraphicsUnit.Point, 204);
            FontLabelsGray = new Font("Microsoft Sans Serif", baseSize + 1.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
            FontButtons = new Font("Microsoft Sans Serif", baseSize + 4.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
            FontLabelsBlack = new Font("Microsoft Sans Serif", baseSize + 4.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
            FontLabelsTitle = new Font("Microsoft Sans Serif", baseSize + 7.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
            FontLabelsMaximum = new Font("Microsoft Sans Serif", baseSize + 16.00f, FontStyle.Bold, GraphicsUnit.Point, 204);
        }

        #endregion
    }
}
