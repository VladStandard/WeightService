using System.Collections.Generic;

namespace DeviceControlCore.DAL
{
    public class FieldListEntity
    {
        #region Public and private fields and properties

        public bool Use { get; set; }
        public Dictionary<string, object> Fields { get; set; }

        #endregion

        #region Constructor and destructor

        public FieldListEntity(Dictionary<string, object> fields)
        {
            Use = true;
            Fields = fields;
        }

        public FieldListEntity()
        {
            Use = false;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strFields = string.Empty;
            var i = 0;
            foreach (var field in Fields)
            {
                strFields += $"Field[{i}]: {field.Key} = {field.Value}. ";
                i++;
            }
            strFields = strFields.TrimEnd('\r', ' ', '\n');
            return $"{nameof(Use)}: {Use}. {strFields}";
        }

        #endregion
    }
}
