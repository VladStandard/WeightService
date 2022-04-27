// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using static DataCore.ShareEnums;

namespace DataCore.DAL.Models
{
    public class FieldListEntity
    {
        #region Public and private fields and properties

        public bool Use { get; set; }
        public Dictionary<string, object?> Fields { get; set; }

        #endregion

        #region Constructor and destructor

        public FieldListEntity(Dictionary<string, object?> fields)
        {
            Use = true;
            Fields = fields;
        }

        public FieldListEntity(Dictionary<DbField, object?> fields)
        {
            Use = true;
            Fields = GetFields(fields);
        }

        public FieldListEntity()
        {
            Use = false;
            Fields = new Dictionary<string, object?>();
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strFields = string.Empty;
            int i = 0;
            foreach (KeyValuePair<string, object?> field in Fields)
            {
                strFields += $"Field[{i}]: {field.Key} = {field.Value}. ";
                i++;
            }
            strFields = strFields.TrimEnd('\r', ' ', '\n');
            return $"{nameof(Use)}: {Use}. {strFields}";
        }

        private Dictionary<string, object?> GetFields(Dictionary<DbField, object?> fields)
        {
            Dictionary<string, object?> result = new();
            if (fields != null && fields.Count > 0)
            {
                foreach (KeyValuePair<DbField, object?> field in fields)
                {
                    result.Add(field.Key.ToString(), field.Value);
                }
            }
            return result;
        }
        
        #endregion
    }
}
