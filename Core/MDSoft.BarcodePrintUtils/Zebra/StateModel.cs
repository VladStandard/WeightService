// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.BarcodePrintUtils.Zebra;

public class StateModel
{
	#region Public and private fields and properties

	public string Message { get; private set; }
	public bool IsAlive { get; set; }
	public int OdometerUserLabel { get; private set; }
	public string Peeled { get; private set; }

	#endregion

	#region Public and private methods

	public void LoadResponse(string request, string msg)
	{
		Message = msg;
		bool noErrors = false;
		bool noWarnings = false;

		if (request == ZplUtils.ZplHostStatusReturn)
		{
			if (msg.Contains("PRINTER STATUS"))
			{
				foreach (string item in msg.Split(new string[] { "\r\n" }, StringSplitOptions.None))
				{
					if (item.Contains("ERRORS:") && item.Contains("0 00000000 00000000"))
					{
						noErrors = true;
					}
					if (item.Contains("WARNINGS:") && item.Contains("0 00000000 00000000"))
					{
						noWarnings = true;
					}
				}
			}
		}

		if (request == ZplUtils.ZplGetOdometerUserLabel())
		{
			OdometerUserLabel = int.Parse(msg);
		}

		if (request == ZplUtils.ZplPeelerState())
		{

		}

		IsAlive = noErrors && noWarnings;

	}

	#endregion
}