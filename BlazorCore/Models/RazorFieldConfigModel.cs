// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using Radzen;

namespace BlazorCore.Models;

public class RazorFieldConfigModel
{
	#region Public and private fields, properties, constructor

	public string FieldLinkUrl { get; set; }
	public SqlTableBase FieldSqlTable { get; set; }
	public string FieldName { get; set; }
	public TextAlign FieldTextAlign { get; set; }
	public string FieldTitle { get; set; }
	public string FieldType { get; set; }

	public RazorFieldConfigModel()
	{
		FieldLinkUrl = string.Empty;
		FieldSqlTable = new SqlTableEmptyModel();
		FieldName = string.Empty;
		FieldTextAlign = TextAlign.Center;
		FieldTitle = string.Empty;
		FieldType = "string";
	}

	public RazorFieldConfigModel(string name, TextAlign textAlign, string title, string type) : this()
	{
		FieldName = name;
		FieldTextAlign = textAlign;
		FieldTitle = title;
		FieldType = type;
	}

	public RazorFieldConfigModel(string linkUrl, SqlTableBase sqlTable, string name, TextAlign textAlign, string title, string type) : 
		this(name, textAlign, title, type)
	{
		FieldLinkUrl = linkUrl;
		FieldSqlTable = sqlTable;
	}

	public RazorFieldConfigModel(string name, TextAlign textAlign, string title) : 
		this("", new SqlTableEmptyModel(), name, textAlign, title, "string") { }

	#endregion

	#region Public and private methods

	public override string ToString() =>
		(!string.IsNullOrEmpty(FieldLinkUrl) ? $"{FieldLinkUrl}. " : string.Empty) +
		(FieldSqlTable is not SqlTableEmptyModel ? $"{FieldSqlTable}. " : FieldSqlTable) +
		(!string.IsNullOrEmpty(FieldName) ? $"{FieldName}. " : string.Empty) +
		(!string.IsNullOrEmpty(FieldTitle) ? $"{FieldTitle}. " : string.Empty) +
		$"{FieldTextAlign}. " +
		(!string.IsNullOrEmpty(FieldType) ? $"{FieldType}. " : string.Empty);

	#endregion
}
