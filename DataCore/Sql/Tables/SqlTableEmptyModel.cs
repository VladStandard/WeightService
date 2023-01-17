// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Interfaces;

namespace DataCore.Sql.Tables;

/// <summary>
/// DB empty table model.
/// </summary>
public class SqlTableEmptyModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlTableEmptyModel()
    {
        //
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() => base.ToString();

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((SqlTableEmptyModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() => base.EqualsDefault();

    public override object Clone()
    {
        SqlTableEmptyModel item = new();
        item.CloneSetup(base.CloneCast());
		return item;
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(SqlTableEmptyModel item) => 
		ReferenceEquals(this, item) || base.Equals(item);

	public new virtual SqlTableEmptyModel CloneCast() => (SqlTableEmptyModel)Clone();

    #endregion
}
