// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

public class StringWriterUtf8Model : StringWriter
{
	#region Public and private fields, properties, constructor

	public override Encoding Encoding => Encoding.UTF8;

	/// <summary>
	/// Constructor.
	/// </summary>
	public StringWriterUtf8Model()
	{
		//
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public StringWriterUtf8Model(IFormatProvider formatProvider) : base(formatProvider)
	{
		//
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public StringWriterUtf8Model(StringBuilder sb) : base(sb)
	{
		//
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public StringWriterUtf8Model(StringBuilder sb, IFormatProvider formatProvider) : base(sb, formatProvider)
	{
		//
	}

	#endregion
}