﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;

namespace BlazorDeviceControl.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UploadController : Controller
{
    private readonly IWebHostEnvironment _environment;

    public UploadController(IWebHostEnvironment environment)
    {
        Console.WriteLine("UploadController");
        _environment = environment;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> MultipleAsync(IFormFile[] files, string currentDirectory)
    {
        Console.WriteLine("MultipleAsync");
        try
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (IFormFile file in HttpContext.Request.Form.Files)
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
            return StatusCode(200);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
