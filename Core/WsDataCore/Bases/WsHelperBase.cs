// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Bases;

public class WsHelperBase
{
    #region Public and private fields, properties, constructor

    protected bool IsExecute { get; private set; }
    private Action CloseAction { get; set; }

    protected WsHelperBase()
    {
        IsExecute = false;
        CloseAction = () => { };
    }

    #endregion

    #region Public and private methods

    protected virtual void Init()
    {
        IsExecute = false;
        CloseAction = () => { };
    }

    public virtual void Execute()
    {
        if (IsExecute) return;
        IsExecute = true;
    }

    public virtual void Close()
    {
        if (!IsExecute) return;
        IsExecute = false;
        CloseAction.Invoke();
    }

    #endregion
}