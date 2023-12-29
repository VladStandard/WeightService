using System;
using Ws.Shared.Utils;

namespace Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

[DebuggerDisplay("{ToString()}")]
public class SqlTemplateResourceEntity : SqlEntityBase
{
    public virtual string Type { get; set; }
    public virtual FieldBinaryModel Data { get; set; }
    public virtual bool IsMarked { get; set; }
    public virtual byte[] DataValue { get => Data.Value ?? Array.Empty<byte>(); set => Data.Value = value; }
    
    public SqlTemplateResourceEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Type = string.Empty;
        Data = new();
    }
    
    public SqlTemplateResourceEntity(SqlTemplateResourceEntity item) : base(item)
    {
        Type = item.Type;
        Data = new(item.Data);
        DataValue = DataUtils.ByteClone(item.DataValue);
    }
    
    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Type)}: {Type}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlTemplateResourceEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(SqlTemplateResourceEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Type, item.Type) &&
        Data.Equals(item.Data);
}