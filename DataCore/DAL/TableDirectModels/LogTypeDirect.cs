﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.Utils;
using Microsoft.Data.SqlClient;
using System;

namespace DataCore.DAL.TableDirectModels
{
    public class LogTypeDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

        public Guid Uid { get; set; }
        public byte Number { get; set; }
        public string Icon { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public LogTypeDirect()
        {
            Uid = Guid.Empty;
            Number = 0;
            Icon = string.Empty;
        }

        public LogTypeDirect(Guid uid, byte number, string icon) : this()
        {
            Uid = uid;
            Number = number;
            Icon = icon;
        }

        #endregion

        #region Public and private methods

        public void Save(byte number, string icon)
        {
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            StringUtils.SetStringValueTrim(ref icon, 32);
            using (SqlCommand cmd = new(SqlQueries.DbServiceManaging.Tables.Logs.AddLogType))
            {
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@number", number);
                cmd.Parameters.AddWithValue("@icon", icon);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public void Save()
        {
            Save(Number, Icon);
        }

        #endregion
    }
}