// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Service
{
    public class FileDownload : IFileDownload
    {
        private readonly IWebHostEnvironment _environment;
        public FileDownload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task DownloadAsync(IBlazorDownloadFileService blazorDownloadFileService, TemplateResourceEntity entity)
        {
            if (entity == null || entity.ImageData == null || entity.ImageData.Length == 0)
                return;
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            char[] chars = Encoding.UTF8.GetChars(entity.ImageData);
            byte[] bytes = entity.CloneBytes(Convert.FromBase64CharArray(chars, 0, chars.Length));
            await blazorDownloadFileService.DownloadFile(entity.Name.Contains('.') ? entity.Name : $"{entity.Name}.{entity.Type}", 
                bytes, "application/octet-stream").ConfigureAwait(false);
        }
    }
}