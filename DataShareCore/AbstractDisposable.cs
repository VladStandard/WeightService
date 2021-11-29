// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataShareCore
{
    public class AbstractDisposable : IDisposable
    {
        #region Public and private fields and properties

        public bool Disposed { get; private set; }
        public delegate void ReleaseManagedCallback();
        public delegate void ReleaseUnmanagedCallback();
        public ReleaseManagedCallback? ReleaseManagedResources { get; set; }
        public ReleaseUnmanagedCallback? ReleaseUnmanagedResources { get; set; }

        #endregion

        #region Constructor and destructor

        public AbstractDisposable()
        {
            ReleaseManagedResources = null;
            ReleaseUnmanagedResources = null;
        }

        public void Init(ReleaseManagedCallback releaseManagedResources, ReleaseUnmanagedCallback releaseUnmanagedResources)
        {
            lock (this)
            {
                ReleaseManagedResources = releaseManagedResources;
                ReleaseUnmanagedResources = releaseUnmanagedResources;
            }
        }

        ~AbstractDisposable()
        {
            Dispose();
        }

        #endregion

        #region Public and private methods

        public void CheckIfDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException("Object has been disposed!");
            }
        }

        public virtual void Dispose()
        {
            lock (this)
            {
                if (!Disposed)
                {
                    // Releasing managed resources.
                    ReleaseManagedResources?.Invoke();

                    // Releasing unmanaged resources.
                    ReleaseUnmanagedResources?.Invoke();

                    // Resource release flag.
                    Disposed = true;
                }

                // Disable the garbage collector from calling the destructor.
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}
