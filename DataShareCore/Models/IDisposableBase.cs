// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataShareCore.Models
{
    public interface IDisposableBase
    {
        public delegate void InitCallback();
        //public delegate void OpenCallback();
        public delegate void CloseCallback();
        public delegate void ReopenCallback();
        public delegate void RequestCallback();
        public delegate void ResponseCallback();
        public delegate void ReleaseManagedCallback();
        public delegate void ReleaseUnmanagedCallback();

        public void CloseMethod();

        public void Dispose();

        public void ReleaseManaged();

        public void ReleaseUnmanaged();
    }
}
