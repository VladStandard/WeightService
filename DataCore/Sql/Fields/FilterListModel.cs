//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCore.Sql.Fields;

///// <summary>
///// DB filter list entity.
///// </summary>
//public class FilterListModel
//{
//    #region Public and private fields, properties, constructor

//    /// <summary>
//    /// Enabled.
//    /// </summary>
//    public bool IsEnabled { get; set; }
//    /// <summary>
//    /// Filter fields.
//    /// </summary>
//    public List<FieldComparerModel> Fields { get; }

//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    /// <param name="fields"></param>
//    public FilterListModel(List<FieldComparerModel> fields)
//    {
//        IsEnabled = true;
//        Fields = fields;
//    }

//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public FilterListModel()
//    {
//        IsEnabled = false;
//        Fields = new();
//    }

//    #endregion

//    #region Public and private methods

//    /// <summary>
//    /// To string override.
//    /// </summary>
//    /// <returns></returns>
//    public override string ToString()
//    {
//        string? strFields = string.Empty;
//        int i = 0;
//        foreach (FieldComparerModel? field in Fields)
//        {
//            strFields += $"Field[{i}]: {field.Name} | {field.Comparer} | {field.Value}. ";
//            i++;
//        }
//        strFields = strFields.TrimEnd('\r', ' ', '\n');
//        return $"{nameof(IsEnabled)}: {IsEnabled}. {strFields}";
//    }

//    #endregion
//}
