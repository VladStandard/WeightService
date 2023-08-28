namespace WsStorageCore.Models;

public class WsSqlStringWriterUtf8Model : StringWriter
{
	#region Public and private fields, properties, constructor

	public override Encoding Encoding => Encoding.UTF8;


	public WsSqlStringWriterUtf8Model()
	{
		//
	}


	public WsSqlStringWriterUtf8Model(IFormatProvider formatProvider) : base(formatProvider)
	{
		//
	}


	public WsSqlStringWriterUtf8Model(StringBuilder sb) : base(sb)
	{
		//
	}


	public WsSqlStringWriterUtf8Model(StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider)
	{
		//
	}

	#endregion
}