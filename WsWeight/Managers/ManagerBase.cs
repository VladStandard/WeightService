// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.Managers;

public class ManagerBase : DisposableBase, IDisposableBase
{
	#region Public and private fields and properties - Manager

	public ManagerItemModel Reopen { get; set; }
    public ManagerItemModel Request { get; set; }
    public ManagerItemModel Response { get; set; }
    private readonly object _locker = new();

	#endregion

	#region Public and private methods

    protected ManagerBase() : base()
    {
        Reopen = new();
        Request = new();
        Response = new();
		Init(Close, ReleaseManaged, ReleaseUnmanaged);
	}

    protected void Init(TaskType taskType, Action? init, ManagerConfigModel waitConfig)
	{
		lock (_locker)
		{
            Reopen.TaskType = taskType;
            Request.TaskType = taskType;
            Response.TaskType = taskType;
            Reopen.Config = waitConfig;
            Request.Config = waitConfig;
            Response.Config = waitConfig;
			init?.Invoke();
		}
	}

    protected void Open(Action? reopenCallback, Action? requestCallback, Action? responseCallback)
	{
		Close();
		Open();
        Reopen.Open(reopenCallback);
		Request.Open(requestCallback);
        Response.Open(responseCallback);
	}

	public new void Close()
	{
		base.Close();
		CheckIsDisposed();
        Reopen.Close();
        Request.Close();
        Response.Close();
	}

	public void ReleaseManaged()
	{
		Close();
        Reopen.ReleaseManaged();
	}

	public void ReleaseUnmanaged()
	{
		//
	}

    public void Suspend()
    {
        Reopen.IsSuspend = true;
        Request.IsSuspend = true;
        Response.IsSuspend = true;
    }

    public void UnSuspend()
    {
        Reopen.IsSuspend = false;
        Request.IsSuspend = false;
        Response.IsSuspend = false;
    }

    #endregion
}