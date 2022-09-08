// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Xml;

[Serializable]
public class DeviceModel : TableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public virtual ScaleModel Scale { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceModel() : base(SqlFieldIdentityEnum.Id)
    {
        Scale = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private DeviceModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Scale)}: {Scale}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is DeviceModel item)
        {
            return
               Scale.Equals(item.Scale);
        }
        return false;
    }

    public override int GetHashCode() => Scale.GetHashCode();

    public override bool EqualsNew() => Equals(new DeviceModel());

    public override bool EqualsDefault()
    {
        if (!Scale.EqualsDefault())
            return false;
        return base.EqualsDefault();
    }

    public override object Clone()
    {
        DeviceModel item = new()
        {
            Scale = Scale.CloneCast(),
        };
        item.CloneSetup(base.CloneCast());
        return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Scale), Scale);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(DeviceModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return
            base.Equals(item) &&
            Equals(Scale, item.Scale);
    }

    public new virtual DeviceModel CloneCast() => (DeviceModel)Clone();

    #endregion
}
