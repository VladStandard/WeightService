// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;

// ReSharper disable HollowTypeName

namespace ScalesMsi.Helpers
{
    /// <summary>
    /// Помощник инфо Windows.
    /// </summary>
    internal class WinInfoHelper
    {
        #region Design pattern "Lazy Singleton"

        private static WinInfoHelper _instance;
        public static WinInfoHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public WinInfoHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            // Default methods
        }

        #endregion

        #region Major
        
        /// <summary>
        /// Gets the major version number of the operating system running on this computer.
        /// </summary>
        public int MajorVersion => Environment.OSVersion.Version.Major;

        #endregion

        #region Minor
        
        /// <summary>
        /// Gets the minor version number of the operating system running on this computer.
        /// </summary>
        public int MinorVersion => Environment.OSVersion.Version.Minor;

        #endregion
    }
}
