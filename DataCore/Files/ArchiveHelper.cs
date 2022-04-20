// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;
using System.IO.Compression;
using System.Threading;

namespace DataCore.Files
{
    public class ArchiveHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static ArchiveHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static ArchiveHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region public methods

        public void Compress(string sourceFile, string compressedFile)
        {
            using FileStream sourceStream = new(sourceFile, FileMode.OpenOrCreate);
            using FileStream targetStream = File.Create(compressedFile);
            using GZipStream compressionStream = new(targetStream, CompressionMode.Compress);
            sourceStream.CopyTo(compressionStream);
        }

        public void Decompress(string compressedFile, string targetFile)
        {
            using FileStream sourceStream = new(compressedFile, FileMode.OpenOrCreate);
            using FileStream targetStream = File.Create(targetFile);
            using GZipStream decompressionStream = new(sourceStream, CompressionMode.Decompress);
            decompressionStream.CopyTo(targetStream);
        }

        #endregion
    }
}
