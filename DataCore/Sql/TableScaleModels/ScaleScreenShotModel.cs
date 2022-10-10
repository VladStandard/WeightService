﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "SCALES_SCREENSHOTS".
/// </summary>
[Serializable]
public class ScaleScreenShotModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual ScaleModel Scale { get; set; }
    [XmlElement] public virtual byte[] ScreenShot { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ScaleScreenShotModel() : base(SqlFieldIdentityEnum.Uid)
	{
	    Scale = new();
        ScreenShot = Array.Empty<byte>();
    }

	/// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ScaleScreenShotModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
	    Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
	    ScreenShot = (byte[])info.GetValue(nameof(ScreenShot), typeof(byte));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
	    $"{nameof(Scale)}: {Scale.Description}. " + 
	    $"{nameof(ScreenShot)}: {ScreenShot.Length}. ";

    public override bool Equals(object obj)
	{
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ScaleScreenShotModel)obj);
    }

    public override int GetHashCode() => (Scale, ScreenShot.Length).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
    {
        if (!Scale.EqualsDefault())
            return false;
        return
            base.EqualsDefault() &&
            Equals(ScreenShot, Array.Empty<byte>());
    }

	public override object Clone()
    {
        ScaleScreenShotModel item = new();
        item.Scale = Scale.CloneCast();
        item.ScreenShot = ScreenShot;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Scale), Scale);
        info.AddValue(nameof(ScreenShot), ScreenShot);
    }

    public override void FillProperties()
    {
	    base.FillProperties();
		Scale = new();
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(ScaleScreenShotModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!Scale.Equals(item.Scale))
			return false;
		return
			base.Equals(item) &&
			Equals(Scale, item.Scale) &&
			Equals(ScreenShot, item.ScreenShot);
	}

	public new virtual ScaleScreenShotModel CloneCast() => (ScaleScreenShotModel)Clone();

	#endregion
}
