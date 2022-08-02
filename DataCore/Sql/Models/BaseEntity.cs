// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
// ReSharper disable MissingXmlDoc

namespace DataCore.Sql.Models;

/// <summary>
/// Enum column name.
/// </summary>
public enum ColumnName
{
    Default,
    Id,
    Uid,
}

[Serializable]
public class BaseEntity : BaseSerializeEntity, ICloneable, ISerializable
{
    #region Public and private fields, properties, constructor

    //[XmlIgnore] private virtual ColumnName IdentityName { get; set; }
    public virtual Guid IdentityUid { get; set; }
    public virtual long IdentityId { get; set; }
    public virtual DateTime CreateDt { get; set; }
    public virtual DateTime ChangeDt { get; set; }
    public virtual bool IsMarked { get; set; }
    [XmlIgnore] public virtual string IdentityUidStr
    {
        get => IdentityUid.ToString() is string str ? str : Guid.Empty.ToString(); 
        set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty;
    }

    private BaseEntity()
    {
        //IdentityName = ColumnName.Default;
        IdentityId = 0;
        IdentityUid = Guid.Empty;
        CreateDt = DateTime.MinValue;
        ChangeDt = DateTime.MinValue;
        IsMarked = false;
    }

    public BaseEntity(long identityId) : this()
    {
        //IdentityName = ColumnName.Id;
        IdentityId = identityId;
    }

    public BaseEntity(Guid identityUid) : this()
    {
        //IdentityName = ColumnName.Uid;
        IdentityUid = identityUid;
    }

    protected BaseEntity(SerializationInfo info, StreamingContext context)
    {
        //IdentityName = GetColumnName(info.GetString(nameof(IdentityName)));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
        IdentityUid = Guid.Parse(info.GetString(nameof(IdentityUid)));
        IdentityId = info.GetInt64(nameof(IdentityId));
        IdentityUidStr = info.GetString(nameof(IdentityUidStr));
    }

    #endregion

    #region Public and private methods

    private ColumnName GetColumnName(string columnName)
    {
        return columnName switch
        {
            "Id" => ColumnName.Id,
            "Uid" => ColumnName.Uid,
            _ => ColumnName.Default,
        };
    }

    public override string ToString()
    {
        //string strIdentity = IdentityName switch
        //{
        //    ColumnName.Id => $"{nameof(IdentityId)}: {IdentityId}. ",
        //    ColumnName.Uid => $"{nameof(IdentityUid)}: {IdentityUid}. ",
        //    _ => $"{IdentityName}. ",
        //};
        //string strCreateDt = CreateDt != null && CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
        string strCreateDt = CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
        //string strChangeDt = ChangeDt != null && ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
        string strIsMarked = IsMarked ? $"{nameof(IsMarked)}: {IsMarked}. " : string.Empty;
        //return strIdentity + strCreateDt + strChangeDt + strIsMarked;
        return strCreateDt + strChangeDt + strIsMarked;
    }

    //public override int GetHashCode() => IdentityName switch
    //{
    //    ColumnName.Id => IdentityId.GetHashCode(),
    //    ColumnName.Uid => IdentityUid.GetHashCode(),
    //    _ => default,
    //};

    //public virtual bool EqualsEmpty()
    //{
    //    bool isIdentityEmpty = IdentityName switch
    //    {
    //        ColumnName.Id => Equals(IdentityId, 0),
    //        ColumnName.Uid => Equals(IdentityUid, Guid.Empty),
    //        _ => Equals(IdentityName, ColumnName.Default),
    //    };
    //    return isIdentityEmpty;
    //}

    public virtual bool Equals(BaseEntity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return
            //IdentityName.Equals(item.IdentityName) &&
            IdentityId.Equals(item.IdentityId) &&
            IdentityUid.Equals(item.IdentityUid) &&
            Equals(CreateDt, item.CreateDt) &&
            Equals(ChangeDt, item.ChangeDt) &&
            Equals(IsMarked, item.IsMarked);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseEntity)obj);
    }

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsMarked), IsMarked);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(IdentityId), IdentityId);
        info.AddValue(nameof(IdentityUid), IdentityUid);
        //info.AddValue(nameof(IdentityName), IdentityName);
        info.AddValue(nameof(IdentityUidStr), IdentityUidStr);
    }

    public virtual bool EqualsDefault()
    {
        return
            Equals(IdentityId, (long)0) &&
            Equals(IdentityUid, Guid.Empty) &&
            Equals(CreateDt, DateTime.MinValue) &&
            Equals(ChangeDt, DateTime.MinValue) &&
            Equals(IsMarked, false);
    }

    public virtual object Clone() => new BaseEntity()
    {
        //IdentityName = IdentityName,
        IdentityId = IdentityId,
        IdentityUid = IdentityUid,
        CreateDt = CreateDt,
        ChangeDt = ChangeDt,
        IsMarked = IsMarked,
    };

    public virtual BaseEntity CloneCast() => (BaseEntity)Clone();

    public virtual void Setup(BaseEntity baseItem)
    {
        //IdentityName = baseItem.IdentityName;
        IdentityId = baseItem.IdentityId;
        IdentityUid = baseItem.IdentityUid;
        CreateDt = baseItem.CreateDt;
        ChangeDt = baseItem.ChangeDt;
        IsMarked = baseItem.IsMarked;
    }

    #endregion
}
