﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using static DataCore.Models.IDisposableBase;

namespace DataCore.Models
{
    public class DisposableBase : IDisposable
    {
        #region Public and private fields and properties

        public bool IsOpen { get; private set; }
        private readonly ushort _maxCount = ushort.MaxValue;
        private ushort _reopenCount;
        public ushort ReopenCount
        {
            get => _reopenCount;
            set
            {
                if (value >= _maxCount)
                    value = 0;
                _reopenCount = value;
            }
        }
        private ushort _requestCount;
        public ushort RequestCount
        {
            get => _requestCount;
            set
            {
                if (value >= _maxCount)
                    value = 0;
                _requestCount = value;
            }
        }
        private ushort _responseCount;
        public ushort ResponseCount
        {
            get => _responseCount;
            set
            {
                if (value >= _maxCount)
                    value = 0;
                _responseCount = value;
            }
        }
        public bool IsDispose { get; private set; }
        public CloseCallback? CloseCaller { get; private set; }
        public ReleaseManagedCallback? ReleaseManagedResourcesCaller { get; private set; }
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
            IsOpen = false;
            IsDispose = false;
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
            if (IsDispose)
            {
                throw new ObjectDisposedException("Object has been disposed!");
            }
        }

        public void Open()
        {
            CheckIsDisposed();
            lock (_locker)
            {
                if (IsOpen) return;
                IsOpen = true;
            }
        }

        public void Close()
        {
            // For Close - don't use check for disposing.
            //CheckIsDisposed(filePath, lineNumber, memberName);
            lock (_locker)
            {
                if (!IsOpen) return;
                IsOpen = false;
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
                if (!IsDispose)
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
                    IsDispose = true;
                }

                // Disable the garbage collector from calling the destructor.
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}
