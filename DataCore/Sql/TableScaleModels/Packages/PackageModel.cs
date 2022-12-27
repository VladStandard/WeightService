//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
//// ReSharper disable VirtualMemberCallInConstructor

//using DataCore.Sql.Tables;

//namespace DataCore.Sql.TableScaleModels.Packages;

///// <summary>
///// Table "PACKAGES".
///// </summary>
//[Serializable]
//[DebuggerDisplay("Type = {nameof(PackageModel)}")]
//public class PackageModel : SqlTableBase
//{
//    #region Public and private fields, properties, constructor

//    [XmlElement] public virtual decimal Weight { get; set; }

//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public PackageModel() : base(SqlFieldIdentityEnum.Uid)
//    {
//        Weight = default;
//    }

//    /// <summary>
//    /// Constructor for serialization.
//    /// </summary>
//    /// <param name="info"></param>
//    /// <param name="context"></param>
//    protected PackageModel(SerializationInfo info, StreamingContext context) : base(info, context)
//    {
//        Weight = info.GetDecimal(nameof(Weight));
//    }

//    #endregion

//    #region Public and private methods - override

//    public override string ToString() =>
//        $"{nameof(IsMarked)}: {IsMarked}. " +
//        $"{nameof(Name)}: {Name}. " +
//        $"{nameof(Weight)}: {Weight}. ";

//    public override bool Equals(object obj)
//    {
//        if (ReferenceEquals(null, obj)) return false;
//        if (ReferenceEquals(this, obj)) return true;
//        if (obj.GetType() != GetType()) return false;
//        return Equals((PackageModel)obj);
//    }

//    public override int GetHashCode() => base.GetHashCode();

//    public override bool EqualsNew() => Equals(new());

//    public override bool EqualsDefault() =>
//        base.EqualsDefault() &&
//        Equals(Weight, default(decimal));

//    public override object Clone()
//    {
//        PackageModel item = new();
//        item.Weight = Weight;
//        item.CloneSetup(base.CloneCast());
//        return item;
//    }

//    public override void GetObjectData(SerializationInfo info, StreamingContext context)
//    {
//        base.GetObjectData(info, context);
//        info.AddValue(nameof(Weight), Weight);
//    }

//    public override void FillProperties()
//    {
//        base.FillProperties();
//        Weight = 0.560M;
//    }

//    #endregion

//    #region Public and private methods - virtual

//    public virtual bool Equals(PackageModel item) =>
//        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
//        Equals(Weight, item.Weight);

//    public new virtual PackageModel CloneCast() => (PackageModel)Clone();

//    #endregion
//}
