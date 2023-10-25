namespace WsDataCore.Models;

public sealed class ActionSettingsModel
{
	#region Public and private fields and properties

	public bool IsDevice { get; set; }
	public bool IsNesting { get; set; }
	public bool IsKneading { get; set; }
	public bool IsNewPallet { get; set; }
	public bool IsPlu { get; set; }
	public bool IsPrint { get; set; }
	public bool IsScalesTerminal { get; set; }

	#endregion

	#region Constructor and destructor

	public ActionSettingsModel()
	{
		IsDevice = false;
		IsNesting = false;
		IsKneading = false;
		IsNewPallet = false;
		IsPlu = false;
		IsPrint = false;
		IsScalesTerminal = false;
	}

	#endregion
}