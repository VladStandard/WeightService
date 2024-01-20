using System.Xml.Linq;
using Ws.StorageCore.Entities.Diag.LogWebs;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Utils;

namespace Ws.WebApiScales.Common;

public abstract class BaseController(IHttpContextAccessor httpContextAccessor, ResponseDto responseDto) : ControllerBase
{
    protected ContentResult HandleXmlRequest<TDto>(XElement xml, Action<TDto> processMethod) where TDto : class
    {
        try
        {
            DateTime requestTime = DateTime.Now;
            string currentUrl = httpContextAccessor.HttpContext?.Request.Path ?? string.Empty; 
            
            TDto requestDto = XmlUtil.DeserializeFromXml<TDto>(xml);

            processMethod(requestDto);

            string response = XmlUtil.SerializeToXml(responseDto);
            
            new SqlLogWebRepository().Save(requestTime, xml.ToString(),response, currentUrl, 
            responseDto.SuccessesCount, responseDto.ErrorsCount);
            
            return new() { Content = response, ContentType = "application/xml", StatusCode = 200 };
        }
        catch (Exception ex)
        {
            return new() { Content = $"Error deserializing XML: {ex.Message}", StatusCode = 200 };
        }
    }
}