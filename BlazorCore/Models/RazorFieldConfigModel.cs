// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen;

namespace BlazorCore.Models;

public class RazorFieldConfigModel
{
	#region Public and private fields, properties, constructor

	public string FieldLink { get; set; }
	public string FieldName { get; set; }
	public TextAlign FieldTextAlign { get; set; }
	public string FieldTitle { get; set; }
	public string FieldType { get; set; }

	public RazorFieldConfigModel()
	{
		FieldLink = string.Empty;
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

	public RazorFieldConfigModel(string link, string name, TextAlign textAlign, string title, string type) : 
		this(name, textAlign, title, type)
	{
		FieldLink = link;
	}

	public RazorFieldConfigModel(string name, TextAlign textAlign, string title) : 
		this("", name, textAlign, title, "string") { }

	#endregion
}
