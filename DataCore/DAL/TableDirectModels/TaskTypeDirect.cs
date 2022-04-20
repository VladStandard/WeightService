﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.Data.SqlClient;
using System;

namespace DataCore.DAL.TableDirectModels
{
    public class TaskTypeDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public Guid Uid { get; set; }
        public string Name { get; set; }

        #endregion

        #region Constructor and destructor

        public TaskTypeDirect()
        {
            Uid = Guid.Empty;
            Name = string.Empty;
        }

        public TaskTypeDirect(Guid uid, string name)
        {
            Uid = uid;
            Name = name;
        }

        #endregion

        #region Public and private methods

        public void Save(string name)
        {
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.AddTaskType))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public void Save()
        {
            Save(Name);
        }

        #endregion
    }
}
