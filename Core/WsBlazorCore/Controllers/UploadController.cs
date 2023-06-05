// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBlazorCore.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UploadController //: Controller
{
    #region Public and private fields, properties, constructor

    private readonly IHostingEnvironment _environment; // IWebHostEnvironment

    public UploadController(IHostingEnvironment environment) // IWebHostEnvironment
    {
        Console.WriteLine("UploadController");
        _environment = environment;
    }

    #endregion

    #region Public and private methods

    [HttpPost("[action]")]
    public async Task<IActionResult> MultipleAsync(IFormFile[] files, string currentDirectory)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        try
        {
            if (files.Any())
            {
                foreach (IFormFile file in files)
                {
                    // reconstruct the path to ensure everything goes to uploads directory
                    string requestedPath = currentDirectory.ToLower()
                        .Replace(_environment.WebRootPath.ToLower(), "");
                    requestedPath = requestedPath.Contains("\\uploads\\") ? requestedPath.Replace("\\uploads\\", "") : "";
                    string path = Path.Combine(_environment.WebRootPath, "uploads", requestedPath, file.FileName);
                    await using FileStream stream = new(path, FileMode.Create);
                    await file.CopyToAsync(stream).ConfigureAwait(true);
                }
            }
            return new StatusCodeResult(200);
        }
        catch (Exception)
        {
            //return new StatusCodeResult(500) {  Exception = ex };
            return new StatusCodeResult(500);
        }
    }

    #endregion
}