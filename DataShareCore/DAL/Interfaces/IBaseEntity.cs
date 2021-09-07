// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace DataShareCore.DAL.Interfaces
{
    public interface IBaseEntity
    {
        byte[] CloneBytes(byte[] bytes);
        bool EqualsEmpty();
        Task<byte[]> GetBytes(Stream stream, bool useBase64);
        string GetBytesLength(byte[] bytes);
        object? GetDefaultValue(Type t);
        Image GetImage(byte[] bytes, bool useBase64);
    }
}
