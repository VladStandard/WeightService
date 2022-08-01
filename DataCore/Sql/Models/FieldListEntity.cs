// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace DataCore.Sql.Models;

/// <summary>
/// DB field list entity.
/// </summary>
public class FieldListEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Enabled.
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Filter fields.
    /// </summary>
    public List<FieldEntity> Fields { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fields"></param>
    public FieldListEntity(List<FieldEntity> fields)
    {
        IsEnabled = true;
        Fields = fields;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public FieldListEntity()
    {
        IsEnabled = false;
        Fields = new();
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// To string override.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string? strFields = string.Empty;
        int i = 0;
        foreach (FieldEntity? field in Fields)
        {
            strFields += $"Field[{i}]: {field.Name} | {field.Comparer} | {field.Value}. ";
            i++;
        }
        strFields = strFields.TrimEnd('\r', ' ', '\n');
        return $"{nameof(IsEnabled)}: {IsEnabled}. {strFields}";
    }

    //private List<FieldEntity> GetFields(List<FieldEntity> fields)
    //{
    //    Dictionary<string, object?> result = new();
    //    if (fields.Count <= 0) return result;
    //    foreach (KeyValuePair<DbField, object?> field in fields)
    //    {
    //        result.Add(field.Key.ToString(), field.Value);
    //    }
    //    return result;
    //}

    #endregion
}
