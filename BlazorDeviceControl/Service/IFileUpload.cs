﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;
using System.Threading.Tasks;
using BlazorInputFile;
using DataCore.DAL;
using DataCore.DAL.TableModels;

namespace BlazorDeviceControl.Service
{
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file);
        Task UploadAsync(string name, Stream stream);
        Task UploadAsync(DataAccessEntity dataAccess, TemplateResourceEntity entity, Stream stream);
    }
}