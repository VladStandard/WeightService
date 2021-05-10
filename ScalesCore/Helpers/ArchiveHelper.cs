// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.IO.Compression;

namespace ScalesCore.Helpers
{
    /// <summary>
    /// Помощник архивов.
    /// </summary>
    public sealed class ArchiveHelper
    {
        #region Design pattern "Singleton"

        private static readonly Lazy<ArchiveHelper> _instance = new Lazy<ArchiveHelper>(() => new ArchiveHelper());
        public static ArchiveHelper Instance => _instance.Value;
        private ArchiveHelper() { }

        #endregion

        #region public methods

        /// <summary>
        /// Упаковка архива.
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="compressedFile"></param>
        public void Compress(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (var compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        Console.WriteLine(@"Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Распаковка архива.
        /// </summary>
        /// <param name="compressedFile"></param>
        /// <param name="targetFile"></param>
        public void Decompress(string compressedFile, string targetFile)
        {
            // Поток для чтения из сжатого файла.
            using (var sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // Поток для записи восстановленного файла.
                using (var targetStream = File.Create(targetFile))
                {
                    // поток разархивации
                    using (var decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine(@"Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }

        #endregion
    }
}
