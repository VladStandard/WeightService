// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using static DataCore.Models.IDisposableBase;

namespace DataCore.Models;

public class DisposableBase : IDisposable
{
    #region Public and private fields, properties, constructor

    protected bool IsOpen { get; private set; }
    private const ushort MaxCount = ushort.MaxValue;
    private ushort _reopenCount;
    public ushort ReopenCount
    {
        get => _reopenCount;
        protected set
        {
            if (value >= MaxCount) value = 0;
            _reopenCount = value;
        }
    }
    private ushort _requestCount;
    public ushort RequestCount
    {
        get => _requestCount;
        protected set
        {
            if (value >= MaxCount) value = 0;
            _requestCount = value;
        }
    }
    private ushort _responseCount;
    public ushort ResponseCount
    {
        get => _responseCount;
        protected set
        {
            if (value >= MaxCount) value = 0;
            _responseCount = value;
        }
    }
    private bool IsDispose { get; set; }
    private Action? CloseCaller { get; set; }
    private Action? ReleaseManaged { get; set; }
    private Action? ReleaseUnmanaged { get; set; }
    private readonly object _locker = new();

    protected DisposableBase()
    {
        SetDefault();
    }

    private void SetDefault()
    {
        CloseCaller = null;
        ReleaseManaged = null;
        ReleaseUnmanaged = null;
        IsOpen = false;
        IsDispose = false;
    }

    protected void Init(Action? close, Action? releaseManaged, Action? releaseUnmanaged)
    {
        CheckIsDisposed();
        lock (_locker)
        {
            SetDefault();
            CloseCaller = close;
            ReleaseManaged = releaseManaged;
            ReleaseUnmanaged = releaseUnmanaged;
        }
    }

    ~DisposableBase()
    {
        Dispose(false);
    }

    #endregion

    #region Public and private methods

    protected void CheckIsDisposed()
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
                    ReleaseManaged?.Invoke();
                    ReleaseManaged = null;
                }

                // Releasing unmanaged resources.
                ReleaseUnmanaged?.Invoke();
                ReleaseUnmanaged = null;

                // Resource release flag.
                IsDispose = true;
            }

            // Disable the garbage collector from calling the destructor.
            GC.SuppressFinalize(this);
        }
    }

    #endregion
}