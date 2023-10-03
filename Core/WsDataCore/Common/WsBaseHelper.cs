namespace WsDataCore.Common;

public class WsBaseHelper : IDisposable
{
    #region Public and private fields, properties, constructor

    protected bool IsExecute { get; private set; }
    private Action? CloseAction { get; set; }

    protected WsBaseHelper()
    {
        IsExecute = false;
        CloseAction = null;
    }

    #endregion

    #region Public and private methods

    protected void Init()
    {
        IsExecute = false;
        CloseAction = () => { };
    }

    public virtual void Execute()
    {
        if (IsExecute) return;
        IsExecute = true;
    }

    public virtual void Dispose()
    {
        if (!IsExecute) return;
        IsExecute = false;
        CloseAction?.Invoke();
    }

    #endregion
}