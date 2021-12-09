//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Collections.Generic;
//using System.Configuration;

//namespace terra.Common
//{
//    public static class SqlHelper
//    {
//        #region Public properties

//        public static SqlConnection GetConnection(string connectionString)
//        {
//            var connection = new SqlConnection();
//            try
//            {
//                connection.ConnectionString = connectionString;
//                connection.Open();
//            }
//            catch (Exception ex)
//            {
//                log.Error(ex.Message);
//            }
//            return connection;
//        }

//        public static SqlConnection GetConnection()
//        {
//            var connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
//            var connection = new SqlConnection();
//            try
//            {
//                connection.ConnectionString = connectionString;
//                connection.Open();
//            }
//            catch (Exception ex)
//            {
//                log.Error(ex.Message);
//            }
//            return connection;
//        }


//        public static SqlCommand GetCommand(string commandText, SqlConnection conn, List<SqlParameter> sqlParameters = null, int commandTimeout = 30)
//        {
//            var command = new SqlCommand();
//            try
//            {
//                command.Connection = conn;
//                command.CommandTimeout = commandTimeout;
//                command.CommandText = commandText;

//                command.Parameters.Clear();
//                if (sqlParameters != null)
//                {
//                    foreach (var sqlParameter in sqlParameters)
//                    {
//                        command.Parameters.Add(sqlParameter);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                log.Error(ex.Message);
//            }
//            return command;
//        }

//        public static SqlCommand GetCommand(string commandText, SqlConnection conn, List<SqlParameter> sqlParameters = null )
//        {
//            var commandTimeout = Int32.Parse(ConfigurationManager.AppSettings["dbCommandTimeout"].ToString());

//            var command = new SqlCommand();
//            try
//            {
//                command.Connection = conn;
//                command.CommandTimeout = commandTimeout;
//                command.CommandText = commandText;

//                command.Parameters.Clear();
//                if (sqlParameters != null)
//                {
//                    foreach (var sqlParameter in sqlParameters)
//                    {
//                        command.Parameters.Add(sqlParameter);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                log.Error(ex.Message);
//            }
//            return command;
//        }

//        #endregion
//    }
//}