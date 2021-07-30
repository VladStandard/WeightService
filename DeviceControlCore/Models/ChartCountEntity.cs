using System;

namespace DeviceControlCore.Models
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
