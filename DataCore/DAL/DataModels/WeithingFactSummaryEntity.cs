// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.Utils;
using System;

namespace DataCore.DAL.DataModels
{
    public class WeithingFactSummaryEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime WeithingDate { get; set; }

        public virtual string WeithingDay => WeithingDate == default || WeithingDate == DateTime.MaxValue || WeithingDate == DateTime.MinValue
            ? string.Empty : EnumUtils.GetDayOfWeekRu(WeithingDate.DayOfWeek);
        public virtual int Count { get; set; } = default;
        public virtual string Scale { get; set; } = string.Empty;
        public virtual string Host { get; set; } = string.Empty;
        public virtual string Printer { get; set; } = string.Empty;

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(WeithingDate)}: {WeithingDate}. " +
                   $"{nameof(WeithingDay)}: {WeithingDay}. " +
                   $"{nameof(Count)}: {Count}. " +
                   $"{nameof(Scale)}: {Scale}. " +
                   $"{nameof(Host)}: {Host}. " +
                   $"{nameof(Printer)}: {Printer}. ";
        }

        public virtual bool Equals(WeithingFactSummaryEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return
                   Equals(WeithingDate, entity.WeithingDate) &&
                   Equals(WeithingDay, entity.WeithingDay) &&
                   Equals(Count, entity.Count) &&
                   Equals(Scale, entity.Scale) &&
                   Equals(Host, entity.Host) &&
                   Equals(Printer, entity.Printer);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WeithingFactSummaryEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new WeithingFactSummaryEntity());
        }

        public virtual bool EqualsDefault()
        {
            return
                   Equals(WeithingDate, default(DateTime)) &&
                   Equals(WeithingDay, string.Empty) &&
                   Equals(Count, default(int)) &&
                   Equals(Scale, default(string)) &&
                   Equals(Host, default(string)) &&
                   Equals(Printer, default(string));
        }

        public object Clone()
        {
            return new WeithingFactSummaryEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                WeithingDate = WeithingDate,
                Count = Count,
                Scale = Scale,
                Host = Host,
                Printer = Printer,
            };
        }

        #endregion
    }
}
