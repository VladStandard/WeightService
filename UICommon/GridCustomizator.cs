// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WeightServices.Common
{
    public static class GridCustomizatorClass
    {

        public static void GridCustomizator( TableLayoutPanel panel, int _ColumnCount, int _RowCount)
        {
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();
            panel.ColumnCount = 0;
            panel.RowCount = 0;
            AddColumns(panel, _ColumnCount);
            AddRows(panel, _RowCount);

        }

        private static void AddColumns( TableLayoutPanel panel, int _ColumnCount)
        {
            float width = (float)(100 / (_ColumnCount));
            for (int i = 0; i < _ColumnCount; i++)
            {
                panel.ColumnCount += 1;
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, width));
            }
        }

        private static void AddRows( TableLayoutPanel panel, int _RowCount)
        {
            float height = (float)(100 / _RowCount);
            for (int i = 0; i < _RowCount; i++)
            {
                panel.RowCount += 1;
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, height));
            }
        }

        public static void DropChildsControl( TableLayoutPanel panel)
        {
            panel.Controls.Clear();
        }

        public static void PageBuilder( TableLayoutPanel _Panel, Control[,] _Controls)
        {

            DropChildsControl(_Panel);
            GridCustomizator(_Panel, _Controls.GetUpperBound(0)+1, _Controls.GetUpperBound(1)+1);

            for (int x = 0; x <= _Controls.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= _Controls.GetUpperBound(1); y++)
                {
                    if (_Controls[x, y]!=null)
                        _Panel.Controls.Add(_Controls[x,y], x, y);
                }
            }
        }

    }
}
