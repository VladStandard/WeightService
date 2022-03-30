// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorInputFile;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using System.IO;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Service
{
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file);
        Task UploadAsync(string name, Stream stream);
        Task UploadAsync(DataAccessEntity dataAccess, TemplateResourceEntity? entity, Stream stream);
    }
}
