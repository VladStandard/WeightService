namespace Ws.WebApiScales.Dto.Response;

[XmlRoot("Response")]
public class ResponseDto
{
    [XmlAttribute("SuccessesCount")]
    public int SuccessesCount { get; set; }
    
    [XmlAttribute("ErrorsCount")]
    public int ErrorsCount { get; set; }
    
    [XmlArray("Successes"), XmlArrayItem("Record")]
    public List<ResponseSuccesses> Successes { get; set; } = new();

    [XmlArray("Errors"), XmlArrayItem("Record")]
    public List<ResponseError> Errors { get; set; } = new();
    
    public void AddSuccess(Guid uid, string msg = "")
    {
        Successes.Add(new() { Guid = uid , Message = msg});
        SuccessesCount = Successes.Count;
    }
    
    public void AddError(Guid uid, string msg)
    {
        Errors.Add(new() { Guid = uid, Message = msg});
        ErrorsCount = Errors.Count;
    }
}