// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Models
{
    public interface IDisposableBase
    {
        public delegate void InitCallback();
        public delegate void CloseCallback();
        public delegate void ReopenCallback();
        public delegate void RequestCallback();
        public delegate void ResponseCallback();
        public delegate void ReleaseManagedCallback();
        public delegate void ReleaseUnmanagedCallback();

        public void Close();

        public void Dispose(bool disposing);

        public void ReleaseManaged();

        public void ReleaseUnmanaged();
    }
}
