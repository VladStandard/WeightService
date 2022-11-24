//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
//// ReSharper disable MissingXmlDoc

//namespace DataCore.Sql.Tables;

///// <summary>
///// DB table model.
///// </summary>
//[Serializable]
//public class SqlTableAtributesBase : SerializeBase, ICloneable, ISqlDbBase, ISerializable
//{
//	#region Public and private fields, properties, constructor

//	[XmlAttribute] public virtual SqlFieldIdentityModel Identity { get; }
//	[XmlAttribute] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
//	[XmlAttribute] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
//	[XmlAttribute] public virtual bool IdentityIsNew => Identity.IsNew();
//	[XmlAttribute] public virtual bool IdentityIsNotNew => Identity.IsNotNew();
//	[XmlAttribute] public virtual DateTime CreateDt { get; set; }
//    [XmlAttribute] public virtual DateTime ChangeDt { get; set; }
//    [XmlAttribute] public virtual bool IsMarked { get; set; }
//    [XmlAttribute] public virtual string Name { get; set; }
//    [XmlAttribute] public virtual string Description { get; set; }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//	public SqlTableAtributesBase()
//    {
//	    Identity = new(SqlFieldIdentityEnum.Empty);
//	    ChangeDt = CreateDt = DateTime.MinValue;
//	    IsMarked = false;
//	    Name = string.Empty;
//		Description = string.Empty;
//    }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//	public SqlTableAtributesBase(SqlFieldIdentityEnum identityName) : this()
//    {
//	    Identity = new(identityName);
//    }

//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public SqlTableAtributesBase(SqlFieldIdentityModel identity) : this()
//	{
//		Identity = (SqlFieldIdentityModel)identity.Clone();
//    }

//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    protected SqlTableAtributesBase(SerializationInfo info, StreamingContext context)
//    {
//		Identity = (SqlFieldIdentityModel)info.GetValue(nameof(Identity), typeof(SqlFieldIdentityModel));
//        CreateDt = info.GetDateTime(nameof(CreateDt));
//        ChangeDt = info.GetDateTime(nameof(ChangeDt));
//        IsMarked = info.GetBoolean(nameof(IsMarked));
//		Name = info.GetString(nameof(Name));
//        Description = info.GetString(nameof(Description));
//    }

//	#endregion

//	#region Public and private methods - override

//	public override string ToString()
//    {
//        string strCreateDt = CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
//        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
//        string strIsMarked = $"{nameof(IsMarked)}: {IsMarked}. ";
//        string strName = string.IsNullOrEmpty(Name) ? string.Empty : $"{nameof(Name)}: {Name}. ";
//        string strDescription = string.IsNullOrEmpty(Description) ? string.Empty : $"{nameof(Description)}: {Description}. ";
//		return strCreateDt + strChangeDt + strIsMarked + strName + strDescription;
//    }

//    public virtual bool Equals(SqlTableAtributesBase item) =>
//	    ReferenceEquals(this, item) || Identity.Equals(item.Identity) && //-V3130
//	    Equals(CreateDt, item.CreateDt) &&
//	    Equals(ChangeDt, item.ChangeDt) &&
//	    Equals(IsMarked, item.IsMarked) &&
//	    Equals(Name, item.Name) &&
//	    Equals(Description, item.Description);

//    public override bool Equals(object obj)
//    {
//		if (ReferenceEquals(null, obj)) return false;
//		if (ReferenceEquals(this, obj)) return true;
//		if (obj.GetType() != GetType()) return false;
//        return Equals((SqlTableAtributesBase)obj);
//    }
    
//    public override int GetHashCode() => Identity.GetHashCode();

//	/// <summary>
//	/// Get object data for serialization info.
//	/// </summary>
//	/// <param name="info"></param>
//	/// <param name="context"></param>
//	public override void GetObjectData(SerializationInfo info, StreamingContext context)
//    {
//	    base.GetObjectData(info, context);
//		info.AddValue(nameof(Identity), Identity);
//        info.AddValue(nameof(ChangeDt), ChangeDt);
//        info.AddValue(nameof(CreateDt), CreateDt);
//        info.AddValue(nameof(IsMarked), IsMarked);
//        info.AddValue(nameof(Name), Name);
//        info.AddValue(nameof(Description), Description);
//    }

//	#endregion

//	#region Public and private methods - virtual

//	public virtual bool EqualsNew() => Equals(new());

//	public virtual bool EqualsDefault() =>
//		Identity.EqualsDefault() &&
//		Equals(CreateDt, DateTime.MinValue) &&
//		Equals(ChangeDt, DateTime.MinValue) &&
//		Equals(IsMarked, false) &&
//		Equals(Name, string.Empty) &&
//		Equals(Description, string.Empty);

//	public virtual object Clone() => new SqlTableAtributesBase(Identity)
//	{
//		CreateDt = CreateDt,
//		ChangeDt = ChangeDt,
//		IsMarked = IsMarked,
//		Name = Name,
//		Description = Description,
//	};

//	public virtual SqlTableAtributesBase CloneCast() => (SqlTableAtributesBase)Clone();

//	public virtual void CloneSetup(SqlTableAtributesBase item)
//	{
//		CreateDt = item.CreateDt;
//		ChangeDt = item.ChangeDt;
//		IsMarked = item.IsMarked;
//		Name = item.Name;
//		Description = item.Description;
//	}

//	public virtual void SetDtNow()
//	{
//		ChangeDt = CreateDt = DateTime.Now;
//	}

//	public virtual void ClearNullProperties()
//	{
//		//throw new NotImplementedException();
//	}

//	public virtual void FillProperties()
//	{
//		SetDtNow();
//		Name = LocaleCore.Sql.SqlItemFieldName;
//		Description = LocaleCore.Sql.SqlItemFieldDescription;
//	}

//	#endregion
//}
