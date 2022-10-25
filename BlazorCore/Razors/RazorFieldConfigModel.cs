// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen;

namespace BlazorCore.Razors;

public class RazorFieldConfigModel
{
	#region Public and private fields, properties, constructor

	public string LinkUrl { get; set; }
	public SqlTableBase SqlTable { get; set; }
	public string Name { get; set; }
	public TextAlign TextAlign { get; set; }
	public string Title { get; set; }
	public string Type { get; set; }

	public RazorFieldConfigModel()
	{
		LinkUrl = string.Empty;
		SqlTable = new SqlTableEmptyModel();
		Name = string.Empty;
		TextAlign = TextAlign.Center;
		Title = string.Empty;
		Type = "string";
	}

	public RazorFieldConfigModel(string name, TextAlign textAlign, string title, string type) : this()
	{
		Name = name;
		TextAlign = textAlign;
		Title = title;
		Type = type;
	}

	public RazorFieldConfigModel(string linkUrl, SqlTableBase sqlTable, string name, TextAlign textAlign, string title, string type) :
		this(name, textAlign, title, type)
	{
		LinkUrl = linkUrl;
		SqlTable = sqlTable;
	}

	public RazorFieldConfigModel(SqlTableBase sqlTable, string name, TextAlign textAlign, string title, string type) :
		this(name, textAlign, title, type)
	{
		SqlTable = sqlTable;
	}

	public RazorFieldConfigModel(string name, TextAlign textAlign, string title) :
		this(string.Empty, new SqlTableEmptyModel(), name, textAlign, title, "string") { }

	#endregion

	#region Public and private methods

	public override string ToString() =>
		(!string.IsNullOrEmpty(LinkUrl) ? $"{LinkUrl}. " : string.Empty) +
		(SqlTable is not SqlTableEmptyModel ? $"{SqlTable}. " : SqlTable) +
		(!string.IsNullOrEmpty(Name) ? $"{Name}. " : string.Empty) +
		(!string.IsNullOrEmpty(Title) ? $"{Title}. " : string.Empty) +
		$"{TextAlign}. " +
		(!string.IsNullOrEmpty(Type) ? $"{Type}. " : string.Empty);

	#endregion
}
