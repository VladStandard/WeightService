// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Protocols;

public class ZplExchangeEntity
{
    #region Public and private fields, properties, constructor

    public byte[] Cmd { get; set; }
    public int Length => Cmd.Count();

    /// <summary>
	/// Constructor.
	/// </summary>
	public ZplExchangeEntity()
    {
        Cmd = Array.Empty<byte>();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cmd"></param>
	public ZplExchangeEntity(string cmd)
    {
        Cmd = Encoding.ASCII.GetBytes(cmd);
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cmd"></param>
	public ZplExchangeEntity(byte[] cmd)
    {
        Cmd = cmd;
    }

    #endregion
}
