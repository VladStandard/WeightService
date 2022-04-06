//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.DAL.Models;
//using Microsoft.Data.SqlClient;
//using System;
//using System.Xml.Serialization;

//namespace DataCore.DAL.TableDirectModels
//{
//    [Serializable]
//    public class ContregentDirect : BaseSerializeEntity<ContregentDirect>
//    {
//        #region Public and private fields and properties

//        public Guid Uid { get; set; } = Guid.Empty;
//        public string Name { get; set; } = string.Empty;
//        public DateTime CreateDate { get; set; }
//        public DateTime ChangeDt { get; set; }
//        public string RRefID { get; set; } = string.Empty;
//        public bool IsMarked { get; set; } = false;

//        #endregion

//        #region Constructor and destructor

//        public ContregentDirect()
//        {
//            Load();
//        }

//        public ContregentDirect(Guid uid)
//        {
//            Uid = uid;
//            IsMarked = false;
//            Load();
//        }

//        #endregion

//        #region Public and private methods

//        public void Load()
//        {
//            if (Uid == Guid.Empty)
//                throw new ArgumentException("Value is Guid.Empty!", $"{Uid}");
//            SqlParameter[] parameters = new SqlParameter[] {
//                new SqlParameter("@UID", System.Data.SqlDbType.UniqueIdentifier) { Value = Uid },
//            };
//            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Contragents.GetItemByUid, parameters, (SqlDataReader reader) =>
//            {
//                while (reader.Read())
//                {
//                    Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
//                    Name = SqlConnect.GetValueAsString(reader, "Name");
//                    CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate");
//                    ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt");
//                    RRefID = SqlConnect.GetValueAsString(reader, "1CRRefID");
//                    IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "Marked");
//                }
//            });
//        }

//        #endregion
//    }
//}
