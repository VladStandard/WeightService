// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.DataModels;

[Serializable]
public class LabelQuickEntity : BaseEntity, ISerializable, IBaseEntity
{
    #region Public and private fields, properties, constructor

    public virtual long ScaleId { get; set; }
    public virtual string ScaleDescription { get; set; }
    public virtual int PluId { get; set; }
    public virtual int PluNumber { get; set; }
    public virtual string PluGoodName { get; set; }
    public virtual DateTime WeithingDate { get; set; }
    public virtual decimal NetWeight { get; set; }
    public virtual decimal TareWeight { get; set; }
    public virtual DateTime ProductDate { get; set; }
    public virtual int? RegNum { get; set; }
    public virtual int? Kneading { get; set; }
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
    public virtual string Zpl { get; set; } = string.Empty;
    public virtual string ZplInfo
    {
        get => DataUtils.GetStringLength(Zpl);
        set => _ = value;
    }
    public virtual long TemplateId { get; set; }
    public virtual string TemplateName { get; set; }

	#endregion

	#region Constructor and destructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public LabelQuickEntity() : base(0, false)
    {
	    Init();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public LabelQuickEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
    {
	    Init();
    }

    public new virtual void Init()
    {
	    base.Init();
        ScaleId = 0;
        ScaleDescription = string.Empty;
        PluId = 0;
        PluNumber = 0;
        PluGoodName = string.Empty;
        WeithingDate = DateTime.MinValue;
        NetWeight = 0;
        TareWeight = 0;
        ProductDate = DateTime.MinValue;
        RegNum = null;
        Kneading = null;
        Label = new byte[0];
        Zpl = string.Empty;
        TemplateId = 0;
        TemplateName = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        base.ToString() +
        $"{nameof(ScaleId)}: {ScaleId}. " +
        $"{nameof(ScaleDescription)}: {ScaleDescription}. " +
        $"{nameof(PluId)}: {PluId}. " +
        $"{nameof(PluNumber)}: {PluNumber}. " +
        $"{nameof(PluGoodName)}: {PluGoodName}. " +
        $"{nameof(WeithingDate)}: {WeithingDate}. " +
        $"{nameof(NetWeight)}: {NetWeight}. " +
        $"{nameof(TareWeight)}: {TareWeight}. " +
        $"{nameof(ProductDate)}: {ProductDate}. " +
        $"{nameof(RegNum)}: {RegNum}. " +
        $"{nameof(Kneading)}: {Kneading}. " +
        $"{nameof(ZplInfo)}: {ZplInfo}. " + 
        $"{nameof(TemplateId)}: {TemplateId}. " + 
        $"{nameof(TemplateName)}: {TemplateName}. ";

    public virtual bool Equals(LabelQuickEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(ScaleId, item.ScaleId) &&
               Equals(ScaleDescription, item.ScaleDescription) &&
               Equals(PluId, item.PluId) &&
               Equals(PluNumber, item.PluNumber) &&
               Equals(PluGoodName, item.PluGoodName) &&
               Equals(WeithingDate, item.WeithingDate) &&
               Equals(NetWeight, item.NetWeight) &&
               Equals(TareWeight, item.TareWeight) &&
               Equals(ProductDate, item.ProductDate) &&
               Equals(RegNum, item.RegNum) &&
               Equals(Kneading, item.Kneading) &&
               Equals(Label, item.Label) &&
               Equals(Zpl, item.Zpl) &&
               Equals(TemplateId, item.TemplateId) &&
               Equals(TemplateName, item.TemplateName);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LabelQuickEntity)obj);
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
               Equals(ScaleId, 0) &&
               Equals(ScaleDescription, string.Empty) &&
               Equals(PluId, 0) &&
               Equals(PluNumber, 0) &&
               Equals(PluGoodName, string.Empty) &&
               Equals(WeithingDate, DateTime.MinValue) &&
               Equals(NetWeight, 0) &&
               Equals(TareWeight, 0) &&
               Equals(ProductDate, DateTime.MinValue) &&
               Equals(RegNum, null) &&
               Equals(Label, new byte[0]) &&
               Equals(Zpl, string.Empty) &&
               Equals(TemplateId, 0) &&
               Equals(TemplateName, string.Empty);
    }

    public new virtual object Clone()
    {
        LabelQuickEntity item = new();
        item.ScaleId = ScaleId;
        item.ScaleDescription = ScaleDescription;
        item.PluId = PluId;
        item.PluNumber = PluNumber;
        item.PluGoodName = PluGoodName;
        item.WeithingDate = WeithingDate;
        item.NetWeight = NetWeight;
        item.TareWeight = TareWeight;
        item.ProductDate = ProductDate;
        item.RegNum = RegNum;
        item.Label = DataUtils.ByteClone(Label);
        item.Zpl = Zpl;
        item.TemplateId = TemplateId;
        item.TemplateName = TemplateName;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual LabelQuickEntity CloneCast() => (LabelQuickEntity)Clone();

    #endregion
}
