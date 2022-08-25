//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCore.Sql.TableScaleModels;

///// <summary>
///// Table "WeithingFact".
///// </summary>
//[Serializable]
//public class WeithingFactEntity : BaseEntity, ISerializable, IBaseEntity
//{
//	#region Public and private fields, properties, constructor

//	/// <summary>
//	/// Identity name.
//	/// </summary>
//	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
//	[XmlElement] public virtual PluObsoleteEntity Plu { get; set; }
//	[XmlElement] public virtual ScaleEntity Scale { get; set; }
//	[XmlElement] public virtual ProductSeriesEntity? Serie { get; set; }
//	[XmlElement] public virtual string Sscc { get; set; }
//	[XmlElement] public virtual DateTime WeithingDate { get; set; }
//	[XmlElement] public virtual decimal NetWeight { get; set; }
//	[XmlElement] public virtual decimal TareWeight { get; set; }
//	[XmlElement] public virtual DateTime ProductDate { get; set; }
//	[XmlElement(IsNullable = true)] public virtual int? RegNum { get; set; }
//	[XmlElement(IsNullable = true)] public virtual int? Kneading { get; set; }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//    public WeithingFactEntity() : base(0, false)
//	{
//		Init();
//	}

//    public WeithingFactEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
//	{
//		Init();
//	}

//    #endregion

//    #region Public and private methods

//    public new virtual void Init()
//    {
//		base.Init();
//        Plu = new();
//        Scale = new();
//        Serie = null;
//        Sscc = string.Empty;
//        WeithingDate = DateTime.MinValue;
//        NetWeight = 0;
//        TareWeight = 0;
//        ProductDate = DateTime.MinValue;
//        RegNum = null;
//        Kneading = null;
//    }

//    public override string ToString()
//    {
//        string strPlu = Plu != null ? Plu.IdentityId.ToString() : "null";
//        string strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
//        string strSeries = Serie != null ? Serie.IdentityId.ToString() : "null";
//        return
//			$"{nameof(IdentityId)}: {IdentityId}. " + 
//			$"{nameof(IsMarked)}: {IsMarked}. " +
//			$"{nameof(Plu)}: {strPlu}. " +
//			$"{nameof(Scale)}: {strScale}. " +
//			$"{nameof(Serie)}: {strSeries}. " +
//			$"{nameof(Sscc)}: {Sscc}. " +
//			$"{nameof(WeithingDate)}: {WeithingDate}. " +
//			$"{nameof(NetWeight)}: {NetWeight}. " +
//			$"{nameof(TareWeight)}: {TareWeight}." +
//			$"{nameof(ProductDate)}: {ProductDate}." +
//			$"{nameof(RegNum)}: {RegNum}." +
//			$"{nameof(Kneading)}: {Kneading}.";
//    }

//    public virtual bool Equals(WeithingFactEntity item)
//    {
//        if (item is null) return false;
//        if (ReferenceEquals(this, item)) return true;
//        if (!Plu.Equals(item.Plu))
//            return false;
//        if (!Scale.Equals(item.Scale))
//            return false;
//        if (Serie != null && item.Serie != null && !Serie.Equals(item.Serie))
//            return false;
//        return base.Equals(item) &&
//               Equals(Sscc, item.Sscc) &&
//               Equals(WeithingDate, item.WeithingDate) &&
//               Equals(NetWeight, item.NetWeight) &&
//               Equals(TareWeight, item.TareWeight) &&
//               Equals(ProductDate, item.ProductDate) &&
//               Equals(RegNum, item.RegNum) &&
//               Equals(Kneading, item.Kneading);
//    }

//    public override bool Equals(object obj)
//    {
//        if (obj is null) return false;
//        if (ReferenceEquals(this, obj)) return true;
//        if (obj.GetType() != GetType()) return false;
//        return Equals((WeithingFactEntity)obj);
//    }

//	public override int GetHashCode() => IdentityId.GetHashCode();

//	public virtual bool EqualsNew()
//    {
//        return Equals(new());
//    }

//    public new virtual bool EqualsDefault()
//    {
//        if (!Plu.EqualsDefault())
//            return false;
//        if (!Scale.EqualsDefault())
//            return false;
//        if (Serie != null && !Serie.EqualsDefault())
//            return false;
//        return base.EqualsDefault() &&
//               Equals(Sscc, string.Empty) &&
//               Equals(WeithingDate, DateTime.MinValue) &&
//               Equals(NetWeight, (decimal)0) &&
//               Equals(TareWeight, (decimal)0) &&
//               Equals(ProductDate, DateTime.MinValue) &&
//               Equals(RegNum, null) &&
//               Equals(Kneading, null);
//    }

//    public new virtual object Clone()
//    {
//        WeithingFactEntity item = new();
//        item.Plu = Plu.CloneCast();
//        item.Scale = Scale.CloneCast();
//        item.Serie = Serie?.CloneCast();
//        item.Sscc = Sscc;
//        item.WeithingDate = WeithingDate;
//        item.NetWeight = NetWeight;
//        item.TareWeight = TareWeight;
//        item.ProductDate = ProductDate;
//        item.RegNum = RegNum;
//        item.Kneading = Kneading;
//        item.Setup(((BaseEntity)this).CloneCast());
//        return item;
//    }

//    public new virtual WeithingFactEntity CloneCast() => (WeithingFactEntity)Clone();
    
//    #endregion
//}
