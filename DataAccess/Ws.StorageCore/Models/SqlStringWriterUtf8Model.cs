namespace Ws.StorageCore.Models;

public class SqlStringWriterUtf8Model : StringWriter
{
	public override Encoding Encoding => Encoding.UTF8;


	public SqlStringWriterUtf8Model()
	{
		//
	}


	public SqlStringWriterUtf8Model(IFormatProvider formatProvider) : base(formatProvider)
	{
		//
	}


	public SqlStringWriterUtf8Model(StringBuilder sb) : base(sb)
	{
		//
	}


	public SqlStringWriterUtf8Model(StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider)
	{
		//
	}
}