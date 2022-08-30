// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorDeviceControl.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    #region Public and private fields, properties, constructor

    public string RequestId { get; set; }
    private readonly ILogger<ErrorModel> _logger;
    public ErrorModel(ILogger<ErrorModel> logger)
    {
        RequestId = string.Empty;
        _logger = logger;
    }


    #endregion

    //public void OnGet()
    //{
    //    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    //}
}
