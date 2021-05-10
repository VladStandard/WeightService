// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesCore.Win.Registry.Entities
{
    /// <summary>
    /// Поля записи реестра.
    /// </summary>
    public class Record
    {
        public string FieldName { get; }
        public string FieldOwner { get; }
        public string FieldDeny { get; }
        public string FieldAllow { get; }

        public Record(string fieldName, string fieldOwner, string fieldDeny, string fieldAllow)
        {
            FieldName = fieldName;
            FieldOwner = fieldOwner;
            FieldDeny = fieldDeny;
            FieldAllow = fieldAllow;
        }
    }
}
