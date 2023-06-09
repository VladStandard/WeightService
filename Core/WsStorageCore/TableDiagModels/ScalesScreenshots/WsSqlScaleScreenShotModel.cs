// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using WsStorageCore.Common;

namespace WsStorageCore.TableDiagModels.ScalesScreenshots;

/// <summary>
/// Table "diag.SCALES_SCREENSHOTS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlScaleScreenShotModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlScaleModel Scale { get; set; }
    [XmlElement] public virtual byte[] ScreenShot { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlScaleScreenShotModel() : base(WsSqlFieldIdentity.Uid)
    {
        Scale = new();
        ScreenShot = Array.Empty<byte>();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlScaleScreenShotModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Scale = (WsSqlScaleModel)info.GetValue(nameof(Scale), typeof(WsSqlScaleModel));
        ScreenShot = (byte[])info.GetValue(nameof(ScreenShot), typeof(byte));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Scale)}: {Scale.Description}. " +
        $"{nameof(ScreenShot)}: {ScreenShot.Length}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlScaleScreenShotModel)obj);
    }

    public override int GetHashCode() => (Scale, ScreenShot.Length).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(ScreenShot, Array.Empty<byte>()) &&
        Scale.EqualsDefault();

    public override object Clone()
    {
        WsSqlScaleScreenShotModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Scale = Scale.CloneCast();
        item.ScreenShot = ScreenShot;
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
        Scale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlScaleScreenShotModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Scale, item.Scale) &&
        Equals(ScreenShot, item.ScreenShot) &&
        Scale.Equals(item.Scale);

    public new virtual WsSqlScaleScreenShotModel CloneCast() => (WsSqlScaleScreenShotModel)Clone();

    #endregion
}