// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WeightCore.Gui
{
    public static class GridCustomizatorClass
    {
        #region Public and private methods

        private static void GridCustomizator(TableLayoutPanel panel, ushort columnCount, ushort rowCount)
        {
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();
            panel.ColumnCount = 0;
            panel.RowCount = 0;
            AddColumns(panel, columnCount);
            AddRows(panel, rowCount);
        }

        private static void AddColumns(TableLayoutPanel panel, ushort columnCount)
        {
            ushort width = (ushort)(100 / columnCount);
            for (ushort i = 0; i < columnCount; i++)
            {
                panel.ColumnCount += 1;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
            }
        }

        private static void AddRows(TableLayoutPanel panel, ushort rowCount)
        {
            ushort height = (ushort)(100 / rowCount);
            for (ushort i = 0; i < rowCount; i++)
            {
                panel.RowCount += 1;
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, height));
            }
        }

        private static void DropChildsControl(TableLayoutPanel panel)
        {
            panel.Controls.Clear();
        }

        public static void PageBuilder(TableLayoutPanel panel, Control[,] controls)
        {
            DropChildsControl(panel);
            GridCustomizator(panel, (ushort)(controls.GetUpperBound(0) + 1), (ushort)(controls.GetUpperBound(1) + 1));

            for (ushort column = 0; column <= controls.GetUpperBound(0); column++)
            {
                for (ushort row = 0; row <= controls.GetUpperBound(1); row++)
                {
                    Control control = controls[column, row];
                    if (control != null)
                        panel.Controls.Add(control, column, row);
                }
            }
        }

        #endregion
    }
}
