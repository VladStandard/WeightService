// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using static DataCore.Models.IDisposableBase;

namespace DataCore.Models
{
    public class DisposableBase : IDisposable
    {
        #region Public and private fields and properties

        /// <summary>
        /// Opened state.
        /// </summary>
        public bool IsOpened { get; private set; }
        /// <summary>
        /// Closed state.
        /// </summary>
        public bool IsClosed { get; private set; }
        /// <summary>
        /// Disposed state.
        /// </summary>
        public bool IsDisposed { get; private set; }
        /// <summary>
        /// Callback method to close.
        /// </summary>
        public CloseCallback? CloseCaller { get; private set; }
        /// <summary>
        /// Callback method to release managed resources.
        /// </summary>
        public ReleaseManagedCallback? ReleaseManagedResourcesCaller { get; private set; }
        /// <summary>
        /// Callback method to release unmanaged resources.
        /// </summary>
        public ReleaseUnmanagedCallback? ReleaseUnmanagedResourcesCaller { get; private set; }
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public DisposableBase()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            CloseCaller = null;
            ReleaseManagedResourcesCaller = null;
            ReleaseUnmanagedResourcesCaller = null;
            IsOpened = false;
            IsClosed = false;
            IsDisposed = false;
        }

        public void Init(CloseCallback close, ReleaseManagedCallback releaseManaged, ReleaseUnmanagedCallback releaseUnmanaged)
        {
            CheckIsDisposed();
            lock (_locker)
            {
                SetDefault();
                CloseCaller = close;
                ReleaseManagedResourcesCaller = releaseManaged;
                ReleaseUnmanagedResourcesCaller = releaseUnmanaged;
            }
        }

        ~DisposableBase()
        {
            Dispose(false);
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

        public void Open()
        {
            CheckIsDisposed();
            lock (_locker)
            {
                if (IsOpened) return;
                IsOpened = true;
                IsClosed = false;
            }
        }

        public void Close()
        {
            // For Close - don't use check for sisposing.
            //CheckIsDisposed(filePath, lineNumber, memberName);
            lock (_locker)
            {
                if (IsClosed) return;
                IsOpened = false;
                IsClosed = true;
                CloseCaller?.Invoke();
            }
        }

        public void Dispose()
        {
            Dispose(false);
        }

        public virtual void Dispose(bool disposing)
        {
            Close();
            CloseCaller = null;

            lock (_locker)
            {
                if (!IsDisposed)
                {
                    // Releasing managed resources.
                    if (disposing)
                    {
                        ReleaseManagedResourcesCaller?.Invoke();
                        ReleaseManagedResourcesCaller = null;
                    }

                    // Releasing unmanaged resources.
                    ReleaseUnmanagedResourcesCaller?.Invoke();
                    ReleaseUnmanagedResourcesCaller = null;

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
