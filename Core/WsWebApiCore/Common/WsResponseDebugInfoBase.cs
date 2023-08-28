namespace WsWebApiCore.Common;

[XmlRoot(WsWebConstants.Response, Namespace = "", IsNullable = false)]
public class WsResponseDebugInfoBase : SerializeDebugBase
{
    #region Public and private fields, properties, constructor

    [XmlElement(WsWebConstants.DebugInfo)]
    public WsServiceInfoModel? ServiceInfo { get; set; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsResponseDebugInfoBase()
    {
        //
    }
    
    public WsResponseDebugInfoBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ServiceInfo = (WsServiceInfoModel?)info.GetValue(nameof(ServiceInfo), typeof(WsServiceInfoModel));
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ServiceInfo), ServiceInfo);
    }

    #endregion
}