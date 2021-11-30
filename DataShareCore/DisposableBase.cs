// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using static DataShareCore.IDisposableBase;

namespace DataShareCore
{
    public class DisposableBase : IDisposable
    {
        #region Public and private fields and properties

        public bool IsClosed { get; private set; }
        public bool IsDisposed { get; private set; }
        public ReleaseManagedCallback? ReleaseManagedResources { get; private set; }
        public ReleaseUnmanagedCallback? ReleaseUnmanagedResources { get; private set; }
        public CloseCallback? CloseDelegate { get; private set; }

        #endregion

        #region Constructor and destructor

        public DisposableBase()
        {
            IsClosed = false;
            IsDisposed = false;
            ReleaseManagedResources = null;
            ReleaseUnmanagedResources = null;
            CloseDelegate = null;
        }

        public void Init(CloseCallback close, ReleaseManagedCallback releaseManagedResources, ReleaseUnmanagedCallback releaseUnmanagedResources)
        {
            lock (this)
            {
                IsClosed = false;
                IsDisposed = false;
                CloseDelegate = close;
                ReleaseManagedResources = releaseManagedResources;
                ReleaseUnmanagedResources = releaseUnmanagedResources;
            }
        }

        ~DisposableBase()
        {
            Dispose();
        }

        #endregion

        #region Public and private methods

        public void CheckIsDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("Object has been disposed!");
            }
        }

        public void Close()
        {
            if (IsClosed)
                return;
            
            CloseDelegate?.Invoke();
            IsClosed = true;
        }

        public void Dispose()
        {
            lock (this)
            {
                Close();

                if (!IsDisposed)
                {
                    // Releasing managed resources.
                    ReleaseManagedResources?.Invoke();

                    // Releasing unmanaged resources.
                    ReleaseUnmanagedResources?.Invoke();

                    // Resource release flag.
                    IsDisposed = true;
                }

                // Disable the garbage collector from calling the destructor.
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}
