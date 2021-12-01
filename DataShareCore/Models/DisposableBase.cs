// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using static DataShareCore.Models.IDisposableBase;

namespace DataShareCore.Models
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
        /// Callback method to open.
        /// </summary>
        //public OpenCallback? OpenCaller { get; private set; }
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

        #endregion

        #region Constructor and destructor

        public DisposableBase()
        {
            IsOpened = false;
            IsClosed = false;
            IsDisposed = false;
            //OpenCaller = null;
            CloseCaller = null;
            ReleaseManagedResourcesCaller = null;
            ReleaseUnmanagedResourcesCaller = null;
        }

        public void Init(CloseCallback close, ReleaseManagedCallback releaseManaged, ReleaseUnmanagedCallback releaseUnmanaged)
        {
            CheckIsDisposed();
            lock (this)
            {
                IsOpened = false;
                IsClosed = false;
                IsDisposed = false;
                //OpenCaller = open;
                CloseCaller = close;
                ReleaseManagedResourcesCaller = releaseManaged;
                ReleaseUnmanagedResourcesCaller = releaseUnmanaged;
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

        public void Open()
        {
            CheckIsDisposed();
            lock (this)
            {
                if (IsOpened) return;
                IsOpened = true;
                IsClosed = false;
                //OpenCaller?.Invoke();
            }
        }

        public void Close()
        {
            CheckIsDisposed();
            lock (this)
            {
                if (IsClosed) return;
                IsOpened = false;
                IsClosed = true;
                CloseCaller?.Invoke();
            }
        }

        public void Dispose()
        {
            Close();
            
            lock (this)
            {
                if (!IsDisposed)
                {
                    // Releasing managed resources.
                    ReleaseManagedResourcesCaller?.Invoke();

                    // Releasing unmanaged resources.
                    ReleaseUnmanagedResourcesCaller?.Invoke();

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
