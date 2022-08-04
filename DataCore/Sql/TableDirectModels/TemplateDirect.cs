// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDirectModels;

/// <summary>
/// Table "Templates".
/// </summary>
[Serializable]
public class TemplateDirect : BaseSerializeEntity, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public string Title { get; set; } = string.Empty;
	[XmlElement] public string XslContent { get; set; } = string.Empty;
    [XmlElement(IsNullable = true)] public long? TemplateId { get; set; }
    [XmlIgnore] public Dictionary<string, string> Fonts { get; } = new();
    [XmlIgnore] public Dictionary<string, string> Logos { get; } = new();
    [XmlIgnore] public string CategoryId { get; set; } = string.Empty;

	/// <summary>
	/// Constructor.
	/// </summary>
	public TemplateDirect()
    {
        CategoryId = string.Empty;
        Title = string.Empty;
        XslContent = string.Empty;
        Fonts = new();
        Logos = new();
    }

    public TemplateDirect(long? templateId) : this()
    {
        SqlUtils.GetTemplateFromDb(this, templateId);
    }

    public TemplateDirect(string title) : this()
    {
        SqlUtils.GetTemplateFromDb(this, title);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateDirect(SerializationInfo info, StreamingContext context)
    {
	    Title = info.GetString(nameof(Title));
		XslContent = info.GetString(nameof(XslContent));
		TemplateId = info.GetInt64(nameof(TemplateId));
    }

	#endregion

	#region Public and private methods

	public override bool Equals(object obj)
    {
        if (obj is TemplateDirect item)
        {
            return TemplateId.Equals(item.TemplateId);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return TemplateId.GetHashCode();
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"{CategoryId}/{Title}");
        return sb.ToString();
    }

    public IDictionary<string, object> ObjectToDictionary<T>(T item) where T : class
    {
        Type myObjectType = item.GetType();
        IDictionary<string, object> dict = new Dictionary<string, object>();
        object[] indexer = new object[0];
        PropertyInfo[] properties = myObjectType.GetProperties();
        foreach (PropertyInfo info in properties)
        {
            object value = info.GetValue(item, indexer);
            dict.Add(info.Name, value);
        }
        return dict;
    }

    public T ObjectFromDictionary<T>(IDictionary<string, object> dict)
        where T : class
    {
        Type type = typeof(T);
        T result = (T)Activator.CreateInstance(type);
        foreach (KeyValuePair<string, object> item in dict)
        {
            type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
        }
        return result;
    }

    public void Load()
    {
        SqlUtils.GetTemplateFromDb(this, TemplateId);
    }

    public bool IsDefault()
    {
	    if (!string.IsNullOrEmpty(Title))
		    return false;
	    if (!string.IsNullOrEmpty(XslContent))
		    return false;
	    if (TemplateId != null)
		    return false;
	    if (!string.IsNullOrEmpty(CategoryId))
		    return false;
	    if (!Fonts.Equals(new()))
		    return false;
	    if (!Logos.Equals(new()))
		    return false;
		return true;
    }

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    base.GetObjectData(info, context);
	    info.AddValue(nameof(Title), Title);
	    info.AddValue(nameof(XslContent), XslContent);
	    info.AddValue(nameof(TemplateId), TemplateId);
    }

	#endregion
}
