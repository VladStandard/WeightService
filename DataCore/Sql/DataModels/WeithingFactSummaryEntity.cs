// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.DataModels;

public class WeithingFactSummaryEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    public virtual DateTime WeithingDate { get; set; }
    public virtual int Count { get; set; }
    public virtual string Scale { get; set; }
    public virtual string Host { get; set; }
    public virtual string Printer { get; set; }

    #endregion

    #region Constructor and destructor

    public WeithingFactSummaryEntity() : this(0)
    {
        //
    }

    public WeithingFactSummaryEntity(long id) : base(id)
    {
        WeithingDate = DateTime.MinValue;
        Count = 0;
        Scale = string.Empty;
        Host = string.Empty;
        Printer = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        base.ToString() +
        $"{nameof(WeithingDate)}: {WeithingDate}. " +
        $"{nameof(Count)}: {Count}. " +
        $"{nameof(Scale)}: {Scale}. " +
        $"{nameof(Host)}: {Host}. " +
        $"{nameof(Printer)}: {Printer}. ";

    public virtual bool Equals(WeithingFactSummaryEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(WeithingDate, item.WeithingDate) &&
               Equals(Count, item.Count) &&
               Equals(Scale, item.Scale) &&
               Equals(Host, item.Host) &&
               Equals(Printer, item.Printer);
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
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(WeithingDate, DateTime.MinValue) &&
               Equals(Count, 0) &&
               Equals(Scale, string.Empty) &&
               Equals(Host, string.Empty) &&
               Equals(Printer, string.Empty);
    }

    public new virtual object Clone()
    {
        WeithingFactSummaryEntity item = new();
        item.WeithingDate = WeithingDate;
        item.Count = Count;
        item.Scale = Scale;
        item.Host = Host;
        item.Printer = Printer;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual WeithingFactSummaryEntity CloneCast() => (WeithingFactSummaryEntity)Clone();

    #endregion
}
