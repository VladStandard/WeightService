//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCore.Sql.TableScaleModels;

///// <summary>
///// Table "Labels".
///// </summary>
//[Serializable]
//public class LabelEntity : BaseEntity, ISerializable, IBaseEntity
//{
//	#region Public and private fields, properties, constructor

//    [XmlElement] public virtual WeithingFactEntity WeithingFact { get; set; }
//    [XmlElement(IsNullable = true)] public virtual byte[]? Label { get; set; }
//	[XmlElement] public virtual string LabelString
//    {
//        get => Label == null || Label.Length == 0 ? string.Empty : Encoding.Default.GetString(Label);
//        set => Label = Encoding.Default.GetBytes(value);
//    }
//	[XmlElement] public virtual string LabelInfo
//    {
//        get => DataUtils.GetBytesLength(Label);
//        set => _ = value;
//    }
//	[XmlElement] public virtual string Zpl { get; set; }
//	[XmlElement] public virtual string ZplInfo
//    {
//        get => DataUtils.GetStringLength(Zpl);
//        set => _ = value;
//    }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//    public LabelEntity() : base(0, false)
//	{
//		Init();
//	}

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//	/// <param name="identityId"></param>
//	/// <param name="isSetupDates"></param>
//	public LabelEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
//    {
//		Init();
//	}

//    #endregion

//    #region Public and private methods

//    public new virtual void Init()
//    {
//	    base.Init();
//		WeithingFact = new();
//		Label = Array.Empty<byte>();
//		Zpl = string.Empty;
//	}

//    public override string ToString()
//    {
//        return
//			$"{nameof(IdentityId)}: {IdentityId}. " + 
//			$"{nameof(IsMarked)}: {IsMarked}. " +
//            $"{nameof(WeithingFact)}: {WeithingFact.IdentityId}. " +
//			$"{nameof(Zpl)}: {ZplInfo}. ";
//    }

//    public virtual bool Equals(LabelEntity item)
//    {
//        if (item is null) return false;
//        if (ReferenceEquals(this, item)) return true;
//        if (!WeithingFact.Equals(item.WeithingFact))
//            return false;
//        return base.Equals(item) &&
//               DataUtils.ByteEquals(Label, item.Label) &&
//               Equals(Zpl, item.Zpl);
//    }

//    public override bool Equals(object obj)
//    {
//if (ReferenceEquals(null, obj)) return false;
//if (ReferenceEquals(this, obj)) return true;
//        if (obj.GetType() != GetType()) return false;
//        return Equals((LabelEntity)obj);
//    }

//	public override int GetHashCode() => IdentityId.GetHashCode();

//	public virtual bool EqualsNew()
//    {
//        return Equals(new());
//    }

//    public new virtual bool EqualsDefault()
//    {
//        if (!WeithingFact.EqualsDefault())
//            return false;
//        return base.EqualsDefault() &&
//               DataUtils.ByteEquals(Label, new byte[0]) &&
//               Equals(Zpl, string.Empty);
//    }

//    public new virtual object Clone()
//    {
//        LabelEntity item = new();
//        item.WeithingFact = WeithingFact.CloneCast();
//        item.Label = Label == null ? null : DataUtils.ByteClone(Label);
//        item.Zpl = Zpl;
//        item.Setup(((BaseEntity)this).CloneCast());
//        return item;
//    }

//    public new virtual LabelEntity CloneCast() => (LabelEntity)Clone();

//    #endregion
//}
