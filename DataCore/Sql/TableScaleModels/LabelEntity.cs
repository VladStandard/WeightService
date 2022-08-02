// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Utils;
using System.Text;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Labels".
/// </summary>
public class LabelEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Id;
    public virtual WeithingFactEntity WeithingFact { get; set; }
    public virtual byte[] Label { get; set; }
    public virtual string LabelString
    {
        get => Label == null || Label.Length == 0 ? string.Empty : Encoding.Default.GetString(Label);
        set => Label = Encoding.Default.GetBytes(value);
    }
    public virtual string LabelInfo
    {
        get => DataUtils.GetBytesLength(Label);
        set => _ = value;
    }
    public virtual string Zpl { get; set; }
    public virtual string ZplInfo
    {
        get => DataUtils.GetStringLength(Zpl);
        set => _ = value;
    }

    public LabelEntity() : this(0)
    {
        //
    }

    public LabelEntity(long id) : base(id)
    {
        WeithingFact = new();
        Label = new byte[0];
        Zpl = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string strWeithingFact = WeithingFact != null ? WeithingFact.IdentityId.ToString() : "null";
        return base.ToString() +
               $"{nameof(WeithingFact)}: {strWeithingFact}. " +
               $"{nameof(Label)}: {LabelString}. " +
               $"{nameof(Zpl)}: {ZplInfo}. ";
    }

    public virtual bool Equals(LabelEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (WeithingFact != null && item.WeithingFact != null && !WeithingFact.Equals(item.WeithingFact))
            return false;
        return base.Equals(item) &&
               DataUtils.ByteEquals(Label, item.Label) &&
               Equals(Zpl, item.Zpl);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LabelEntity)obj);
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
        if (WeithingFact != null && !WeithingFact.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               DataUtils.ByteEquals(Label, new byte[0]) &&
               Equals(Zpl, string.Empty);
    }

    public new virtual object Clone()
    {
        LabelEntity item = new();
        item.WeithingFact = WeithingFact.CloneCast();
        item.Label = DataUtils.ByteClone(Label);
        item.Zpl = Zpl;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual LabelEntity CloneCast() => (LabelEntity)Clone();

    #endregion
}
