using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;
using DeviceControl.Core.DAL;
using DeviceControl.Core.DAL.TableModels;

namespace BlazorDeviceControl.Service
{
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file);
        Task UploadAsync(string name, Stream stream);
        Task UploadAsync(DataAccessEntity dataAccess, TemplateResourcesEntity entity, Stream stream);
    }
}