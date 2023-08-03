// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Protocols;

public class MdZplExchangeModel
{
    #region Public and private fields, properties, constructor

    public byte[] Cmd { get; }
    public int Length => Cmd.Length;

    /// <summary>
	/// Constructor.
	/// </summary>
	public MdZplExchangeModel()
    {
        Cmd = Array.Empty<byte>();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cmd"></param>
	public MdZplExchangeModel(string cmd)
    {
        Cmd = Encoding.ASCII.GetBytes(cmd);
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cmd"></param>
	public MdZplExchangeModel(byte[] cmd)
    {
        Cmd = cmd;
    }

    #endregion
}
