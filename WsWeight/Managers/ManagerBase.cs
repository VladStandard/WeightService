// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.Managers;

public class ManagerBase : DisposableBase, IDisposableBase
{
	#region Public and private fields and properties - Manager

	public ManagerItemModel ReopenItem { get; set; }
    public ManagerItemModel RequestItem { get; set; }
    public ManagerItemModel ResponseItem { get; set; }
    private readonly object _locker = new();

	#endregion

	#region Public and private methods

    protected ManagerBase() : base()
    {
        ReopenItem = new();
        RequestItem = new();
        ResponseItem = new();
		Init(Close, ReleaseManaged, ReleaseUnmanaged);
	}

    protected void Init(TaskType taskType, Action? init, ManagerConfigModel waitConfig)
	{
		lock (_locker)
		{
            ReopenItem.TaskType = taskType;
            RequestItem.TaskType = taskType;
            ResponseItem.TaskType = taskType;
            ReopenItem.Config = waitConfig;
            RequestItem.Config = waitConfig;
            ResponseItem.Config = waitConfig;
			init?.Invoke();
		}
	}

    protected void Open(Action? reopenCallback, Action? requestCallback, Action? responseCallback)
	{
		Close();
		Open();
        ReopenItem.Open(reopenCallback);
		RequestItem.Open(requestCallback);
        ResponseItem.Open(responseCallback);
	}

	public new void Close()
	{
		base.Close();
		CheckIsDisposed();
        ReopenItem.Close();
        RequestItem.Close();
        ResponseItem.Close();
	}

	public void ReleaseManaged()
	{
		Close();
        ReopenItem.ReleaseManaged();
	}

	public void ReleaseUnmanaged()
	{
		//
	}

    //public void Suspend()
    //{
    //    Reopen.IsSuspend = true;
    //    Request.IsSuspend = true;
    //    Response.IsSuspend = true;
    //}

    //public void UnSuspend()
    //{
    //    Reopen.IsSuspend = false;
    //    Request.IsSuspend = false;
    //    Response.IsSuspend = false;
    //}

    #endregion
}