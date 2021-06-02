// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace UICommon
{
    public static class GridCustomizatorClass
    {
        #region Public and private methods

        public static void GridCustomizator(TableLayoutPanel panel, int columnCount, int rowCount)
        {
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();
            panel.ColumnCount = 0;
            panel.RowCount = 0;
            AddColumns(panel, columnCount);
            AddRows(panel, rowCount);
        }

        private static void AddColumns(TableLayoutPanel panel, int columnCount)
        {
            //var width = (float)(100 / columnCount);
            var width = 100 / columnCount;
            for (var i = 0; i < columnCount; i++)
            {
                panel.ColumnCount += 1;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
            }
        }

        private static void AddRows(TableLayoutPanel panel, int rowCount)
        {
            //var height = (float)(100 / rowCount);
            var height = 100 / rowCount;
            for (var i = 0; i < rowCount; i++)
            {
                panel.RowCount += 1;
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, height));
            }
        }

        public static void DropChildsControl(TableLayoutPanel panel)
        {
            panel.Controls.Clear();
        }

        public static void PageBuilder(TableLayoutPanel panel, Control[,] controls)
        {
            DropChildsControl(panel);
            GridCustomizator(panel, controls.GetUpperBound(0) + 1, controls.GetUpperBound(1) + 1);

            for (var x = 0; x <= controls.GetUpperBound(0); x++)
            {
                for (var y = 0; y <= controls.GetUpperBound(1); y++)
                {
                    if (controls[x, y] != null)
                        panel.Controls.Add(controls[x, y], x, y);
                }
            }
        }

        #endregion
    }
}
