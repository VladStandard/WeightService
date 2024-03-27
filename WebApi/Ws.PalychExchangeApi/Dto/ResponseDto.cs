using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Dto;

[XmlRoot("Response")]
public sealed class ResponseDto
{
    [XmlAttribute("SuccessesCount")]
    public int SuccessesCount
    {
        get => Successes.Count;
        set => _ = value;
    }

    [XmlAttribute("ErrorsCount")]
    public int ErrorsCount
    {
        get => Errors.Count;
        set => _ = value;
    }

    [XmlArray("Successes"), XmlArrayItem("Record")]
    public List<ResponseSuccesses> Successes { get; set; } = [];

    [XmlArray("Errors"), XmlArrayItem("Record")]
    public List<ResponseError> Errors { get; set; } = [];

    public void AddSuccess(IEnumerable<Guid> uidList)
    {
        uidList = uidList.ToHashSet();
        Successes.AddRange(uidList.Select(uid => new ResponseSuccesses(uid)));
    }

    public void AddError(Guid uid, string msg)
    {
        Errors.Add(new(uid, msg));
    }

    public void AddError(IEnumerable<Guid> uidList, string msg)
    {
        uidList = uidList.ToHashSet();
        Errors.AddRange(uidList.Select(uid => new ResponseError(uid, msg)));
    }
}