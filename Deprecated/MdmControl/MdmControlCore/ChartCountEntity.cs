// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace MdmControlCore
{
    public class ChartCountEntity
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }

        public ChartCountEntity(DateTime date, int count)
        {
            Date = date;
            Count = count;
        }

        public override string ToString()
        {
            return $"{nameof(Date)}: {Date}. {nameof(Count)}: {Count}. ";
        }
    }
}
