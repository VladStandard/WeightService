//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.DAL.Models;
//using Microsoft.Data.SqlClient;
//using System;

//namespace DataCore.DAL.TableDirectModels
//{
//    [Serializable]
//    public class BarCodeTypeDirect : BaseSerializeEntity<BarCodeTypeDirect>
//    {
//        #region Public and private fields and properties

//        public long Id { get; set; } = default;
//        public string Name { get; set; } = string.Empty;

//        #endregion

//        #region Constructor and destructor

//        public BarCodeTypeDirect()
//        {
//            Load(default);
//        }

//        public BarCodeTypeDirect(long id)
//        {
//            Load(id);
//        }

//        #endregion

//        #region Public and private methods

//        public override string ToString()
//        {
//            return
//                $"{nameof(Id)}: {Id}. " +
//                $"{nameof(Name)}: {Name}. ";
//        }

//        public void Load(long id)
//        {
//            if (id == default) return;
//            Id = id;

//            SqlParameter[] parameters = new SqlParameter[] {
//                new SqlParameter("@ID", System.Data.SqlDbType.BigInt) { Value = Id },
//            };
//            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.BarCodeTypes.GetItemById, parameters, (SqlDataReader reader) =>
//            {
//                while (reader.Read())
//                {
//                    Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
//                    Name = SqlConnect.GetValueAsString(reader, "NAME");
//                }
//            });
//        }

//        #endregion
//    }
//}
